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
        [Description("Used to test actions")]
        public void Sandbox()
        {
            var pass = Actions.GenerateHash();
            var hash = Actions.GetHash(1);
            var stats = Actions.GetStats();
        }

        [Test]
        [Description("Application opens with configured PORT")]
        public void Test1()
        {

        }

        [Test]
        [Description("Application hashes provided password and returns job id")]
        public void Test2()
        {

        }

        [Test]
        [Description("Application returns hash for provided job id")]
        public void Test3()
        {

        }

        [Test]
        [Description("Application returns error for invalid job id")]
        public void Test33()
        {

        }

        [Test]
        [Description("Application hashes multiple passwords simultaniously")]
        public void Test4()
        {

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
    }
}