using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace PageObject.Pages
{
    public class LogPage : PageBase
    {
        public DropDownListDriver Logs => ById("logs").Wait();
        public TextAreaDriver Content => ById("log-content").Wait();
        public SideBar SideBar => ByTagName("app-menu").Wait();

        public LogPage(IWebDriver driver) : base(driver) { }
    }

    public static class LogPageExtensions
    {
        [PageObjectIdentify(UrlComapreType.Equals, "https://etwest.minamo.io/history.html")]
        public static LogPage AttachLogPage(this IWebDriver driver)
        {
            driver.WaitForUrl(UrlComapreType.Equals, "https://etwest.minamo.io/history.html");
            return new LogPage(driver);
        }
    }
}