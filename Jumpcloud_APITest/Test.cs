using NUnit.Framework;

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
        public void Test1()
        {
            var pass = Actions.GenerateHash();
            var hash = Actions.GetHash(1);
            var stats = Actions.GetStats();
        }
    }
}