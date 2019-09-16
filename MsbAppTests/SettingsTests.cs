using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;

namespace MsbAppTests
{
    public class SettingsTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var platform = TestContext.Parameters["platform"];
            TestContext.Out.WriteLine($"Platform: {platform}");

            _driver = WebDriverFactory.Create(Platform.NodeWebkit);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void Test1()
        {
            var header = _driver.FindElement(By.Id("header"));
            Assert.AreEqual(header.Text, "Default", "Wrong profile name");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

            var visible = wait.Until(driver => driver.FindElement(By.Id("header")).Displayed);
            Assert.IsTrue(visible, "Header not visible");

            var exist = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("header")));
            //new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("header")));

            var header1 = wait.Until(c => c.FindElement(By.Id("header")));
            Assert.AreEqual(header1.Text, "Default", "Wrong profile name");

            var header2 = _driver.Wait().ForElement(By.Id("header")).ToExist();
            Assert.AreEqual(header2.Text, "Default", "Wrong profile name");


            var userInput = _driver.FindElement(By.Id("userInput"));
            userInput.SendKeys("mobentw");
            var user = userInput.GetAttribute("value");
            Assert.AreEqual(user, "mobentw", "Wrong user");

            var passwordInput = _driver.FindElement(By.Id("passwordInput"));
            passwordInput.SendKeys("sapmobis");
            var password = passwordInput.GetAttribute("value");
            Assert.AreEqual(password, "sapmobis", "Wrong password");

            var loginApplicationInput = _driver.FindElement(By.Id("loginApplicationInput"));
            loginApplicationInput.SendKeys("QS1");
            var application = loginApplicationInput.GetAttribute("value");
            Assert.AreEqual(application, "QS1", "Wrong application");


            var profileButton = _driver.FindElement(By.Id("header"));
            //profileButton.Click();

            var settingsButton = _driver.FindElement(By.Id("settingsButton"));
            settingsButton.Click();


            var cancelButton = _driver.FindElement(By.Id("cancelButton"));
            _driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", cancelButton);
            Thread.Sleep(1000);
            cancelButton.Click();

            //Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var header = _driver.FindElement(By.Id("header"));
            Assert.AreEqual(header.Text, "Default", "Wrong profile name");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

            var visible = wait.Until(driver => driver.FindElement(By.Id("header")).Displayed);
            Assert.IsTrue(visible, "Header not visible");

            var header1 = wait.Until(c => c.FindElement(By.Id("header")));
            Assert.AreEqual(header1.Text, "Default", "Wrong profile name");
        }
    }
}