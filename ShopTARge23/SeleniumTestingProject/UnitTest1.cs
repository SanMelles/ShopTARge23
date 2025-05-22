using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTests
{
    [TestFixture]
    public class ErrTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            // Looge WebDriver (Chrome)
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test1_SuccessfulTest()
        {
            driver.Navigate().GoToUrl("https://www.err.ee/");
            wait.Until(d => d.Title.Contains("ERR"));

            // Kasutame NUnit Asserti õigesti
            Assert.IsTrue(driver.Title.Contains("ERR"), "ERR.ee pealkiri ei sisalda ERR.");
        }

        [Test]
        public void Test2_FailingTest()
        {
            // Siin on tahtlik viga
            Assert.IsTrue(driver.Title.Contains("NotExistingTitle"), "Test ebaõnnestus, kuna pealkiri ei sisaldanud NotExistingTitle.");
        }

        [TearDown]
        public void TearDown()
        {
            // Veenduge, et WebDriver on korralikult sulgatud
            if (driver != null)
            {
                driver.Quit(); // See on samuti oluline, sest driver.Close() ei sulge kõiki brauseri ressursse
                driver.Dispose(); // Vabastame kõik WebDriveri ressursid
            }
        }
    }
}
