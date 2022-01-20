using NUnit.Framework;

namespace Jumpcloud_APITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Utils.StartService();
        }

        [TearDown]
        public void Teardown()
        {
            Utils.KillService();
        }

        [Test]
        public void Test1()
        {
            Utils.Sleep(1000);
        }
    }
}