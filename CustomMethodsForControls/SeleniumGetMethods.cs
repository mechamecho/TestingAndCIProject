using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CustomMethodsForControls
{
    public static class SeleniumGetMethods
    {


        public  static string GetText(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static string GetTextFromDDL(this IWebElement element)
        {
            return new SelectElement(element).SelectedOption.Text;
            //return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text; 
        }
    }
}
