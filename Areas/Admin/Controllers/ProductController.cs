using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : BaseController
{

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Product()
    {
        ViewBag.Category = Provider.Category.GetCategories().ToList();
        var products = Provider.Product.GetProducts().ToList();

        return View(products);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {

        var existingProduct = Provider.Product.GetProducts().FirstOrDefault(c => c.ProductId == product.ProductId);

        if (existingProduct != null)
        {

            TempData["ErrorMessage"] = "Product ID already exists. Please use a different ID.";
            return RedirectToAction("Product");
        }

        Provider.Product.Add(product);

        TempData["SuccessMessage"] = "Product added successfully.";
        return RedirectToAction("Product");
    }


    [HttpPost]
    public IActionResult UpdateProduct(Product product)
    {
        Provider.Product.Update(product);
        TempData["SuccessMessage"] = "Product updated successfully.";
        return RedirectToAction("Product");
    }

    [HttpPost]
    public IActionResult DeleteProduct(string id)
    {
        var result = Provider.Product.Delete(id);
        if (result == -1)
        {
            TempData["ErrorMessage"] = "Product could not be deleted.";
        }
        else
        {
            TempData["SuccessMessage"] = "Product deleted successfully.";
        }

        return RedirectToAction("Product");
    }

    public IActionResult Category()
    {
        var categories = Provider.Category.GetCategories().ToList();
        return View(categories);
    }

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {

        var existingCategory = Provider.Category.GetCategories().FirstOrDefault(c => c.CategoryId == category.CategoryId);

        if (existingCategory != null)
        {

            TempData["ErrorMessage"] = "Category ID already exists. Please use a different ID.";
            return RedirectToAction("Category");
        }

        Provider.Category.Add(category);

        TempData["SuccessMessage"] = "Category added successfully.";
        return RedirectToAction("Category");
    }


    [HttpPost]
    public IActionResult UpdateCategory(Category category)
    {
        Provider.Category.Update(category);
        TempData["SuccessMessage"] = "Category updated successfully.";
        return RedirectToAction("Category");
    }

    [HttpPost]
    public IActionResult DeleteCategory(string id)
    {
        var result = Provider.Category.Delete(id);
        if (result == -1)
        {
            TempData["ErrorMessage"] = "Category could not be deleted.";
        }
        else
        {
            TempData["SuccessMessage"] = "Category deleted successfully.";
        }

        return RedirectToAction("Category");
    }
}

