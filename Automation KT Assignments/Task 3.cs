using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Automation_KT_Assignments
{
    internal class Task_3
    {
        WebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void Positive_LogIn_test()
        {
            
            driver.FindElement(By.Id("username")).SendKeys("student");
            driver.FindElement(By.Id("password")).SendKeys("Password123");
            driver.FindElement(By.Id("submit")).Click();

            // Verify new page URL contains expected substring
            Assert.That(driver.Url, Does.Contain("practicetestautomation.com/logged-in-successfully/"));

            // Verify new page contains expected text
            var PageText = driver.FindElement(By.TagName("body")).Text;
            Assert.That(PageText, Does.Contain("Congratulations").Or.Contain("successfully logged in"));

            // Verify Log out button is displayed
            var logoutButton = driver.FindElement(By.CssSelector("[class=\"wp-block-button__link has-text-color has-background has-very-dark-gray-background-color\"]"));
            Assert.That(logoutButton.Displayed, Is.True);
        }
        [Test]
        public void Negative_username_test()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Id("username")).SendKeys("incorrectUser");
            driver.FindElement(By.Id("password")).SendKeys("Password123");
            driver.FindElement(By.Id("submit")).Click();

            var errorMessage = driver.FindElement(By.Id("error"));
            wait.Until(d => errorMessage.Displayed);
            Assert.That(errorMessage.Displayed, Is.True);
            Assert.That(errorMessage.Text, Is.EqualTo("Your username is invalid!"));
        }
        [Test]
        public void Negative_password_test()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.FindElement(By.Id("username")).SendKeys("student");
            driver.FindElement(By.Id("password")).SendKeys("incorrectPassword");
            driver.FindElement(By.Id("submit")).Click();
            var errorMessage = driver.FindElement(By.Id("error"));
            wait.Until(d => errorMessage.Displayed);
            Assert.That(errorMessage.Displayed, Is.True);
            Assert.That(errorMessage.Text, Is.EqualTo("Your password is invalid!"));

        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
