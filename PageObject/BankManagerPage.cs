using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

public class BankManagerPage
{
    private readonly IWebDriver _driver;
    private WebDriverWait _wait;

    // Локатори для елементів сторінки
    private readonly By bankManagerLoginButton = By.CssSelector("button[ng-click='manager()']");
    private readonly By customersButton = By.CssSelector("button[ng-click='showCust()']");
    private readonly By firstNameSortButton = By.CssSelector("body > div > div > div.ng-scope > div > div.ng-scope > div > div > table > thead > tr > td:nth-child(1) > a");
    private readonly By firstNamesColumn = By.CssSelector("table td:nth-child(1)");

    public void ClickBankManagerLogin()
    {
        // Очікуємо, поки кнопка стане доступною, і натискаємо
        var button = _wait.Until(d => d.FindElement(bankManagerLoginButton));
        button.Click();
    }

    public void ClickCustomersButton()
    {
        var button = _wait.Until(d => d.FindElement(customersButton));
        button.Click();
    }

    public BankManagerPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void ClickFirstNameSortButton()
    {
        try
        {
            // Явне очікування на наявність кнопки
            var button = _wait.Until(d => d.FindElement(firstNameSortButton));
            button.Click();

            // Явне очікування для оновлення таблиці
            _wait.Until(d => d.FindElements(firstNamesColumn).Count > 0);  // Очікуємо, поки з'являться значення
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine("Timeout occurred while waiting for First Name Sort button: " + ex.Message);
            throw;
        }
    }

    public List<string> GetFirstNames()
    {
        // Перевірка, чи з'явились елементи в першому стовпці
        var firstNames = _driver.FindElements(firstNamesColumn)
            .Select(element => element.Text)
            .Where(text => text != "First Name" && !string.IsNullOrEmpty(text))  // Ігноруємо заголовок таблиці та порожні рядки
            .ToList();

        return firstNames;
    }

    public bool IsFirstNameSorted(List<string> firstNames)
    {
        // Перевірка, чи імена відсортовані від A до Z
        var sortedAscending = firstNames.SequenceEqual(firstNames.OrderBy(name => name));
        // Перевірка, чи імена відсортовані від Z до A
        var sortedDescending = firstNames.SequenceEqual(firstNames.OrderByDescending(name => name));

        return sortedAscending || sortedDescending;
    }

}
//using OpenQA.Selenium;
