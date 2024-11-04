using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "0")]
public class MemberController : BaseController
{

    public IActionResult Index()
    {
        ViewBag.member = Provider.Member.GetMembers().ToList();
        return View();
    }
    public IActionResult Admin()
    {
        return View();
    }

}