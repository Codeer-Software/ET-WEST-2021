using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace PageObject.Pages
{
    public class SideBar : ComponentBase
    {
        public AnchorDriver モニタ => ByLinkText("モニタ").Wait();
        public AnchorDriver 記録 => ByLinkText("記録").Wait();
        public AnchorDriver サンプル => ByLinkText("サンプル").Wait();

        public SideBar(IWebElement element) : base(element) { }
        public static implicit operator SideBar(ElementFinder finder) => finder.Find<SideBar>();
    }
}