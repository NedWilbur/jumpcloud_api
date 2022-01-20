using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;

[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(1)]

namespace Jumpcloud_APITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Log.Info($"Starting test '{TestContext.CurrentContext.Test.FullName}'");
            Actions.StartService();
        }

        [TearDown]
        public void Teardown()
        {
            Log.Info($"Stopping test '{TestContext.CurrentContext.Test.FullName}'");
            Actions.KillService();
        }

        [Test]
        [Description("Used to test actions")]
        public void Sandbox()
        {
            //var pass = Actions.GenerateHash();
            //var hash = Actions.GetHash(1);
            //var stats = Actions.GetStats();
        }

        [Test]
        [Description("Application hashes provided password and returns job id greater than 0")]
        public void TC_HashPassword()
        {
            int jobId = int.Parse(Actions.GenerateHash().Content);
            Assert.Greater(jobId, 0);
        }
        

        [Test]
        [Description("Application returns error when provided empty password")]
        public void TC_EmptyPassword()
        {
            // Currently fails - unsure if expected behavior or a bug
            var response = Actions.GenerateHash(string.Empty);
            Assert.AreNotEqual(HttpStatusCode.OK, response.StatusCode); // TODO: Get expected failed status code
        }

        [Test]
        [Description("Application returns hash for provided job id")]
        public void TC_GetJobHash()
        {
            int jobId = int.Parse(Actions.GenerateHash().Content);
            var response = Actions.GetHash(jobId);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(Utils.IsBase64String(response.Content));
        }

        [Test]
        [Description("Application returns error for invalid job id")]
        public void TC_InvalidJobId()
        {
            var response = Actions.GetHash(0);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [Description("Application can hash multiple passwords simultaniously")]
        public void TC_HashMultiplePasswordsAsync()
        {
            int numberOfPasswords = 10;

            for (int i = 0; i < numberOfPasswords; i++)
                Actions.GenerateHashAsync();
            Utils.Sleep(10000); // TODO: Actually detect when async task are completed

            int jobId = int.Parse(Actions.GenerateHash().Content);
            Assert.AreEqual(numberOfPasswords, jobId);
        }

        [Test]
        [Description("Application hashes passwords during shutdown grace period")]
        [Ignore("In progress...")]
        public void TC_HashPasswordsDuringGracePeriod()
        {
            for (int i = 0; i < 10; i++)
                Actions.GenerateHashAsync();

            int jobId = int.Parse(Actions.GenerateHash().Content);
            var response = Actions.GetHash(jobId);
            Actions.ShutdownService();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(Utils.IsBase64String(response.Content));
        }

        [Test]
        [Description("Application denies password hash request during pending shutdown")]
        [Ignore("In progress...")]
        public void TC_HashRequestDuringShutdownReturnsError()
        {
            Actions.ShutdownService();
            int jobId = int.Parse(Actions.GenerateHash().Content);
            var response = Actions.GetHash(jobId);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(Utils.IsBase64String(response.Content));
        }

        [Test]
        [Description("Application provides accurate stats")]
        public void TC_GetStats()
        {
            for (int i = 0; i < 10; i++)
                Actions.GenerateHashAsync();
            Utils.Sleep(10000);

            var stats = JsonConvert.DeserializeObject<Stats>(Actions.GetStats().Content);
            Assert.IsNotNull(stats);
            Assert.AreEqual(10, stats?.TotalRequests); // BUG?: TotalRequest not calculated until after task completed (expected?)
            Assert.Greater(0, stats?.AverageTime); // BUG: AverageTime = 0
        }

        [Test]
        [Description("JobId increments for each requested hash")]
        public void TC_RequestHashPasswordIncrementsJobId()
        {
            for (int i = 1; i <= 5; i++)
                Assert.AreEqual(i, int.Parse(Actions.GenerateHash().Content));
        }

        [Test]
        [Description("/stats does not accept data")]
        public void TC_StatsDoesNotAcceptData()
        {
            var response = Actions.GetStats("Hello World");
            Assert.AreEqual(0, (int)response.StatusCode);
        }
    }
}