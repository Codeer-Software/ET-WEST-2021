using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using Driver.Controls;
using RM.Friendly.WPFStandardControls;
using System.Linq;
using System.Windows.Controls;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "etwest.Pages.Monitor")]
    public class MonitorDriver
    {
        public WPFUIElement Core { get; }
        public ChartDriver Chart => Core.Dynamic().chart; 
        public WPFToggleButton Pause => Core.Dynamic().pause; 
        public WPFTextBox Log => Core.Dynamic().TextBox; 
        public WPFTextBox Name => Core.Dynamic().logName; 
        public WPFButtonBase Record => Core.LogicalTree().ByType<ContentControl>().ByContentText("記録").Single().Dynamic(); 
        public WPFButtonBase Release => Core.LogicalTree().ByType<ContentControl>().ByContentText("解除").Single().Dynamic(); 

        public MonitorDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}