using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using NUnit.Framework;

namespace GetWorkMusic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Using Chrome
            IWebDriver driver = new ChromeDriver();
            Random rnd = new Random();
            String[] listing = {"Chill Nation", "Cercle", "TheFatRat", "ChilloutDeer", "MrSuicideSheep"};

            //Wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WebDriverWait waittest = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //WebDriverWait wait1000 = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000));

            //Navigating to Youtube
            driver.Navigate().GoToUrl("https://www.youtube.com/");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("https://www.youtube.com/"));            

            //Select randomized channel
            String channel = listing[rnd.Next(0, listing.Length)];
            driver.FindElement(By.CssSelector("[name*=search_query]")).SendKeys(channel);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(By.CssSelector("[name*=search_query]"), channel));            
            driver.FindElement(By.CssSelector("[name*=search_query]")).SendKeys(Keys.Enter);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("yt-formatted-string[title*='" + channel + "'] > a")));

            //Go into channel
            driver.FindElement(By.CssSelector("yt-formatted-string[title*='" + channel +"'] > a")).Click();

            //Go to playlist
            try
            {
                waittest.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("paper-button[aria-label*='Got It']")));
                driver.FindElement(By.CssSelector("paper-button[aria-label*='Got It']")).Click();
            }
            catch (Exception) { }
            driver.FindElement(By.CssSelector("[id=tabsContent] paper-tab:nth-of-type(3)")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[id=tabsContent] paper-tab:nth-of-type(3)[aria-selected*=true]")));

            try
            {
                waittest.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("paper-button[aria-label*='Got It']")));
                driver.FindElement(By.CssSelector("paper-button[aria-label*='Got It']")).Click();
            }
            catch (Exception) { }

            //Select playlist
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("[id=tabsContent] paper-tab:nth-of-type(3)[aria-selected*=true]")));
            driver.FindElement(By.CssSelector("img[id=\"img\"][class=\"style-scope yt-img-shadow\"][alt=\"\"][width=\"210\"]")).Click();
        }
    }
}
