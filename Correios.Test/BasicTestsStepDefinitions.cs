using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using TechTalk.SpecFlow;

namespace Correios.Test
{
    [Binding]
    public class BasicTestsStepDefinitions
    {
        private IWebDriver Driver { get; set; }
        private String Url = "https://www.correios.com.br/";

        [BeforeScenario]
        public void BeforeScenario()
        {
            var Url = "https://www.correios.com.br/";
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            Driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
            Driver.Navigate().GoToUrl(Url);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Quit();
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

            var mensagemResultado = Driver.FindElement(By.Id("mensagem-resultado")).Text;
            Assert.AreEqual("Não há dados a serem exibidos", mensagemResultado);

            var mensagemResultadoAlerta = Driver.FindElement(By.XPath("//*[@id=\"mensagem-resultado-alerta\"]/h6")).Text;
            Assert.AreEqual(message, mensagemResultadoAlerta);


        }


        [Then(@"the message of success ""([^""]*)"" is show")]
        public void ThenTheMessageOfSuccessIsShow(string message)
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

            var resultado = Driver.FindElement(By.XPath("//*[@id=\"resultado-DNEC\"]/tbody/tr/td[1]")).Text;
            Assert.AreEqual(message, resultado);
        }

        [Given(@"the user search for code ""([^""]*)""")]
        public void GivenTheUserSearchForCode(string code)
        {
            var codeInput = Driver.FindElement(By.Id("objetos"));
            var codeInputButton = Driver.FindElement(By.XPath("//form/div[@class=\"campo\"]/input[@id=\"objetos\"]/parent::*/button"));
            codeInput.SendKeys(code);
            codeInputButton.Click();
        }

        [Then(@"the message of code ""([^""]*)"" is show")]
        public void ThenTheMessageOfCodeIsShow(string message)
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
            var ultimaTrilha = trilhas.ElementAt(trilhas.Count - 1).Text;

            Assert.AreEqual("Rastreamento", ultimaTrilha);

            var resultado = Driver.FindElement(By.XPath("//*[@id=\"titulo-pagina\"]/h3")).Text;
            Assert.AreEqual(message, resultado);
        }

        [Then(@"click on homepage")]
        public void ThenClickOnHomepage()
        {
            Driver.Navigate().GoToUrl(Url);
        }



    }
}
