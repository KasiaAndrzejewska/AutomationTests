using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace RecruitmentTask.Pages
{
    public class HomePage
    {
        public IWebDriver Driver { get; set; }

        public IWebElement HamburgerMenu {
            get
            {
                return this.Driver.FindElement(By.CssSelector(".menu-btn"));
            }

        }

        public IWebElement ChangeLanguageButton
        { 
            get 
            {
                return this.Driver.FindElement(By.Id("nexer-lang-switcher-desktop"));
            }
        }

        public ReadOnlyCollection<IWebElement> MenuElements
        {
            get
            {
                return this.Driver.FindElements(By.CssSelector(".nav-link"));
            } 
        }

        public HomePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(TestConstants.WebUri);
            Driver.Manage().Window.Maximize();
        }

        public void OpenHamburgerMenu()
        {
            var menuButton = this.HamburgerMenu;
            menuButton.Click();
        }

        public void ChangeLanguage()
        {
            var languageButton = this.ChangeLanguageButton;
            languageButton.Click();
        }
    }
}
