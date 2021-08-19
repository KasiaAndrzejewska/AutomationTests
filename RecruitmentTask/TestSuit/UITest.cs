using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RecruitmentTask.Pages;
using System;
using Xunit;

namespace RecruitmentTask.TestSuit
{
    public class UITest : IDisposable
    {
        private IWebDriver driver;
        public UITest()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public void Dispose()
        {
            driver.Close();
        }


        [Fact]
        public void CheckLanguageOfMenuIsCorrectlyChangedFromEnglishToSwedish()
        {
            var home = new HomePage(driver);
            home.OpenHomePage();
            home.MenuElements[0].Text.ToString().Should().Equals("STRATEGY");
            home.MenuElements[1].Text.ToString().Should().Equals("TECH");
            home.MenuElements[2].Text.ToString().Should().Equals("COMMUNICATION");

            home.OpenHamburgerMenu();
            home.ChangeLanguage();

            home.MenuElements[0].Text.ToString().Should().Equals("STRATEGI");
            home.MenuElements[1].Text.ToString().Should().Equals("TEKNIK");
            home.MenuElements[2].Text.ToString().Should().Equals("KOMMUNIKATION");
        }
    }
}
