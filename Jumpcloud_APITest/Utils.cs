using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
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
        private static Random Random = new Random();
        private static Process appProcess;
        private static Dictionary<string, string> headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } };

        internal static void StartService()
        {
            appProcess = Process.Start(Config.AppPath);
        }

        internal static void StopService()
        {
            Api.Post(Config.BaseUrl, headers, "shutdown");
        }

        internal static void KillService()
        {
            appProcess?.Kill();
            appProcess?.WaitForExit(10 * 1000);
        }

        internal static IRestResponse GenerateHash(string password = null)
        {
            if (password == null) password = Random.Next(5000).ToString(); // TODO: Use faker lib to generate better fake data
            string body = JsonConvert.SerializeObject(new{ password = password });
            return Api.Post($"{Config.BaseUrl}/hash", headers, body);
        }

        internal static IRestResponse GetHash(int id)
        {
            return Api.Get($"{Config.BaseUrl}/hash/{id}", headers, null);
        }

        internal static Stats GetStats()
        {
            IRestResponse response = Api.Get($"{Config.BaseUrl}/stats", headers, null);
            return JsonConvert.DeserializeObject<Stats>(response.Content);
        }

        internal static void Sleep(int ms) => Task.Delay(ms);
    }
}
