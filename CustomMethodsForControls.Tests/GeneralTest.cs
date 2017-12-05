using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomMethodsForControls;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace CustomMethodsForControls.Tests
{
    class GeneralTest
    {
        [SetUp]
        //opens the browser
        public void Initialize()
        {
            //Create reference for our browser
            PropertiesCollection.Driver = new ChromeDriver();
            //Navigate to Google page
            PropertiesCollection.Driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");
            Console.WriteLine("Opened URL");
        }

        [Test]
        //Executes the tests 
        public void ExecuteTest()
        {
            //Console.WriteLine("Works");
            //Initialize a LoginPageObject
            LoginPageObject pageLogin = new LoginPageObject();
            //Console.WriteLine("works");

            string[] languages = new string[] { "English", "Hindi" };
            //Console.WriteLine("Still works");

            //Login, then fill the user form(PageLogin.Login returns a EAPageObject that we can use the FillUserForm method with
            pageLogin.Login("Nafissa", "some password").FillUserForm("Ms.", "VC", "Viktor", "mage", "male", languages);

        }

        [TearDown]
        //Closes the browser/driver
        public void CleanUp()
        {
            //To close the browser after typing the value
            PropertiesCollection.Driver.Close();

            Console.WriteLine("Closed the Browser");
        }
    }
}
