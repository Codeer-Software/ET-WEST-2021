using System;
using System.Collections.Generic;
using System.Windows;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Driver.InTarget
{
    [CaptureCodeGenerator("Driver.Controls.ChartDriver")]
    public class ChartDriverGenerator : CaptureCodeGeneratorBase
    {
        List<Action> _removes = new List<Action>();
        UIElement _element;

        protected override void Attach()
        {
            _element = (UIElement)ControlObject;
            _removes.Add(EventAdapter.Add(ControlObject, "SelectionChanged", OnSelectionChanged));
        }

        protected override void Detach()
            => _removes.ForEach(e => e());

        private void OnSelectionChanged(object sender, dynamic args)
        {
            DateTime start = args.Start;
            DateTime end = args.End;
            AddUsingNamespace(typeof(DateTime).Namespace);
            AddSentence(new TokenName(), ".EmulateChangeSelection", $"(new DateTime({ start.Year}, {start.Month}, {start.Day}, {start.Hour}, {start.Minute}, {start.Second}), new DateTime({ end.Year}, {end.Month}, {end.Day}, {end.Hour}, {end.Minute}, {end.Second}));");
        }

        public override void Optimize(List<Sentence> code)
        {
            GenerateUtility.RemoveDuplicationFunction(this, code, "EmulateChangeSelection");
        }
    }
}
