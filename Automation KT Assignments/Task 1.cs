using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace Automation_KT_Assignments
{
    public class Task_1_Tests
    {
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Topic1_Test()
        {
            
            driver.Navigate().GoToUrl("https://www.example.com");

            var url = driver.Url;
            var title = driver.Title;
            var htmlsrccode = driver.PageSource;
            var pageid = driver.CurrentWindowHandle;
            
            Console.WriteLine("Current URL :" + url );
            Console.WriteLine("Current title :" + title);
            Console.WriteLine("Current HTML source code :" + htmlsrccode);
            Console.WriteLine("Current uniqe id for the page :" + pageid);

            driver.Navigate().GoToUrl("https://www.selenium.dev");

            driver.Navigate().Back();
            driver.Navigate().Forward();    
            driver.Navigate().Refresh();

            var windowsize = driver.Manage().Window.Size;
            var windowposition = driver.Manage().Window.Position;

            Console.WriteLine("Current window size :" + windowsize);
            Console.WriteLine("Current window position :" + windowposition);

            driver.Manage().Window.Size = new System.Drawing.Size(1024, 768);
            driver.Manage().Window.Position = new System.Drawing.Point(200, 150);

            driver.Manage().Window.Maximize();
            driver.Manage().Window.FullScreen();

            driver.Close();
            
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/");
            driver.Quit();
        }


        [Test]
        public void Topic2_Test()
        {
            driver.Navigate().GoToUrl("https://www.facebook.com/r.php?entry_point=login");

            By firstname = By.Name("firstname");
            driver.FindElement(firstname).SendKeys("Assem");


            By lastname = By.Name("lastname");
            driver.FindElement(lastname).SendKeys("Barakat");

            By DOBDay = By.Id("day");
            driver.FindElement(DOBDay).SendKeys("28");
            By DOBMonth = By.Id("month");
            driver.FindElement(DOBMonth).SendKeys("Apr");
            By DOBYear = By.Id("year");
            driver.FindElement(DOBYear).SendKeys("2001");

            By email = By.XPath("//input[@name='reg_email__']");
            driver.FindElement(email).SendKeys("assem.test@gmail.com");

            By password = By.CssSelector("input[name='reg_passwd__']");
            driver.FindElement(password).SendKeys("test@1234");

        }
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}