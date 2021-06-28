using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Linq;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "etwest.Pages.Sample")]
    public class SampleDriver
    {
        public WPFUIElement Core { get; }
        public WPFTextBox textBox => Core.Dynamic().textBox; 
        public WPFComboBox comboBox => Core.Dynamic().comboBox; 
        public WPFToggleButton check1 => Core.Dynamic().check1; 
        public WPFToggleButton check2 => Core.Dynamic().check2; 
        public WPFToggleButton check3 => Core.Dynamic().check3; 
        public WPFToggleButton radio1 => Core.Dynamic().radio1; 
        public WPFToggleButton radio2 => Core.Dynamic().radio2; 
        public WPFToggleButton radio3 => Core.Dynamic().radio3; 
        public WPFListBox listBox => Core.Dynamic().listBox; 
        public WPFDatePicker DatePicker => Core.LogicalTree().ByType("System.Windows.Controls.DatePicker").Single().Dynamic(); 
        public WPFListView<SampleListViewItemBaseDriver> _listView => Core.Dynamic()._listView; 
        public WPFDataGrid dataGrid => Core.Dynamic().dataGrid; 

        public SampleDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}