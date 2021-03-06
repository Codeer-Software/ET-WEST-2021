using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

namespace Driver.Windows
{
    [WindowDriver(TypeFullName = "etwest.MainWindow")]
    public class MainWindowDriver
    {
        public WindowControl Core { get; }
        public WPFTabControl TabControl => Core.LogicalTree().ByType("System.Windows.Controls.TabControl").Single().Dynamic();
        public MonitorDriver Monitor => Core.LogicalTree().ByType("etwest.Pages.Monitor").FirstOrDefault()?.Dynamic();
        public SampleDriver SampleDriver => Core.LogicalTree().ByType("etwest.Pages.Sample").FirstOrDefault()?.Dynamic();

        public MainWindowDriver(WindowControl core)
        {
            Core = core;
        }

        public MainWindowDriver(AppVar core)
        {
            Core = new WindowControl(core);
        }
    }

    public static class MainWindowDriverExtensions
    {
        [WindowDriverIdentify(TypeFullName = "etwest.MainWindow")]
        public static MainWindowDriver AttachMainWindow(this WindowsAppFriend app)
            => app.WaitForIdentifyFromTypeFullName("etwest.MainWindow").Dynamic();
    }
}