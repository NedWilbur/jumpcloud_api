using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumpcloud_APITest
{
    internal static class Log
    {
        // TODO: Add additional levels
        internal static void Info(string message)
        {
            TestContext.Progress.WriteLine(message);
            Console.WriteLine("INFO | " + message);
        }
    }
}
