using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_KT_Assignments
{
    internal class Task_4
    {
        WebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
            driver.Manage().Window.Maximize();
        }

        [Category("Regression Test Cases")]
        [Category("Valid Login Tests")]
        [Order(1)]
        [Test]
        public void Valid_LogIn_test()
        {

            driver.FindElement(By.Id("username")).SendKeys("tomsmith");
            driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            driver.FindElement(By.CssSelector("[type=\"submit\"]")).Click();

            // Verify new page URL contains expected URL
            Assert.That(driver.Url, Does.Contain("https://the-internet.herokuapp.com/secure"));

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        
            // With this improved explicit wait:
            var logoutButton = wait.Until(drv =>
            {
                var element = drv.FindElement(By.CssSelector("[href=\"/logout\"]"));
                return element.Displayed ? element : null;
            });
            Assert.That(logoutButton.Displayed, Is.True);
        }

        [Category("Regression Test Cases")]
        [Category("Invalid Login Tests")]
        [Order(2)]
        [Test]
        public void Invalid_Login_Test()
        {
            driver.FindElement(By.Id("username")).SendKeys("tomsmith");
            driver.FindElement(By.Id("password")).SendKeys("Password1234");
            driver.FindElement(By.CssSelector("[type=\"submit\"]")).Click();

            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var flashMessage = wait.Until(drv =>
            {
                var element = drv.FindElement(By.Id("flash"));
                return element.Displayed ? element : null;
            });
            wait.Until(d => flashMessage.Displayed);
            Assert.That(flashMessage.Displayed, Is.True);


        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
