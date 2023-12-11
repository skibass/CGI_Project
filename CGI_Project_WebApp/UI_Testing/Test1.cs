namespace UI_Testing;
using CGI_Project_WebApp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[TestClass]
public class Test1
{
    private IWebDriver driver;
    private string baseUrl = "https://localhost:7033/";
    private string username = "denzel@hotmail.nl";
    private string password = "Welkom01234!";
    [TestMethod]
    public void TestMethod1()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(baseUrl);
        AttemptLogin((ChromeDriver)driver);
        
    }

    public void AttemptLogin(ChromeDriver driver){
        IWebElement loginBtn = driver.FindElement(By.Id("login-button"));
        loginBtn.Click();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
        IWebElement usernameInput = driver.FindElement(By.Id("username"));
        IWebElement passwordInput = driver.FindElement(By.Id("password"));
        IWebElement submitButton = driver.FindElement(By.XPath("/html/body/div/main/section/div/div[2]/div/form/div[3]/button"));
        
        usernameInput.SendKeys(username);
        passwordInput.SendKeys(password);
        submitButton.Click();
        
    }
}