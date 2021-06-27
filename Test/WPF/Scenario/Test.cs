using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;

namespace Scenario
{
    [TestFixture]
    public class Test
    {
        WindowsAppFriend _app;

        [SetUp]
        public void TestInitialize() => _app = ProcessController.Start();

        [TearDown]
        public void TestCleanup() => _app.Kill();

        [Test]
        public void TestMethod1()
        {

        }
    }
}
