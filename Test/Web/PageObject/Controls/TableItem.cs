using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace PageObject.Controls
{
    public class TableItem : ComponentBase
    {
        public IWebElement label => ByXPath("td[1]").Wait().Find();
        public CheckBoxDriver checkbox => ByCssSelector("input[type='checkbox']").Wait();
        public TextBoxDriver text => ByCssSelector("input[type='text']").Wait();
        public ButtonDriver button => ByTagName("button").Wait();

        public TableItem(IWebElement element) : base(element) { }
        public static implicit operator TableItem(ElementFinder finder) => finder.Find<TableItem>();
    }
}