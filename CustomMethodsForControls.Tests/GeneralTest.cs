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

        //To initialize the languages array
        string[] languages;

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
            languages = new string[] { "English", "Hindi" };


        }

        [TestCaseSource(typeof(UsernameAndPasswordSource))]

        public void FillLoginForm_Username_UsernameTextIsCorrect(string Username, string Password)
        {
            Console.WriteLine("Filling out the login form");
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
            Console.WriteLine("Filling out the login form");
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
            Console.WriteLine("Checking if the Login process and the filling out"+"" +
                " of the user info process work over all");
            Console.WriteLine("Filling out Login form");
            //Login method returns an EAPage object.
            pageLogin.Login("Nafissa", "some password").FillAndSubmitUserForm("Ms.", "VC", "Viktor", "mage", "male", languages);
            Console.WriteLine("Filled out Login form, then logged in, then filled out the User Info form, and clicked the save button");
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetTitle_TitleIsCorrect(string Title, string initial, string firstname, string middlename, string gender)
        {
            Console.WriteLine("Logging in");
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            eapage.FillAndSubmitUserForm(Title, initial, firstname, middlename, gender, languages);
            Console.WriteLine("Filled out the user form and submitted it");
            string actualTitle = eapage.DDLTitleID.GetTextFromDDL();
            string expectedTitle = Title;
            Console.WriteLine("Checking if the title that I chose, is the currently chosen title");
            Assert.That(expectedTitle, Is.EqualTo(actualTitle));
        }

        public class UserFormInputs : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new string[] { "Ms.", "NH", "Nafissa", "Hassan", "female" };
                yield return new string[] { "Mr.", "LC", "Louis", "Caballer", "Male" };
                yield return new string[] { "Ms.", "MF", "Miss", "Fortune", "FeMale" };
                yield return new string[] { "Mr.", "GC", "Graves", "Cigar", "Male" };
                yield return new string[] { "Mr.", "MY", "Master", "Yi", "Male" };
            }
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetInitial_initialIsCorrect(string Title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(Title, initial, firstname, middlename, gender, languages);
            Console.WriteLine("Filled out the user form and submitted it");
            string actualInitial = eapage.TxtInitial.GetText();
            string expectedInitial = initial;
            Console.WriteLine("Checking if given input initials, and current saved input initials are equal");
            Assert.That(expectedInitial, Is.EqualTo(actualInitial));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetfirstName_firstNameIsCorrect(string Title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(Title, initial, firstname, middlename, gender, languages);
            Console.WriteLine("Filled out the user form, and submitted it");
            string actualFirstName = eapage.TxtFirstName.GetText();
            string expectedFirstName = firstname;
            Console.WriteLine("Checking if the firstname given matches the currently saved firstname");
            Assert.That(expectedFirstName, Is.EqualTo(actualFirstName));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetMiddleName_MiddleNameIsCorrect(string Title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(Title, initial, firstname, middlename, gender, languages);
            Console.WriteLine("Filled out the user form, and submitted it");
            string actualMiddleName = eapage.TxtMiddleName.GetText();
            string expectedMiddleName = middlename;
            Console.WriteLine("Checking if the middlename given matches the currently saved middlename");
            Assert.That(expectedMiddleName, Is.EqualTo(actualMiddleName));
        }

        [TestCaseSource(typeof(UserFormInputs))]

        public void FillUserForm_SetGender_GenderIsCorrect(string Title, string initial, string firstname, string middlename, string gender)
        {
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm(Title, initial, firstname, middlename, gender, languages);
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
            string expectedGender = gender.ToLower();
            Console.WriteLine("Asserting that the currently saved gender, and the expected gender resulting from the if statement are equal");
            Assert.That(expectedGender, Is.EqualTo(actualGender));
        }

        [TestCaseSource(typeof(LanguagesInput))]

        public void FillUserForm_SetLangugae_LanguageIsCorrectEnglish(string firstlanguage, string secondlanguage)
        {
            languages[0] = firstlanguage;
            languages[1] = secondlanguage;
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            Console.WriteLine("Logged in");
            eapage.FillAndSubmitUserForm("Mrs.", "NH", "Nafissa", "Hassan" , "Female", languages);
            Console.WriteLine("Filled out the User information form and submitted");
            if (languages[0].ToLower().Equals("english"))
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
                yield return new string[] { "english", "hindi" };
                yield return new string[] { "", ""};
                yield return new string[] { "", "hindi" };
                yield return new string[] { "english", "" };
            }
        }

        [TestCaseSource(typeof(LanguagesInput))]

        public void FillUserForm_SetLanguage_LanguageIsCorrectHindi(string firstlanguage, string secondlanguage)
        {
            languages[0] = firstlanguage;
            languages[1] = secondlanguage;
            EAPageObject eapage = pageLogin.Login("Nafissa", "Password");
            eapage.FillAndSubmitUserForm("Mrs.", "NH", "Nafissa", "Hassan", "Female", languages);

            if (languages[1].ToLower().Equals("hindi"))
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
