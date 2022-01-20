using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumpcloud_APITest
{
    internal static class Utils
    {
        public static Random Random = new Random();

        /// <summary>
        /// Sleep for provided milliseconds
        /// </summary>
        /// <param name="ms"></param>
        internal static void Sleep(int ms) => Task.Delay(ms);

        /// <summary>
        /// Returns true if string is base64
        /// </summary>
        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        /// <summary>
        /// Create environment variable PORT equal to provided port
        /// </summary>
        public static void SetPort(int port) => Environment.SetEnvironmentVariable("PORT", port.ToString());
    }
}
