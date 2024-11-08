using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class InvoiceController : BaseController
{
    public IActionResult Index()
    {
        string memberId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        // Lấy danh sách hóa đơn từ repository
        var invoices = Provider.Invoice.GetOrdersByMemberId(memberId);
        // Truyền danh sách vào View
        return View(invoices);
    }
}