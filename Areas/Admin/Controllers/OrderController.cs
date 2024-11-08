using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;

namespace WebApp.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController: BaseController
{
    public IActionResult Index(){
        return View();
    }
}