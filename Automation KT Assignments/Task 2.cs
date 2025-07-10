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
    internal class Task_2_Tests 
       
    {
        WebDriver driver;
        WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Topic3Test1()
        {  
            driver.Navigate().GoToUrl("https://www.dummyticket.com/dummy-ticket-for-visa-application/");
            driver.Manage().Window.Maximize();

            //selecting the product
            driver.FindElement(By.Id("product_3186")).Click();
            //entering the passenger details
            driver.FindElement(By.Name("travname")).SendKeys("Assem");
            driver.FindElement(By.Name("travlastname")).SendKeys("Barakat");
            
            // selecting date from date picker
            driver.FindElement(By.Id("dob")).Click();
            SelectDate(driver, "28", "Apr", "2001");
            //selecting a gender and travel type
            driver.FindElement(By.Id("sex_1")).Click();
            driver.FindElement(By.Id("traveltype_2")).Click();



            // Fill additional required fields for Travel Details
            driver.FindElement(By.Name("fromcity")).SendKeys("Cairo");
            driver.FindElement(By.Name("tocity")).SendKeys("London");
            driver.FindElement(By.Name("departon")).Click();
            SelectDate(driver, "15", "Jan", "2026");

            driver.FindElement(By.Name("returndate")).Click();
            SelectDate(driver, "25", "Jan", "2026");

            //Select "Whatsapp" delivery
            driver.FindElement(By.CssSelector("[for=\"deliverymethod_2\"]")).Click();

            //fill in billing details
            driver.FindElement(By.Name("billname")).SendKeys("Geidea");
            driver.FindElement(By.Name("billing_phone")).SendKeys("0123456789");
            driver.FindElement(By.Name("billing_email")).SendKeys("assem@test.com");
            driver.FindElement(By.Name("billing_address_1")).SendKeys("123 Main St");
            driver.FindElement(By.Name("billing_city")).SendKeys("Cairo");
            driver.FindElement(By.Name("billing_postcode")).SendKeys("12345");
            var countryDropdown = new SelectElement(driver.FindElement(By.Id("billing_country")));
            countryDropdown.SelectByText("Egypt");
            var stateDropdown = new SelectElement(driver.FindElement(By.Name("billing_state")));
            stateDropdown.SelectByText("Cairo");

            //filling in card details and switching to the payment iframe
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("[title=\"Secure payment input frame\"]")));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("Field-numberInput")).Displayed);
            driver.FindElement(By.Id("Field-numberInput")).SendKeys("4111111111111111");
            driver.FindElement(By.Id("Field-expiryInput")).SendKeys("12/25");
            driver.FindElement(By.Id("Field-cvcInput")).SendKeys("123");
            
            // Submit the form
            driver.SwitchTo().DefaultContent(); // Switch back to the main content after filling payment details
            driver.FindElement(By.Id("place_order")).Click();
        }

        // method to select date dynamically for Test1forTopic3
        public void SelectDate(IWebDriver driver, string day, string month, string year)
        {
            // Select year
            var yearDropdown = new SelectElement(driver.FindElement(By.ClassName("ui-datepicker-year")));
            yearDropdown.SelectByText(year);

            // Select month 
            var monthDropdown = new SelectElement(driver.FindElement(By.ClassName("ui-datepicker-month")));
            monthDropdown.SelectByText(month);

            // Select day
            driver.FindElement(By.XPath($"//a[text()='{day}']")).Click();
        }

        [Test]
        public void Topic3Test2()
        {
            driver.Navigate().GoToUrl("https://aa-practice-test-automation.vercel.app/Pages/uploadFile.html");
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Upload first file with first button
            var regularFileInput = wait.Until(drv => drv.FindElement(By.Id("regularFileInput")));
            regularFileInput.SendKeys(@"C:\Users\Assem.Barakat\Pictures\Testing IMGs\1.97 MB JPG.jpg");

            // Upload second file with second button
            var fileInput = driver.FindElement(By.Id("fileInput"));

            // Make the file input visible because it's hidden through javaScript 
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display = '';", fileInput);
            fileInput.SendKeys(@"C:\Users\Assem.Barakat\Pictures\Testing IMGs\656 KB JPG.jpg");
        }

        [Test]
        public void Topic4Test1()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/add_remove_elements/");
            driver.Manage().Window.Maximize();
            //Find a button and print its tagName, attribute (like class or id),
            //and location/size on screen also check if is enabled or not : 

            // Find the "Add Element" button
            var button = driver.FindElement(By.CssSelector("[onclick=\"addElement()\"]"));

            // Print tagName
            Console.WriteLine($"Tag Name: {button.TagName}");

            // Print id and class attributes if present
            Console.WriteLine($"ID: {button.GetAttribute("id") ?? "N/A"}");
            Console.WriteLine($"Class: {button.GetAttribute("class") ?? "N/A"}");

            Console.WriteLine($"Text: {button.Text}");

            // Print location and size
            var location = button.Location;
            var size = button.Size;
            Console.WriteLine($"Location: X={location.X}, Y={location.Y}");
            Console.WriteLine($"Size: Width={size.Width}, Height={size.Height}");

            // Check if enabled
            Console.WriteLine($"Is Enabled: {button.Enabled}");
        }

        [Test]
        public void Topic4Test2()
        {
            driver.Navigate().GoToUrl("https://aa-practice-test-automation.vercel.app/Pages/checkbox_Radio.html");
            driver.Manage().Window.Maximize();
            // Find the checkbox and radio button elements  
            var ALAhly_checkbox = driver.FindElement(By.Id("Ahly"));
            var ALZamalek_checkbox = driver.FindElement(By.Id("Zamalek"));

            // Check if the checkboxes are selected 
            Console.WriteLine($"AL Ahly Checkbox Selected: {ALAhly_checkbox.Selected}");
            Console.WriteLine($"AL Zamalek Checkbox Selected: {ALZamalek_checkbox.Selected}");

        }

        [Test]
        public void Topic5Test1()
        {
            driver.Navigate().GoToUrl("https://books-pwakit.appspot.com/");
            driver.Manage().Window.Maximize();

            // Find the shadow host
            var shadowHost = driver.FindElement(By.CssSelector("book-app"));

            // Get the shadow root
            var shadowRoot = shadowHost.GetShadowRoot();

            // Find the search input inside the shadow root
            var searchInput = shadowRoot.FindElement(By.CssSelector("[aria-label=\"Search Books\"]"));

            // Enter a search term
            searchInput.SendKeys("Selenium WebDriver");
        }

        [Test]
        public void Topic5Test2()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/shadowdom");
            driver.Manage().Window.Maximize();

            // Find the shadow host
            var shadowHost = driver.FindElement(By.TagName("my-paragraph"));

            // Get the shadow root
            var shadowRoot = shadowHost.GetShadowRoot();

            // Find the text inside the shadow root
            var paragraph = shadowRoot.FindElement(By.CssSelector("[name=\"my-text\"]"));

            // Print the text of the paragraph
            Console.WriteLine($"Shadow DOM Text: {paragraph.Text}");

        }

        [Test]
        public void Topic5Test3()
        {
            driver.Navigate().GoToUrl("http://watir.com/examples/shadow_dom.html");
            driver.Manage().Window.Maximize();

            // Find the shadow host
            var shadowHost = driver.FindElement(By.Id("shadow_host"));

            // Get the shadow root
            var shadowRoot = shadowHost.GetShadowRoot();

            // Find the text inside the shadow root
            shadowRoot.FindElement(By.CssSelector("[type=\"text\"]")).SendKeys("Hello Shadow DOM!");

        }

        [Test]
        public void Topic6Test1() { 
        
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2 ");
            driver.Manage().Window.Maximize();

            // Click the "Start" button to initiate the loading
            driver.FindElement(By.CssSelector("#start button")).Click();
            // Wait for the loading to complete and the text to appear
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(By.Id("finish")).Displayed);
            // Get the text of the loaded element
            Console.WriteLine($"Loaded Text: {driver.FindElement(By.Id("finish")).Text}");

        }

        [Test]
        public void Topic7Test1()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            driver.Manage().Window.Maximize();

            // Click the "Click Here" link to open a new window 
            driver.FindElement(By.LinkText("Click Here")).Click();

            // Switch to the new window
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            // Wait for the new window to load and get its title
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.Title.Contains("New Window"));
            Console.WriteLine($"New Window Title: {driver.Title}");
            //switch back to the original window
            driver.SwitchTo().Window(driver.WindowHandles.First());

        }

        [Test]
        public void Topic8Test1()
        {
            // Part 1: Switch to frame and type text
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/iframe");
            driver.Manage().Window.Maximize();

            // Switch to the iframe
            driver.SwitchTo().Frame("mce_0_ifr");
            // Type text into the editor
            var editor = driver.FindElement(By.Id("tinymce"));
            editor.Clear();
            editor.SendKeys("Hello from Selenium!");
            // Switch back to default content
            driver.SwitchTo().DefaultContent();

          
        }

        [Test]
        public void Topic8Test2() {
            // Part 2: Print all text on the 4 frames in nested_frames
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            driver.Manage().Window.Maximize();

            // Switch to top frame
            driver.SwitchTo().Frame("frame-top");

            // Switch to left frame and print text
            driver.SwitchTo().Frame("frame-left");
            Console.WriteLine("Left Frame Text: " + driver.FindElement(By.TagName("body")).Text);

            // Switch back to top, then to middle frame and print text
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame("frame-middle");
            Console.WriteLine("Middle Frame Text: " + driver.FindElement(By.Id("content")).Text);

            // Switch back to top, then to right frame and print text
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame("frame-right");
            Console.WriteLine("Right Frame Text: " + driver.FindElement(By.TagName("body")).Text);

            // Switch back to default content, then to bottom frame and print text
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame("frame-bottom");
            Console.WriteLine("Bottom Frame Text: " + driver.FindElement(By.TagName("body")).Text);

            // Switch back to default content at the end
            driver.SwitchTo().DefaultContent();


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