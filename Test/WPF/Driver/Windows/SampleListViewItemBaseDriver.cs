using Codeer.Friendly;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;

namespace Driver.Windows
{
    [UserControlDriver(TypeFullName = "System.Windows.Controls.ListViewItem")]
    public class SampleListViewItemBaseDriver
    {
        public WPFUIElement Core { get; }

        public SampleListViewItemBaseDriver(AppVar core)
        {
            Core = new WPFUIElement(core);
        }
    }
}
