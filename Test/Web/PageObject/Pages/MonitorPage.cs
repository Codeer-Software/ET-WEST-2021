using OpenQA.Selenium;
using PageObject.Controls;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace PageObject.Pages
{
    public class MonitorPage : PageBase
    {
        public ChartDriver Chart => ById("myChart").Wait();
        public CheckBoxDriver Pause => ById("pause").Wait();
        public TextAreaDriver SelectedValues => ById("selected-values").Wait();
        public TextBoxDriver Name => ById("input-log-name").Wait();
        public ButtonDriver Save => ById("button-save").Wait();
        public ButtonDriver Clear => ById("button-clear").Wait();
        public SideBar SideBar => ByTagName("app-menu").Wait();

        public MonitorPage(IWebDriver driver) : base(driver) { }
    }

    public static class MonitorPageExtensions
    {
        [PageObjectIdentify(UrlComapreType.Equals, "https://etwest.minamo.io/")]
        public static MonitorPage AttachMonitorPage(this IWebDriver driver)
        {
            driver.WaitForUrl(UrlComapreType.Equals, "https://etwest.minamo.io/");
            return new MonitorPage(driver);
        }
    }
}