using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.ElementSelectors;

namespace Behavioral.Automation.Playwright.Pages;

class MainPage : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;
    
    //Login
    public ElementSelector Username = new() { IdSelector = "username"};
    public ElementSelector Password = new() { IdSelector = "password"};
    public ElementSelector LoginButton = new() { IdSelector = "login-button"};
    
    //Items
    public ElementSelector AddBackpackToCart = new() { IdSelector = "add-to-cart-sauce-labs-backpack" };
    
    //ShoppingCart
    public ElementSelector ShoppingCartBadge = new() { Selector = "//span[@class='shopping_cart_badge']"};
}