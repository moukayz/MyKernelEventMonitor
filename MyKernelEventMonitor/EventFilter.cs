using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System.Threading.Tasks;

namespace MyKernelEventMonitor
{
    class EventFilter
    {
        Process my = new Process();

        public void FilterEvent(TraceEvent data)
        {
            //Process.
        }
    }
}
