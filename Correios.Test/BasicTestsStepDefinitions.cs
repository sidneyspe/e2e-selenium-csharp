using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Correios.Test
{
    [Binding]
    public class BasicTestsStepDefinitions
    {
        private IWebDriver Driver { get; set; }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var url = "https://www.correios.com.br/";
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            Driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Driver.Quit();
        }

        [Then(@"the homepage is loaded")]
        public void ThenTheHomepageIsLoaded()
        {
            var logoCorreios = Driver.FindElement(By.XPath("//*[@class=\"logo-correios\"]"));
            Assert.IsNotNull(logoCorreios);
        }

        [Given(@"the user search for CEP ""([^""]*)""")]
        public void GivenTheUserSearchForCEP(string cep)
        {
            var cepInput = Driver.FindElement(By.Id("relaxation"));
            var cepInputButton = Driver.FindElement(By.XPath("//form/div[@class=\"campo\"]/input[@id=\"relaxation\"]/parent::*/button"));
            cepInput.SendKeys(cep);
            cepInputButton.Click();
        }

        [Then(@"the message ""([^""]*)"" is show")]
        public void ThenTheMessageIsShow(string message)
        {
            var currentHandle = Driver.CurrentWindowHandle;
            var handles = Driver.WindowHandles;

            foreach (var actual in handles)
            {
                if (!actual.Equals(currentHandle))
                {
                    //Switch to the opened tab
                    Driver.SwitchTo().Window(actual);
                    //opening the URL saved.
                }
            }
            var trilha = Driver.FindElement(By.Id("trilha"));
            var trilhas = trilha.FindElements(By.TagName("a"));
            var ultimaTrilha = trilhas.ElementAt(trilhas.Count - 1).FindElement(By.TagName("b")).Text;

            Assert.AreEqual("Busca por Endereço ou CEP", ultimaTrilha);

        }

    }
}
