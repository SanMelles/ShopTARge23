using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            driver.Quit();
        }
    }
}
