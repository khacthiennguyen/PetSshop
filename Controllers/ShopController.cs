using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class ShopController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}