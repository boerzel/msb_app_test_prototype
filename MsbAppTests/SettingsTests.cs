using System;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using OpenQA.Selenium.Appium.Service;

namespace MsbAppTests
{
    public class SettingsTests
    {
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        private WindowsDriver<IWebElement> _driver;
        private AppiumLocalService _appiumService;

        [SetUp]
        public void Setup()
        {
            var appFilePath = TestContext.Parameters["appFilePath"];

            _appiumService = AppiumLocalService.BuildDefaultService();
            _appiumService.Start();

            // platform specified in .runsettings.xml
            var platform = Enum.Parse<Platform>(TestContext.Parameters["platform"]);
            TestContext.Out.WriteLine($"Platform: {platform}");
            _driver = new WebDriverFactory(
                    "http://localhost:4723/wd/hub",
                    "WindowsPC",
                    appFilePath)
                .Create(platform);

            //BringWindowToFront();
        }

        private void BringWindowToFront()
        {
            var x = _driver.WindowHandles.First().Substring(2);
            SetForegroundWindow(int.Parse(x, NumberStyles.HexNumber));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _appiumService.Dispose();
        }

        [Test]
        public void Test1()
        {
            //var header = _driver.FindElementByAccessibilityId(("ButtonAddNewSapSystem"));
            //var name = header.GetAttribute("Name");
            //TestContext.Out.WriteLine($"Profile: {name}");
            //Assert.AreEqual(name, "SAP-System erstellen", "Wrong profile name");

            //var sysButton = _driver.FindElement(By.XPath("//Text[@AutomationId='SID' and @Name='MT7']/.."));
            var sysButton = _driver.FindElement(By.XPath("//Text[@AutomationId='SID' and @Name='MT7']/parent::*"));
            //sysButton.Click();
            var description = sysButton.FindElement(By.XPath("//Text[@AutomationId='SystemDescription']"))
                .GetAttribute("Name");

            var helpButton = sysButton.FindElement(By.XPath("//Button[@AutomationId='EditSapSystemButton']"));
            var removeSapSystemButton =
                sysButton.FindElement(By.XPath("//Button[@AutomationId='RemoveSapSystemButton']"));

            //var t1 = _driver
            //    .FindElementsByAccessibilityId("SID");

            //var t2 = t1.Single(e => e.GetAttribute("Name") == "MT7");

            var text = _driver.FindElement(By.XPath("//Text[@AutomationId='SID' and @Name='MT7']"));

            //var t3 = text.FindElement(By.XPath("./.."));

            //var sysButton = _driver
            //    .FindElementsByAccessibilityId("SID")
            //    .Single(e => e.GetAttribute("Name") == "MT7")
            //    .FindElement(By.XPath("./.."));

            var systems = _driver.FindElements(By.XPath("//Text[@AutomationId='SID']"));
            var sids = systems.Select(e => e.GetAttribute("Name")).ToList();

            var systemButtons = _driver.FindElementsByAccessibilityId(("SapSystemButton"));
            foreach (var system in systemButtons)
            {
                //var button = system.FindElement(By.XPath("//Text[@AutomationId='SID']/.."));

                //    var test = system.FindElement(By.XPath("//Text[@AutomationId='SID']")).GetAttribute("Name");
                //    TestContext.Out.WriteLine($"System: {test}");
            }


            //var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            //wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

            //var visible = wait.Until(driver => driver.FindElement(By.Id("header")).Displayed);
            //Assert.IsTrue(visible, "Header not visible");

            //var exist = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("header")));
            ////new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("header")));

            //var header1 = wait.Until(c => c.FindElement(By.Id("header")));
            //Assert.AreEqual(header1.Text, "Default", "Wrong profile name");

            //var header2 = _driver.Wait().ForElement(By.Id("header")).ToExist();
            //Assert.AreEqual(header2.Text, "Default", "Wrong profile name");


            //var userInput = _driver.FindElement(By.Id("userInput"));
            //userInput.SendKeys("mobentw");
            //var user = userInput.GetAttribute("value");
            //Assert.AreEqual(user, "mobentw", "Wrong user");
            //TestContext.Out.WriteLine($"User: {user}");

            //var passwordInput = _driver.FindElement(By.Id("passwordInput"));
            //passwordInput.SendKeys("sapmobis");
            //var password = passwordInput.GetAttribute("value");
            //Assert.AreEqual(password, "sapmobis", "Wrong password");
            //TestContext.Out.WriteLine($"Password: {password}");

            //var loginApplicationInput = _driver.FindElement(By.Id("loginApplicationInput"));
            //loginApplicationInput.SendKeys("QS1");
            //var application = loginApplicationInput.GetAttribute("value");
            //Assert.AreEqual(application, "QS1", "Wrong application");
            //TestContext.Out.WriteLine($"Application: {application}");


            //var profileButton = _driver.FindElement(By.Id("header"));
            ////profileButton.Click();

            //var settingsButton = _driver.FindElement(By.Id("settingsButton"));
            //settingsButton.Click();


            //var cancelButton = _driver.FindElement(By.Id("cancelButton"));
            //_driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", cancelButton);
            //Thread.Sleep(1000);
            //cancelButton.Click();

            //Assert.Pass();
        }

        //[Test]
        //public void Test2()
        //{
        //    var header = _driver.FindElement(By.Id("header"));
        //    Assert.AreEqual(header.Text, "Default", "Wrong profile name");

        //    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        //    wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

        //    var visible = wait.Until(driver => driver.FindElement(By.Id("header")).Displayed);
        //    Assert.IsTrue(visible, "Header not visible");

        //    var header1 = wait.Until(c => c.FindElement(By.Id("header")));
        //    Assert.AreEqual(header1.Text, "Default", "Wrong profile name");
        //}
    }
}