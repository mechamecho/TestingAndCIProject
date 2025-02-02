﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace CustomMethodsForControls
{
    public class EAPageObject
    {
        DataEntry dataentry = new DataEntry();


        public EAPageObject()
        {
            //To initialize the instance of this class(Page) and populate it with the given elements(properties of this class)
            PageFactory.InitElements(PropertiesCollection.Driver, this);
            RadioElements = new List<IWebElement>();
        }


        //To identify the title element
        [FindsBy(How = How.Id, Using = "TitleId")]
        public IWebElement DDLTitleID { get; set; }

        [FindsBy(How = How.Name, Using = "Initial")]
        public IWebElement TxtInitial { get; set; }

        [FindsBy(How = How.Name, Using = "FirstName")]
        public IWebElement TxtFirstName { get; set; }

        [FindsBy(How = How.Name, Using = "MiddleName")]
        public IWebElement TxtMiddleName { get; set; }

        [FindsBy(How = How.Name, Using = "Save")]
        public IWebElement BtnSave { get; set; }

        [FindsBy(How = How.Name, Using = "Male")]
        public IWebElement GenderMaleChoice { get; set; }

        [FindsBy(How = How.Name, Using = "Female")]
        public IWebElement GenderFemaleChoice { get; set; }

        public List<IWebElement> RadioElements;

        [FindsBy(How = How.Name, Using = "english")]
        public IWebElement EnglishCheckbox { get; set; }

        [FindsBy(How = How.Name, Using = "Hindi")]
        public IWebElement HindiCheckbox { get; set; }



        //To select the title
        public void SelectTitle(string title)
        {

            switch (title.ToLower())
            {
                case ("mr."):
                    DDLTitleID.SelectDropDown("Mr.");
                    break;

                case ("ms."):
                    DDLTitleID.SelectDropDown("Ms.");
                    break;
            }


        }

        //To click the Gender RadioButton
        public void ClickGender(string gender)
        {
            switch (gender.ToLower())
            {
                case ("male"):
                    GenderMaleChoice.Click();
                    break;

                case ("female"):
                    GenderFemaleChoice.Click();
                    break;
            }

            AddSelectedRadioElements();
        }

        private void AddSelectedRadioElements()
        {
            if (GenderFemaleChoice.Selected)
                RadioElements.Add(GenderFemaleChoice);
            if (GenderMaleChoice.Selected)
                RadioElements.Add(GenderMaleChoice);
        }

        //To click the Languages checkboxes
        public void ClickLanguage(string[] languages)
        {
            //To uncheck the default English checkbox
            if (!languages[0].ToLower().Equals("english"))
            {
                EnglishCheckbox.Clicks();
            }
            if (languages[1].ToLower().Equals("hindi"))
            {

                HindiCheckbox.Clicks();
            }

        }

        public void FillUserForm(string title, string initial, string firstName, string middleName, string gender, string[] languages)
        {
            //The extended methods works directly on the IWebElement
            SelectTitle(title);
            TxtInitial.EnterText(initial);
            TxtFirstName.FormatInput(firstName);
            TxtMiddleName.FormatInput(middleName);
            ClickGender(gender);
            ClickLanguage(languages);

            AddValuesToDatabase(languages);
        }

        public void FillAndSubmitUserForm(string title, string initial, string firstName, string middleName, string gender, string[] languages)
        {
            FillUserForm(title, initial, firstName, middleName, gender, languages);
            SubmitForm();
        }

        public void AddValuesToDatabase(string[] languages)
        {
            string TitleText = DDLTitleID.GetTextFromDDL();
            string Initial = TxtInitial.GetText();
            string Firstname = TxtFirstName.GetText();
            string Middlename = TxtMiddleName.GetText();
            string Gender = "Male";
            if (GenderMaleChoice.Selected)
            {
                Gender = GenderMaleChoice.GetText();
            }
            if (GenderFemaleChoice.Selected)
            {
                Gender = GenderFemaleChoice.GetText();
            }

            if (EnglishCheckbox.Selected)
            {
                languages[0] = "English";
            }
            else
            {
                languages[0] = "not chosen";
            }


            if (HindiCheckbox.Selected)
            {
                languages[1] = "Hindi";

            }


            dataentry.PopulateUserFormTable(TitleText, Initial, Firstname, Middlename, Gender, languages);
        }

        public void SubmitForm()
        {
            BtnSave.Clicks();

        }

    }
}
