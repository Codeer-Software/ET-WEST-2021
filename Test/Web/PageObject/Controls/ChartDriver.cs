using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
using System;

namespace PageObject.Controls
{
    public class ChartDriver : ControlDriverBase
    {
        public ChartDriver(IWebElement element) : base(element) { }

        public void Select(DateTime start, DateTime end)
        {
            JS.ExecuteScript("setPaused(true); setSelection(new Date(arguments[0]), new Date(arguments[1]));",
                start.ToString("O"), end.ToString("O"));
        }

        public void Edit(string text)
        {
            Element.SendKeys(text);
        }

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
    element.addEventListener(""x-selection"", e => {{
        const {{ start, end }} = e.detail;
        const generated = __codeerTestAssistantPro.getCode();
        const usings = __codeerTestAssistantPro.getUsings();
        const tail = generated.length - 1;
        const value = `.Select(DateTime.Parse(${{new Date(start).toISOString()}}), DateTime.Parse(${{new Date(end).toISOString()}}));`;
        const name = __codeerTestAssistantPro.getElementName(element);

        if(tail >= 0 && generated[tail].startsWith(name + '.Select(')){{
            generated.pop();
        }}
        __codeerTestAssistantPro.clearCodeAndUsing();
        for(const using of usings) {{ __codeerTestAssistantPro.pushUsings(usings); }}
        for(const code of generated) {{ __codeerTestAssistantPro.pushCode(code); }}
        __codeerTestAssistantPro.pushCode(name + value);
    }});
".Trim();
        }

        public static implicit operator ChartDriver(ElementFinder finder) => finder.Find<ChartDriver>();
    }
}

