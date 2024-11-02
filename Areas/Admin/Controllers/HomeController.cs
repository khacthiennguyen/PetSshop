using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApp.Controllers;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : BaseController

{
    public IActionResult Index()
    {
        return View();
    }

}