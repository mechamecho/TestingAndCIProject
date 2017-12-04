using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace CustomMethodsForControls
{
    class Program
    {
        

        static void Main(string[] args)
        {

        }

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

            string[] languages = new string[] { "English", ""};
            //Console.WriteLine("Still works");

            //Login, then fill the user form(PageLogin.Login returns a EAPageObject that we can use the FillUserForm method with
            pageLogin.Login("Nafissa", "some password").FillUserForm("Ms.", "VC", "Vayne", "adc", "female", languages);
            
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
