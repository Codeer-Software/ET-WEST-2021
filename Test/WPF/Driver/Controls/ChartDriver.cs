using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System;

namespace Driver.Controls
{
    [ControlDriver(TypeFullName = "etwest.Controls.Chart", Priority = 2)]
    public class ChartDriver : WPFUIElement
    {
        public DateTime Start => this.Dynamic()._start;
        public DateTime End => this.Dynamic()._end;

        public ChartDriver(AppVar appVar)
            : base(appVar) { }

        public void EmulateChangeSelection(DateTime start, DateTime end)
        {
            this.Dynamic()._start = start;
            this.Dynamic()._end = end;
            this.Dynamic().UpdateSelection();
        }
    }
}
