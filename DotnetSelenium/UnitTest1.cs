using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTesting
{
    [TestFixture]
    public class AccountsTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void VerifyAccountsAreDisplayed()
        {
            _driver.Navigate().GoToUrl("http://localhost:5160/Accounts");

            // Verify that the accounts are displayed
            Assert.IsTrue(_driver.FindElement(By.CssSelector(".btn.btn-danger")).Displayed);
        }

        [Test]
        public void DeleteAccount()
        {
            _driver.Navigate().GoToUrl("http://localhost:5160/Accounts");

            // Locate the delete button for the specific account
            IWebElement deleteButton = _wait.Until(driver => 
                driver.FindElement(By.CssSelector(".btn.btn-danger")));

            // Click the delete button
            deleteButton.Click();
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
