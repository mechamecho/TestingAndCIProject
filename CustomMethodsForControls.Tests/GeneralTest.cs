using System;
using System.Collections;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace CustomMethodsForControls.Tests
{
    [TestFixture]
    public class GeneralTest
    {
        //Initialize a LoginPageObject
        LoginPageObject pageLogin;

        [SetUp]
        //opens the browser
        public void Initialize()
        {
            //Create reference for our browser
            PropertiesCollection.Driver = new ChromeDriver();
            //Maximizes the browsers window to full screen
            PropertiesCollection.Driver.Manage().Window.Maximize();
            //Navigate to Google page
            PropertiesCollection.Driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");
            Console.WriteLine("Opened URL");
            pageLogin = new LoginPageObject();
        }

        [TestCaseSource(typeof(UsernameAndPasswordSource))]

        public void FillLoginForm_Username_UsernameTextIsCorrect(string Username, string Password)
        {
            pageLogin.FillLoginForm(Username, Password);
            string expectedUsername;
            if (Username.Length <= 10)
            {
                expectedUsername = Username;
            }
            else
            {
                expectedUsername = Username.Substring(0, 10);
            }
            
            Assert.That(pageLogin.TxtUserName.GetText(), Is.EqualTo(expectedUsername));

        }

        [TestCaseSource(typeof(UsernameAndPasswordSource))]
        public void FillLoginForm_Password_PasswordTextIsCorrect(string Username, string Password)
        {
            pageLogin.FillLoginForm(Username, Password);
            string expectedPassword;
            if (Password.Length <= 10)
            {
                expectedPassword = Password;
            }
            else
            {
                expectedPassword = Password.Substring(0, 10);
            }

            Assert.That(pageLogin.TxtPassWord.GetText(), Is.EqualTo(expectedPassword));
        }


        public class UsernameAndPasswordSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new string[] { "Nafissa", "somePassword" };
                yield return new string[] { "Viktorius", "AnotherPassword with space" };
            }
        }

        [Test]
        //Executes the tests 
        public void ExecuteTest()
        {
            string[] languages = new string[] { "English", "Hindi" };

            //Login method returns an EAPage object.
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
