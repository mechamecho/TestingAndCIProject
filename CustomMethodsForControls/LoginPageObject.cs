using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace CustomMethodsForControls
{
    public class LoginPageObject
    {
         

        public LoginPageObject()
        {

            PageFactory.InitElements(PropertiesCollection.Driver, this);
            

        }



        [FindsBy(How=How.Name, Using ="UserName")]
        public IWebElement TxtUserName { get; set; }

        [FindsBy(How = How.Name, Using = "Password")]
        public IWebElement TxtPassWord { get; set; }

        [FindsBy(How = How.Name, Using = "Login")]
        public IWebElement BtnLogin { get; set; }

        public EAPageObject Login(string UserName, string Password)
        {
            //had console outputs here for debugginh
            DataEntry dataentry = new DataEntry();
            //Here too

            //UserName
            //Extension Methods
            TxtUserName.EnterText(UserName);

            //Password
            TxtPassWord.EnterText(Password);



            //To get the username, we entered
            string UsernameText = TxtUserName.GetText();



            //To get the password we entered
            string PasswordText=TxtPassWord.GetText();




            dataentry.PopulateLoginTable(UsernameText, PasswordText);
            

            //Click button
            BtnLogin.Submit();

            //To Navotagte to the HomePage, returns an instance of it
            return new EAPageObject();
            
        }
    }
}
