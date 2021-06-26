using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace PageObject.Pages
{
    public class SamplePage : PageBase
    {
        public TextBoxDriver textBoxName => ById("textBoxName").Wait();
        public DateDriver date => ById("date").Wait();
        public DropDownListDriver dropDownFruit => ById("dropDownFruit").Wait();
        public DropDownListDriver dropDownFruitValue => ById("dropDownFruitValue").Wait();
        public RadioButtonDriver radioMan => ById("radioMan").Wait();
        public RadioButtonDriver radioWoman => ById("radioWoman").Wait();
        public CheckBoxDriver checkBoxCellPhone => ById("checkBoxCellPhone").Wait();
        public CheckBoxDriver checkBoxCar => ById("checkBoxCar").Wait();
        public CheckBoxDriver checkBoxCottage => ById("checkBoxCottage").Wait();
        public TextAreaDriver textareaFreeans => ById("textareaFreeans").Wait();
        public ButtonDriver inputJS => ById("inputJS").Wait();
        public AnchorDriver codeer => ById("codeer").Wait();
        public SideBar SideBar => ByTagName("app-menu").Wait();

        public SamplePage(IWebDriver driver) : base(driver) { }
    }

    public static class SamplePageExtensions
    {
        [PageObjectIdentify(UrlComapreType.Equals, "https://etwest.minamo.io/sample.html")]
        public static SamplePage AttachSamplePage(this IWebDriver driver)
        {
            driver.WaitForUrl(UrlComapreType.Equals, "https://etwest.minamo.io/sample.html");
            return new SamplePage(driver);
        }
    }
}