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
        LoginPageObject _pageLogin;

        //To initialize the languages array
        private string[] _languages;

        [SetUp]
        //opens the browser
        public void Initialize()
        {
            //Create reference for our browser
            PropertiesCollection.Driver = new ChromeDriver();
            //Maximizes the browsers window to full screen
            Console.WriteLine("Maximizing browser screen");
            PropertiesCollection.Driver.Manage().Window.Maximize();
            //Navigate to Google page
            Console.WriteLine(@"Navigating to URL 'http://executeautomation.com/demosite/Login.html'");
            PropertiesCollection.Driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");
            _pageLogin = new LoginPageObject();
            _languages = new[] { "English", "Hindi" };
        }

        [TestCaseSource(typeof(UsernameAndPasswordSource))]

        public void FillLoginForm_Username_UsernameTextIsCorrect(string username, string password)
        {
            Console.WriteLine($"Filling out the login form with username:{username} and password:{password}");
            _pageLogin.FillLoginForm(username, password);
            var expectedUsername = username.Length <= 10 ? username : username.Substring(0, 10);
            
            Assert.That(_pageLogin.TxtUserName.GetText(), Is.EqualTo(expectedUsername));

        }

        [TestCaseSource(typeof(UsernameAndPasswordSource))]
        public void FillLoginForm_Password_PasswordTextIsCorrect(string username, string password)
        {
            Console.WriteLine($"Filling out the login form with username: {username} and password:{password} ");
            _pageLogin.FillLoginForm(username, password);
            var expectedPassword = password.Length <= 10 ? password : password.Substring(0, 10);

            Assert.That(_pageLogin.TxtPassWord.GetText(), Is.EqualTo(expectedPassword));
        }


        public class UsernameAndPasswordSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "Nafissa", "somePassword" };
                yield return new[] { "Viktorius", "AnotherPassword with space" };
            }
        }

        [Test]
        //Executes the tests 
        public void ExecuteTest()
        {
            Console.WriteLine("Checking if the Login process and the filling out"+
                " of the user info process works over all");
            Console.WriteLine("Filling out Login form");
            //Login method returns an EAPage object.
            _pageLogin.Login("Nafissa", "some password").FillAndSubmitUserForm("Ms.", "VC", "Viktor", "mage", "male", _languages);
            Console.WriteLine("Filled out Login form, then logged in, then filled out the User Info form, and clicked the save button");
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetTitle_TitleIsCorrect(string title, string initial, string firstname, string middlename, string gender)
        {
            Console.WriteLine("Logging in");
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            eapage.FillAndSubmitUserForm(title, initial, firstname, middlename, gender, _languages);
            Console.WriteLine("Filled out the user form and submitted it");
            string actualTitle = eapage.DDLTitleID.GetTextFromDDL();
            string expectedTitle = title;
            Console.WriteLine("Checking if the title that I chose, is the currently chosen title");
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }

        public class UserFormInputs : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "Dr.", "NH", "Nafissa", "Hassan", "female" };
                yield return new[] { "Mr.", "LC", "Louis", "Caballer", "Male" };
                yield return new[] { "Ms.", "MF", "Miss", "Fortune", "FeMale" };
                yield return new[] { "Mr.", "GC", "Graves", "Cigar", "Male" };
                yield return new[] { "Mr.", "MY", "Master", "Yi", "Male" };
            }
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetInitial_initialIsCorrect(string title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(title, initial, firstname, middlename, gender, _languages);
            Console.WriteLine("Filled out the user form and submitted it");
            string actualInitial = eapage.TxtInitial.GetText();
            string expectedInitial = initial;
            Console.WriteLine("Checking if given input initials, and current saved input initials are equal");
            Assert.That(expectedInitial, Is.EqualTo(actualInitial));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetfirstName_firstNameIsCorrect(string title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(title, initial, firstname, middlename, gender, _languages);
            Console.WriteLine("Filled out the user form, and submitted it");
            var actualFirstName = eapage.TxtFirstName.GetText();
            var expectedFirstName = firstname;
            Console.WriteLine("Checking if the firstname given matches the currently saved firstname");
            Assert.That(expectedFirstName, Is.EqualTo(actualFirstName));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetMiddleName_MiddleNameIsCorrect(string title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(title, initial, firstname, middlename, gender, _languages);
            Console.WriteLine("Filled out the user form, and submitted it");
            var actualMiddleName = eapage.TxtMiddleName.GetText();
            var expectedMiddleName = middlename;
            Console.WriteLine("Checking if the middlename given matches the currently saved middlename");
            Assert.That(expectedMiddleName, Is.EqualTo(actualMiddleName));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetGender_GenderIsCorrect(string title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(title, initial, firstname, middlename, gender, _languages);
            Console.WriteLine("Filled out the user form, and submitted it");
            string actualGender;
            if (eapage.GenderFemaleChoice.Selected)
            {
                Console.WriteLine("Checking if the the female radio button was selected, if it was that will be the expected gender"+
                    "due to the bug on the page that allows users to select both buttons");
                actualGender = eapage.GenderFemaleChoice.GetText();
            }
            else
            {
                Console.WriteLine("else, if the female gender button wasn't selected, the expected gender will be male");
                actualGender = eapage.GenderMaleChoice.GetText();
            }
            var expectedGender = gender.ToLower();
            Console.WriteLine("Asserting that the currently saved gender, and the expected gender resulting from the if statement are equal");
            Assert.That(expectedGender, Is.EqualTo(actualGender));
        }

        [TestCaseSource(typeof(LanguagesInput))]

        public void FillUserForm_SetLangugae_LanguageIsCorrectEnglish(string firstlanguage, string secondlanguage)
        {
            _languages[0] = firstlanguage;
            _languages[1] = secondlanguage;
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm("Mrs.", "NH", "Nafissa", "Hassan" , "Female", _languages);
            Console.WriteLine("Filled out the User information form and submitted");
            if (_languages[0].ToLower().Equals("english"))
            {
                Console.WriteLine("Asserting that if the languages array contains english, "+
                    "the english checkbox must be selected");
                Assert.That(eapage.EnglishCheckbox.Selected, Is.True);
            }
            else
            {
                Console.WriteLine("If english is not in the languages array, then assert that the english checkbox must be unselected");
                Assert.That(eapage.EnglishCheckbox.Selected, Is.False);
            }
        }

        public class LanguagesInput : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new[] { "english", "hindi" };
                yield return new[] { "", ""};
                yield return new[] { "", "hindi" };
                yield return new[] { "english", "" };
            }
        }

        [TestCaseSource(typeof(LanguagesInput))]

        public void FillUserForm_SetLanguage_LanguageIsCorrectHindi(string firstlanguage, string secondlanguage)
        {
            _languages[0] = firstlanguage;
            _languages[1] = secondlanguage;
            EAPageObject eapage = _pageLogin.Login("Nafissa", "Password");
            eapage.FillAndSubmitUserForm("Mrs.", "NH", "Nafissa", "Hassan", "Female", _languages);

            if (_languages[1].ToLower().Equals("hindi"))
            {
                Console.WriteLine("Asserting that if the languages array contains hindi, " +
    "the hindi checkbox must be selected");
                Assert.That(eapage.HindiCheckbox.Selected, Is.True);
            }
            else
            {
                Console.WriteLine("If hindi is not in the languages array, then assert that the hindi checkbox must be unselected");
                Assert.That(eapage.HindiCheckbox.Selected, Is.False);
            }
        }





        [TearDown]
        //Closes the browser
        public void CleanUp()
        {
            //To close the browser after typing the value
            PropertiesCollection.Driver.Close();

            Console.WriteLine("Closed the Browser");
        }
    }
}
