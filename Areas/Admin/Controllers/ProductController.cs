using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]

public class ProductController : BaseController
{

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Product()
    {
        return View();
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

