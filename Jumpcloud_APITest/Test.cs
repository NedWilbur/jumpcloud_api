using NUnit.Framework;
using System.Net;

namespace Jumpcloud_APITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Log.Info($"Starting test '{TestContext.CurrentContext.Test.FullName}'");
            //Actions.StartService(); // TODO: Not working. investigate.
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
            var pass = Actions.GenerateHash();
            var hash = Actions.GetHash(1);
            var stats = Actions.GetStats();
        }

        [Test]
        [Description("Application opens with configured PORT")]
        public void TC_OpenWithPort()
        {
            // TODO
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
            Assert.IsTrue(Actions.IsBase64String(response.Content));
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

            Actions.Sleep(6000); // TODO: Dev better way to detect when async task are completed
            int jobId = int.Parse(Actions.GenerateHash().Content);
            Assert.AreEqual(numberOfPasswords, jobId);
        }

        [Test]
        [Description("Application hashes passwords during shutdown grace period")]
        public void Test5()
        {

        }

        [Test]
        [Description("Application denies password hash request during pending shutdown")]
        public void Test6()
        {

        }

        [Test]
        [Description("Application provides accurate stats")]
        public void Test7()
        {

        }
    }
}