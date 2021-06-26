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
            //TODO JS使ったり、SeleniumのAction使ったり
        }

        public void Edit(string text)
        {
            Element.SendKeys(text);
        }

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            //TODO キャプチャ時にコード生成するJS
            //以下は完全にダミー
            var guid = Guid.NewGuid();
            return $@"

    if (!window.__GeneratorDictionary) {{
        window.__GeneratorDictionary = {{}};
    }}

    //クリックはイベントで検知
    element.addEventListener('click', function() {{ 
        var name = __codeerTestAssistantPro.getElementName(this);
        __codeerTestAssistantPro.pushCode(name + '.Click();');
    }}, false);


    //これでポーリング登録できる
    __codeerTestAssistantPro.addPolling(() => {{
        
        //なんか見張ってて変化があったら的な感じ
        let oldValue = window.__GeneratorDictionary['{guid}'];
        let value = element.innerText;
        if (oldValue === undefined) {{
            oldValue = value;
        }}


        if (oldValue != value) {{
            //ここに生成するコードを登録する
            const name = __codeerTestAssistantPro.getElementName(element);
            __codeerTestAssistantPro.pushCode(name + '.SendKeys(""' + value + '"");');
        }}

        window.__GeneratorDictionary['{guid}'] = value;
        }});
".Trim();
        }

        public static implicit operator ChartDriver(ElementFinder finder) => finder.Find<ChartDriver>();
    }
}

