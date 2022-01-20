using NUnit.Framework;

namespace Jumpcloud_APITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Actions.StartService();
        }

        [TearDown]
        public void Teardown()
        {
            Actions.KillService();
        }

        [Test]
        public void Test1()
        {
            
        }
    }
}