using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "etwest.Pages.History")]
    public class HistoryDriver
    {
        public WPFUIElement Core { get; }
        public WPFListView list => Core.Dynamic().list; 
        public WPFTextBox SelectedItemContent => Core.LogicalTree().ByBinding("SelectedItem.Content").Single().Dynamic(); 

        public HistoryDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}