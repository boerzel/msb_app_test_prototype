using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace MsbAppTests
{
    public enum Platform
    {
        NodeWebkit,
        Android,
        iOS
    }


    public class WebDriverFactory
    {
        private static IWebDriver CreateNodeWebkitDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument(@"nwapp=C:\Ranorex\AUT\MSB-App-win64\msb-app.exe");

            var service =
                ChromeDriverService.CreateDefaultService(
                    @"C:\products\five\development\quality\testautomation\test\nwjs-sdk-v0.40.2-win-x64");
            service.SuppressInitialDiagnosticInformation = true;

            return new ChromeDriver(service, options);
        }

        private static IWebDriver CreateIOSDriver()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPad Entwickler Pool");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Ranorex\AUT\iOS\MSBApp.ipa");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");

            return new IOSDriver<IWebElement>(
                new HttpCommandExecutor(new Uri("http://localhost:4723/wd/hub"), TimeSpan.FromMinutes(5), true),
                capabilities);
        }

        private static IWebDriver CreateAndroidDriver()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "HT4CLJT00667");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "7.1.1");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Ranorex\AUT\android\app-debug.apk");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);

            //capabilities.AddAdditionalCapability("appPackage", "com.mobisys.msbclientdev");
            //capabilities.AddAdditionalCapability("appActivity", "com.mobisys.msbclientdev.MainActivity");

            //capabilities.AddAdditionalCapability("appWaitPackage", "com.mobisys.msbclientdev");
            //capabilities.AddAdditionalCapability("appWaitActivity", "com.mobisys.msbclientdev.MainActivity");

            return new AndroidDriver<IWebElement>(
                new HttpCommandExecutor(new Uri("http://localhost:4723/wd/hub"), TimeSpan.FromMinutes(5)),
                capabilities);
        }

        public static IWebDriver Create(Platform platform)
        {
            IWebDriver driver;

            switch (platform)
            {
                case Platform.NodeWebkit:
                    driver = WebDriverFactory.CreateNodeWebkitDriver();
                    break;
                case Platform.Android:
                    driver = WebDriverFactory.CreateAndroidDriver();
                    break;
                case Platform.iOS:
                    driver = WebDriverFactory.CreateIOSDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform), platform, null);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return driver;
        }
    }
}