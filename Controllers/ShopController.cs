using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ShopController : BaseController
{
    public IActionResult Index()
    {
        var  AllProducts = Provider.Product.GetProducts().ToList();

        return View(AllProducts);
    }
}