using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ShopController : BaseController
{
    public IActionResult Index()
    {
        var AllProducts = Provider.Product.GetProducts().ToList();

        ViewBag.Category = Provider.Category.GetCategories().ToList();

        return View(AllProducts);
    }

    public IActionResult ProductDetail()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ProductDetail(string ProductId)
    {
        Product? product = Provider.Product.GetProduct(ProductId);

        if (product is null)
        {
            return Redirect("/");
        }
        ViewBag.Product = product;
        ViewBag.RelatedProducts = Provider.Product.GetProductsRelation(product.CategoryId, ProductId);
        return View();
    }


    [HttpPost]
    public IActionResult AddToLoveList(string MemberId, string ProductId)
    {
        
            int ret = Provider.LoveList.Add(MemberId, ProductId);

            if (ret > 0)
            {
                return Redirect("/home/lovelist");
            }
            else
            {
                return Redirect("/home");
            }
    }












}
