using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumpcloud_APITest
{
    internal static class Utils
    {
        private static Process appProcess;

        internal static void StartService()
        {
            appProcess = Process.Start(Config.AppPath);
        }

        internal static void StopService()
        {
            throw new NotImplementedException();
        }

        internal static void KillService()
        {
            appProcess?.Kill();
            appProcess?.WaitForExit(10 * 1000);
        }

        internal static void GenerateHash(string password)
        {
            throw new NotImplementedException();
        }

        internal static void GetHash(int id)
        {
            throw new NotImplementedException();
        }

        internal static void GetStats()
        {
            throw new NotImplementedException();
        }

        internal static void Sleep(int ms) => Task.Delay(ms);
    }
}
