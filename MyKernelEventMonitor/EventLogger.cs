using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System.Diagnostics;
using System.IO;

namespace MyKernelEventMonitor
{
    class EventLogger : IDisposable
    {
        public EventLogger()
        {
            _imageWriter = new EventLogWriter(_imageLogName);
            _processWriter = new EventLogWriter(_processLogName);
            _fileIOWriter = new EventLogWriter(_fileIOLogName);
            _registryWriter = new EventLogWriter(_registryLogName);
            _netWriter = new EventLogWriter(_netLogName);
        }

        #region UnhandledEventLog for debug
        public void LogEvent(UnhandledTraceEvent data)
        {
            //Console.WriteLine("Got Unhandled Event2222!");
            return;
        }
		#endregion
        #region ImageEventLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
		public void LogEvent(ImageLoadTraceData data)
        {
            //---------
            // Filter event data
            // Not written yet..
            //----------

            //---------------
            // Log ImageLoad event 
            //Stream log = File.Open(_imageLogName, FileMode.Append, FileAccess.ReadWrite);
            //using (EventLogWriter writer = new EventLogWriter(_imageLogName))
            //{
            //    LogRow text = new LogRow();

            //    text.Add(data.TimeStamp.ToString());
            //    text.Add(data.EventName);
            //    text.Add(data.ProcessID.ToString());
            //    text.Add(data.ProcessName);
            //    text.Add(data.FileName);
            //    writer.WriteRow(text);
            //}
            //------------------
            LogRow text = new LogRow();
            _imageWriter.WriteHeader(data, text);
            text.Add(data.FileName);
            _imageWriter.WriteRow(text);
        }
        #endregion
        #region ProcEventLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void LogEvent(ProcessTraceData data)
        {
            //---------
            // Filter event data
            // Not written yet..
            //----------

            //-----------------
            // Log Process/Stop/Start event
            //using (EventLogWriter writer = new EventLogWriter(_processLogName))
            //{
            //    LogRow text = new LogRow();

            //    text.Add(data.TimeStamp.ToString());
            //    text.Add(data.EventName);
            //    text.Add(data.ProcessID.ToString());
            //    text.Add(data.ProcessName);
            //    text.Add(data.CommandLine);
            //    text.Add(data.ImageFileName);
            //    writer.WriteRow(text);
            //}
            LogRow text = new LogRow();
            _processWriter.WriteHeader(data, text);
            text.Add(data.CommandLine);
            text.Add(data.ImageFileName);
            _processWriter.WriteRow(text);
        }
        #endregion
        #region FileEventLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void LogEvent(FileIOCreateTraceData data)
        {
            #region MyRegion
            //---------
            // Filter event data
            // Not written yet..
            //----------
            //---------------
            // Log ImageLoad event 
            //using (EventLogWriter writer = new EventLogWriter(_fileIOLogName))
            //{
            //    LogRow text = new LogRow();

            //    text.Add(data.TimeStamp.ToString());
            //    text.Add(data.EventName);
            //    text.Add(data.ProcessID.ToString());
            //    text.Add(data.ProcessName);
            //    text.Add(data.FileName);
            //    text.Add(data.CreateOptions.ToString());
            //    writer.WriteRow(text);
            //}
            //------------------ 
            #endregion
            LogRow text = new LogRow();
            _fileIOWriter.WriteHeader(data, text);
            text.Add(data.FileName);
            _fileIOWriter.WriteRow(text);
        }
        public void LogEvent(FileIOInfoTraceData data)
        {
            #region Comments
            //LogEvent((FileIOCreateTraceData)data);
            //---------
            // Filter event data
            // Not written yet..
            //----------
            //---------------
            // Log ImageLoad event 
            //using (EventLogWriter writer = new EventLogWriter(_fileIOLogName))
            //{
            //    LogRow text = new LogRow();
            //    text.Add(data.TimeStamp.ToString());
            //    text.Add(data.EventName);
            //    text.Add(data.ProcessID.ToString());
            //    text.Add(data.ProcessName);
            //    text.Add(data.FileName);
            //    writer.WriteRow(text);
            //}
            //------------------ 
            #endregion
            LogRow text = new LogRow();
            _fileIOWriter.WriteHeader(data, text);
            text.Add(data.FileName);
            _fileIOWriter.WriteRow(text);
        }
        public void LogEvent(FileIODirEnumTraceData data)
        {
            //---------
            // Filter event data
            // Not written yet..
            //----------

            //---------------
            // Log ImageLoad event 
            LogRow text = new LogRow();
            _fileIOWriter.WriteHeader(data, text);
            text.Add(data.FileName);
            text.Add(data.DirectoryName);
            _fileIOWriter.WriteRow(text);
        }
        public void LogEvent(FileIOReadWriteTraceData data)
        {
            //---------
            // Filter event data
            // Not written yet..
            //----------

            //---------------
            // Log ImageLoad event 
            LogRow text = new LogRow();
            _fileIOWriter.WriteHeader(data, text);
            text.Add(data.FileName);
            _fileIOWriter.WriteRow(text);
            //------------------
        }
        #endregion
        #region RegEventLog
        public void LogEvent(RegistryTraceData data)
        {
            LogRow text = new LogRow();
            _registryWriter.WriteHeader(data, text);
            text.Add(data.KeyName);
            text.Add(data.ValueName);
            _registryWriter.WriteRow(text);
        }
        #endregion
        #region NetEventLog
        public void LogEvent(TcpIpSendTraceData data)
        {
            LogRow text = new LogRow();
            _netWriter.WriteHeader(data, text);
            text.Add(data.saddr.ToString());
            text.Add(data.sport.ToString());
            text.Add(data.daddr.ToString());
            text.Add(data.dport.ToString());
            _netWriter.WriteRow(text);
        }
        public void LogEvent(TcpIpTraceData data)
        {
            LogRow text = new LogRow();
            _netWriter.WriteHeader(data, text);
            text.Add(data.saddr.ToString());
            text.Add(data.sport.ToString());
            text.Add(data.daddr.ToString());
            text.Add(data.dport.ToString());
            _netWriter.WriteRow(text);

        }
        public void LogEvent(UdpIpTraceData data)
        {
            LogRow text = new LogRow();
            _netWriter.WriteHeader(data, text);
            text.Add(data.saddr.ToString());
            text.Add(data.sport.ToString());
            text.Add(data.daddr.ToString());
            text.Add(data.dport.ToString());
            _netWriter.WriteRow(text);
        }
        #endregion

        public void Dispose()
        {

        }

        //Dictionary<Type, Action> _logEvents;
        EventLogWriter _imageWriter;
        EventLogWriter _processWriter;
        EventLogWriter _fileIOWriter;
        EventLogWriter _registryWriter;
        EventLogWriter _netWriter;
        static string _imageLogName = "ImageLog.csv";
        static string _processLogName = "ProcessLog.csv";
        static string _fileIOLogName = "FileIOLog.csv";
        static string _registryLogName = "RegistryLog.csv";
        static string _netLogName = "NetLog.csv";
        static List<string> _imageLoadHeader = new List<string> { "TimeStamp", "EventName", "ProcessID", "ProcessName", "ImagePath" };
    }

    class LogRow : List<string>
    {
        public LogRow() { }
        public LogRow(List<string> strings) { this.AddRange(strings); }
        public string rowText { get; set; }
    }

    class EventLogWriter : StreamWriter
    {
        public EventLogWriter(Stream stream) : base(stream) { }
        public EventLogWriter(string filename, bool append = true) : base(filename, append) { }

        public void WriteRow(LogRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool isFirstCol = true;

            foreach (string field in row)
            {
                if (!isFirstCol)
                    builder.Append(',');

                if (field.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", field.Replace("\"", "\"\""));
                else
                    builder.Append(field);
                isFirstCol = false;
            }
            row.rowText = builder.ToString();
            WriteLine(row.rowText);
        }
        public void WriteHeader(TraceEvent data, LogRow header)
        {
            header.Add(data.TimeStamp.ToString());
            header.Add(data.EventName);
            header.Add(data.ProcessID.ToString());
            header.Add(data.ProcessName);
        }

    }
}
