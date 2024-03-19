using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SwabLabAutomation
{
    [TestClass]
    public class SwagLab
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void Setup()
        {
            webDriver = WebDriverFactory.CreateChromeDriver();
        }

        [TestCleanup]
        public void Teardown()
        {
            WebDriverFactory.QuitWebDriver(webDriver);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://www.saucedemo.com/v1/";
            webDriver.Navigate().GoToUrl(url);
            webDriver.Manage().Window.Maximize();
            webDriver.FindElement(By.Name("user-name")).SendKeys("standard_user");
            webDriver.FindElement(By.Name("password")).SendKeys("secret_sauce");
            webDriver.FindElement(By.CssSelector("#login-button")).Click();
            Assert.AreEqual("https://www.saucedemo.com/v1/inventory.html", webDriver.Url);
            webDriver.FindElement(By.XPath("//div[text()='Sauce Labs Backpack']")).Click();
        }

        [TestMethod]
        public void TestMethod2()
        {
            string url = "https://www.saucedemo.com/";
            webDriver.Navigate().GoToUrl(url);
            Assert.AreEqual("Swag Labs", webDriver.Title, "Title does not match Swag Labs");
            webDriver.Navigate().GoToUrl("https://www.facebook.com");
            webDriver.Navigate().Back();
            string currentUrl = webDriver.Url;
            Console.WriteLine("Current URL: " + currentUrl);
            webDriver.Navigate().Refresh();
        }

        [TestMethod]
        public void TestMethod3()
        {
            string url = "https://www.saucedemo.com/";
            webDriver.Navigate().GoToUrl(url);
            string currentUrl = webDriver.Url;
            if (currentUrl == url)
            {
                Console.WriteLine("URL verification PASSED.");
            }
            else
            {
                Console.WriteLine("URL verification FAILED.");
            }
            webDriver.FindElement(By.Name("user-name")).SendKeys("standard_user");
            webDriver.FindElement(By.Name("password")).SendKeys("secret_sauce");
            webDriver.FindElement(By.CssSelector("#login-button")).Click();
            if (webDriver.Url.Contains("inventory.html"))
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Login unsuccessful.");
            }
        }
    }

    public static class WebDriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            return new ChromeDriver();
        }

        public static void QuitWebDriver(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}