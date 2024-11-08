using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MemberController : BaseController
{

    public IActionResult Index()
    {
        ViewBag.member = Provider.Member.GetMembers().Where(member => member.Role == "Customer").ToList();
        return View();
    }
    public IActionResult Admin()
    {
        ViewBag.admin = Provider.Member.GetMembers().Where(member => member.Role == "Admin").ToList();
        return View();
    }

    public IActionResult DeleteMember(string id)
    {
        int ret = Provider.Member.DeleteMember(id);
        if (ret == -1)
        {
            TempData["ErrorMessage"] = "Member could not be deleted.";
        }
        else
        {
            TempData["SuccessMessage"] = "Member deleted successfully.";
        }
        return Redirect("/admin/member/index");

    }

    [HttpPost]
    public IActionResult UpdateMemberRole(string MemberId, string Role)
    {
        int ret = Provider.Member.UpdateMemberRole(MemberId, Role);
        if (ret > 0)
        {
            TempData["SuccessMessage"] = "Member updated successfully.";

        }
        else
        {

            TempData["ErrorMessage"] = "Member could not be updated.";
        }
        return Redirect("/admin/member/index");
    }



}