using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System.Text.RegularExpressions;

namespace MyKernelEventMonitor
{
    public class KernelMonitor
    {
        static TextWriter _out = Console.Out;
        static EventLogger _logger = new EventLogger();

        public static void Run()
        {
            // Check OS version
            if (Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor < 62)
            {
                _out.WriteLine("The monitor only works on Win 8 / Win Server 2012 and above.");
                return;
            }
            
            // Check the process's privilege
            if (TraceEventSession.IsElevated() != true)
            {
                _out.WriteLine("You must be elevated as admin to run this program.");
                return;
            }

            TraceEventSession session = null;

            _out.WriteLine("************** KernelEventMonitor ***************");
            _out.WriteLine("Press <C>-C to stop the monitor!");
            _out.WriteLine();

            // Set up <C>-C to stop kernel mode sessions
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs cancelArgs)
            {
                if (session != null)
                    session.Dispose();
                cancelArgs.Cancel = true;
            };

            // Set up a timer to stop processing after monitorTime
            //var timer = new Timer(delegate (object state)
            //{
            //    Out.WriteLine("Stopped Monitoring after {0} sec", monitorTime);
            //    if (session != null)
            //        session.Dispose();
            //}, null, monitorTime * 1000, Timeout.Infinite);

            // Create new session to receive kernel evetn
            using (session = new TraceEventSession("MonitorKernelEventSession"))
            {
                //Enable the kernel events we interest
                _out.WriteLine("Enabling Image Load, Process, FileIO, Registry and Network");
                session.EnableKernelProvider(
                    KernelTraceEventParser.Keywords.ImageLoad |
                    KernelTraceEventParser.Keywords.Process |
                    KernelTraceEventParser.Keywords.FileIOInit |
                    KernelTraceEventParser.Keywords.Registry |
                    KernelTraceEventParser.Keywords.NetworkTCPIP);

                // Subscribe kernel events we interest
                session.Source.UnhandledEvents += EventProcess;
                session.Source.Kernel.FileIODelete += EventProcess;
                session.Source.Kernel.FileIOCreate += EventProcess;
                session.Source.Kernel.FileIODirEnum += EventProcess;
                session.Source.Kernel.FileIORead += EventProcess;
                session.Source.Kernel.FileIORename += EventProcess;
                session.Source.Kernel.FileIOWrite += EventProcess;
                session.Source.Kernel.ProcessStop += EventProcess;
                session.Source.Kernel.ProcessStart += EventProcess;
                session.Source.Kernel.ImageLoad += EventProcess;
                session.Source.Kernel.TcpIpSend += EventProcess;
                session.Source.Kernel.TcpIpRecv += EventProcess;
                session.Source.Kernel.UdpIpRecv += EventProcess;
                session.Source.Kernel.UdpIpSend += EventProcess;
                session.Source.Kernel.RegistryCreate += EventProcess;
                session.Source.Kernel.RegistryDelete += EventProcess;
                session.Source.Kernel.RegistryDeleteValue += EventProcess;
                session.Source.Kernel.RegistrySetValue += EventProcess;

                // Begin to monitor events
                session.Source.Process();
            }

            //timer.Dispose();
        }

        static void EventProcess(TraceEvent data)
        {
            //Debugger.Break();
            try
            {
                if (data.Opcode == TraceEventOpcode.DataCollectionStart || data.Opcode == TraceEventOpcode.DataCollectionStop)
                    return;

                //Regex re = new Regex(@"C:\\users\\mouka\\desktop\\.*", RegexOptions.IgnoreCase);
                //if (!re.IsMatch(data.PayloadByName("DirectoryName").ToString()))
                //    return;
                //if (data.PayloadByName("KeyName").ToString().Equals(""))
                //    return;
                //Process currentProcess = Process.GetProcessById(data.ProcessID);

                //Debugger.Break();
                //_out.Write("****** Got Event ******");
                //_out.WriteLine("EventName: {0}", data.EventName);
                _logger.LogEvent((dynamic)data);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Got Unhandled event1111!");
                throw e;
            }


        }
    }

}