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
    internal static class Actions
    {
        private static Random Random = new Random();
        private static Process appProcess;
        private static Dictionary<string, string> headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } };

        internal static void StartService()
        {
            Log.Info("Starting service");
            appProcess = Process.Start(Config.AppPath);
        }

        internal static void StopService()
        {
            Log.Info("Stopping service");
            Api.Post(Config.BaseUrl, headers, "shutdown");
        }

        internal static void KillService()
        {
            Log.Info("Killing service");
            appProcess?.Kill();
            appProcess?.WaitForExit(10 * 1000);
        }

        internal static IRestResponse GenerateHash(string password = null)
        {
            if (password == null) password = Random.Next(5000).ToString(); // TODO: Use faker lib to generate better fake data
            Log.Info($"Generating hash for {password}");

            string body = JsonConvert.SerializeObject(new{ password = password });
            return Api.Post($"{Config.BaseUrl}/hash", headers, body);
        }

        internal static IRestResponse GetHash(int id)
        {
            Log.Info($"Getting hash for id {id}");
            return Api.Get($"{Config.BaseUrl}/hash/{id}", headers, null);
        }

        internal static Stats GetStats()
        {
            Log.Info("Getting stats");
            IRestResponse response = Api.Get($"{Config.BaseUrl}/stats", headers, null);
            return JsonConvert.DeserializeObject<Stats>(response.Content);
        }

        internal static void Sleep(int ms) => Task.Delay(ms);
    }
}
