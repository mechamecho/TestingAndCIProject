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
            FillLoginForm(UserName, Password);
            return ClickLoginBtn();
            
        }

        public void FillLoginForm(string UserName, string Password)
        {
            DataEntry dataentry = new DataEntry();

            //UserName
            //Extension Methods
            TxtUserName.EnterText(UserName);

            //Password
            TxtPassWord.EnterText(Password);

            //To get the username, we entered
            string UsernameText = TxtUserName.GetText();

            //To get the password we entered
            string PasswordText = TxtPassWord.GetText();

            dataentry.PopulateLoginTable(UsernameText, PasswordText);
        }

        public EAPageObject ClickLoginBtn()
        {
            //Click button
            BtnLogin.Submit();
            return new EAPageObject();
        }
    }
}
