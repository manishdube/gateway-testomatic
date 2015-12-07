using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace msrb.org.queryreportperf.utilities
{
    class SelectHelper
    {
        public void Select(IWebDriver driver, string dropDownId, string dropDownItemName)
        {
            IWebElement dropDownListBox = driver.FindElement(By.Id(dropDownId));
            var clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(dropDownItemName);
        }

        public bool isSelected(IWebDriver driver, string dropDownId, string dropDownItemName)
        {
            IWebElement dropDownListBox = driver.FindElement(By.Id(dropDownId));
            var clickThis = new SelectElement(dropDownListBox);
            foreach (var item in clickThis.Options)
            {
                if (item.Text == dropDownItemName)
                {
                    clickThis.SelectByText(dropDownItemName);
                    return true;
                }
            }

            return false;
        }

        public void Select(IWebDriver driver, By _by, string dropDownItemName)
        {
            IWebElement dropDownListBox = driver.FindElement(_by);
            var clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(dropDownItemName);
        }

        public void Select(IWebDriver driver, By _by, int index)
        {
            IWebElement dropDownListBox = driver.FindElement(_by);
            var clickThis = new SelectElement(dropDownListBox);
            (clickThis.Options[index]).Click();
        }
    }
}
