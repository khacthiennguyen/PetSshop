using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;


namespace WebApp.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var viewModel = new ProductsViewModel
        {
            FoodProducts = Provider.Product.GetAllFood().ToList(),
            ClothesProducts = Provider.Product.GetAllClothes().ToList(),
            BestSellerProducts = Provider.Product.GetProducts().ToList().Where(p => p.ProductStatus == "Best Seller").ToList(),
        };
        return View(viewModel);
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
