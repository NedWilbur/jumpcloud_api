using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jumpcloud_APITest
{
    internal static class Actions
    {
        private static Random Random = new Random();
        private static Process appProcess;
        private static Dictionary<string, string> headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } };


        /// <summary>
        /// Starts service located at {Config.AppPath}
        /// </summary>
        internal static void StartService()
        {
            Log.Info("Starting service");
            appProcess = Process.Start(Config.AppPath);
        }

        /// <summary>
        /// Request service shutdown
        /// </summary>
        internal static void Shutdown()
        {
            throw new NotImplementedException("Shutdown not currently working");

            Log.Info("Stopping service");
            IRestResponse response = Api.Post(Config.BaseUrl, headers, "shutdown", false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Force kill service
        /// </summary>
        internal static void KillService()
        {
            Log.Info("Killing service");
            appProcess?.Kill();
            appProcess?.WaitForExit(10 * 1000);
        }

        /// <summary>
        /// Generate a new password hash
        /// </summary>
        /// <param name="password">Password to hash. If not provided, a random password will be used.</param>
        /// <returns></returns>
        internal static IRestResponse GenerateHash(string password = null)
        {
            if (password == null) password = Random.Next(5000).ToString(); // TODO: Use faker lib to generate better fake data
            Log.Info($"Generating hash for {password}");

            string body = JsonConvert.SerializeObject(new{ password = password });
            return Api.Post($"{Config.BaseUrl}/hash", headers, body);
        }

        /// <summary>
        /// Generate a new password hash async (does not wait for task to complete)
        /// </summary>
        /// <param name="password">Password to hash. If not provided, a random password will be used.</param>
        // TODO: Investigate way to reduce redundancy of below method (similar to above method)
        internal static async void GenerateHashAsync(string password = null)
        {
            if (password == null) password = Random.Next(5000).ToString(); // TODO: Use faker lib to generate better fake data
            Log.Info($"Generating hash for {password} (ASYNC)");

            string body = JsonConvert.SerializeObject(new { password = password });
            Api.PostAsync($"{Config.BaseUrl}/hash", headers, body);
        }

        /// <summary>
        /// Request hash for provided job ID
        /// </summary>
        /// <param name="jobId">Job id</param>
        /// <returns></returns>
        internal static IRestResponse GetHash(int jobId)
        {
            Log.Info($"Getting hash for job id {jobId}");
            return Api.Get($"{Config.BaseUrl}/hash/{jobId}", headers, null);
        }

        /// <summary>
        /// Request stats
        /// </summary>
        /// <returns>Stats object containing TotalRequest and AverageTime </returns>
        internal static Stats GetStats()
        {
            Log.Info("Getting stats");
            IRestResponse response = Api.Get($"{Config.BaseUrl}/stats", headers, null);
            return JsonConvert.DeserializeObject<Stats>(response.Content);
        }

        // Utils (TODO: Move to Util class?)
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
    }
}
