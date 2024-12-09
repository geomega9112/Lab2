using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[Binding]
public class BankManagerSteps
{
    private IWebDriver _driver;
    private readonly BankManagerPage _bankManagerPage;

    public BankManagerSteps()
    {
        _driver = new ChromeDriver();
        _bankManagerPage = new BankManagerPage(_driver);
    }

    [Given(@"I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        _driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
    }

    [When(@"I click on the ""(.*)"" button")]
    public void WhenIClickOnTheButton(string button)
    {
        if (button == "Bank Manager Login")
            _bankManagerPage.ClickBankManagerLogin();
        else if (button == "Customers")
            _bankManagerPage.ClickCustomersButton();
    }

    [Then(@"the customer names should be sorted by first name when I click the ""(.*)"" sort button")]
    public void ThenTheCustomerNamesShouldBeSortedByFirstName(string button)
    {
        if (button == "First Name")
        {
            // Натискаємо на кнопку сортування
            _bankManagerPage.ClickFirstNameSortButton();

            // Отримуємо імена після першого сортування
            var firstNamesFirstSort = _bankManagerPage.GetFirstNames();
            Console.WriteLine("First Sort: " + string.Join(", ", firstNamesFirstSort)); // Вивести список імен після першого сортування

            // Перевіряємо, чи вони правильно відсортовані
            Assert.IsTrue(_bankManagerPage.IsFirstNameSorted(firstNamesFirstSort), "First names are not sorted correctly after first click!");

            // Натискаємо ще раз на кнопку сортування
            _bankManagerPage.ClickFirstNameSortButton();

            // Отримуємо імена після другого сортування
            var firstNamesSecondSort = _bankManagerPage.GetFirstNames();
            Console.WriteLine("Second Sort: " + string.Join(", ", firstNamesSecondSort)); // Вивести список імен після другого сортування

            // Перевіряємо, чи вони правильно відсортовані
            Assert.IsTrue(_bankManagerPage.IsFirstNameSorted(firstNamesSecondSort), "First names are not sorted correctly after second click!");

            // Перевірка, чи перші імена змінилися після другого натискання
            Assert.AreNotEqual(firstNamesFirstSort[0], firstNamesSecondSort[0], "The first name did not change after second click!");
        }
    }

    //[AfterScenario]
    //public void CleanUp()
    //{
    //    _driver.Quit();
    //}
}
    //[AfterScenario]
    //public void CleanUp()
    //{
    //    _driver.Quit();
    //}


