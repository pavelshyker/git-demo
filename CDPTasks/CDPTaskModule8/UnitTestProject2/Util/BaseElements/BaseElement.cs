using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestWebProject.Webdriver
{
    public class BaseElement : IWebElement
    {
        protected IWebDriver driver = Browser.GetDriver();

        public string Name { get; }

        public By Locator { get; }

        protected IWebElement Element { get; set; }

        public BaseElement(By locator, string name)
        {
            this.Locator = locator;
            this.Name = name == "" ? this.GetText() : name;
        }

        public BaseElement(By locator)
        {
            this.Locator = locator;
        }

        public string GetText()
        {
            this.WaitForIsVisible();
            return this.Element.Text;
        }

        public IWebElement GetElement()
        {
            try
            {
                this.Element = driver.FindElement(this.Locator);
            }
            catch (Exception)
            {

                throw;
            }
            return this.Element;
        }

        public void WaitForIsVisible()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementIsVisible(this.Locator));
        }


        public IWebElement FindElement(By @by)
        {
            this.WaitForIsVisible();
            return driver.FindElement(by);
        }

        public ReadOnlyCollection <IWebElement> FindElements (By @by)
        {
            this.WaitForIsVisible();
            return driver.FindElements(by);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void SendKeys(string text)
        {
            this.WaitForIsVisible();
            driver.FindElement(this.Locator).SendKeys(text);
        }

        public void Submit()
        {
            throw new System.NotImplementedException();
        }

        public void Click()
        {
            this.WaitForIsVisible();
            driver.FindElement(this.Locator).Click();
        }

        public string GetAttribute(string attributeName)
        {
            this.WaitForIsVisible();
            return driver.FindElement(this.Locator).GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            throw new System.NotImplementedException();
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool Displayed
        {
            get
            {
                this.WaitForIsVisible();
                return driver.FindElement(this.Locator).Displayed;
            }
        }

        public string Text
        {
            get
            {
                this.WaitForIsVisible();
                return driver.FindElement(this.Locator).Text;
            }
        }

        public void JsClick()
        {
            this.WaitForIsVisible();
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            executor.ExecuteScript("arguments[0].click();", this.GetElement());
        }

        public void JsHighlight()
        {
            this.WaitForIsVisible();
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            executor.ExecuteScript("arguments[0].style.backgroundColor = '" + "yellow" + "'", this.GetElement());
        }

        public string TagName { get; }
        public bool Enabled { get; }
        public bool Selected { get; }
        public Point Location { get; }
        public Size Size { get; }
    }
}