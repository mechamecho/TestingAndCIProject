using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CustomMethodsForControls
{
    public static class SeleniumSetMethods
    {
        public static void FormatInput(this IWebElement element, string value)
        {
            
            string UpperCaseData = value.ToUpper();
            element.EnterText(UpperCaseData[0] + UpperCaseData.Substring(1).ToLower());
        }

        /// <summary>
        /// Extended method for entering the text in the control
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void EnterText(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        /// <summary>
        /// Click into a button, Checkbox, option etc
        /// </summary>
        /// <param name="element"></param>
        public static void Clicks(this IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        ///Selecting a drop down control
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SelectDropDown(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }
    }
}
