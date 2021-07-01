using Codeer.Friendly.Windows;
using Driver.TestController;
using NUnit.Framework;
using Driver.Windows;
using System;

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

            var mainWindow = _app.AttachMainWindow();
            mainWindow.SampleDriver.textBox.EmulateChangeText("yyy");
            mainWindow.SampleDriver.check2.EmulateCheck(true);
            mainWindow.SampleDriver.check3.EmulateCheck(true);
            var sampleListViewItem3 = mainWindow.SampleDriver._listView.GetItemDriver(2).AsSampleListViewItem3();
            sampleListViewItem3.SliderData.EmulateChangeValue(5);
            mainWindow.SampleDriver.listBox.EmulateChangeSelectedIndex(2);
            mainWindow.TabControl.EmulateChangeSelectedIndex(2);
            mainWindow.SampleDriver.listBox.EmulateChangeSelectedIndex(1);


        }
    }
}

