using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class CartController : BaseController
    {


        VnPaymentService vnPayment;

        public CartController(VnPaymentService vnPayment)
        {
            this.vnPayment = vnPayment;
        }

        const string cart = "cart";


        public IActionResult Index()
        {
            string? code = Request.Cookies[cart];
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/"); // Nếu không có CartCode, chuyển hướng về trang chủ
            }

            // Lấy MemberId từ claim
            var memberId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return Redirect("/login"); // Chuyển hướng đến trang đăng nhập nếu không có MemberId
            }

            // Lấy giỏ hàng với CartCode và MemberId
            var cartItems = Provider.Cart.GetCarts(code, memberId);
            return View(cartItems);
        }


        [HttpPost]
        public IActionResult Edit([FromBody] Cart obj)
        {
            try
            {
                string? code = Request.Cookies[cart];
                if (string.IsNullOrEmpty(code))
                {
                    return BadRequest("Cart code is missing.");
                }
                obj.CartCode = code;
                var result = Provider.Cart.Edit(obj);
                return Json(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in Cart/Edit: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }


        // [HttpPost]
        // public IActionResult Add(Cart obj)
        // {
        //     // Cookies
        //     string? code = Request.Cookies[cart];
        //     if (string.IsNullOrEmpty(code))
        //     {
        //         code = Guid.NewGuid().ToString().Replace("-", "");
        //         Response.Cookies.Append(cart, code);
        //     }
        //     obj.CartCode = code;

        //     int ret = Provider.Cart.Add(obj);
        //     if (ret > 0)
        //     {
        //         return Redirect("/cart");
        //     }
        //     return Redirect("/cart/error");
        // }

        [HttpPost]
        public IActionResult Add(Cart obj)
        {
            // Cookies
            string? code = Request.Cookies[cart];
            if (string.IsNullOrEmpty(code))
            {
                code = Guid.NewGuid().ToString().Replace("-", "");
                Response.Cookies.Append(cart, code);
            }
            obj.CartCode = code;

            // Lấy userId từ NameIdentifier claim
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                obj.MemberId = userId;
            }
            else
            {
                return Redirect("/cart/error"); // Trả về trang lỗi nếu không có userId
            }

            int ret = Provider.Cart.Add(obj);
            if (ret > 0)
            {
                return Redirect("/cart");
            }
            return Redirect("/cart/error");
        }

        public IActionResult Delete(string id)
        {
            string? code = Request.Cookies[cart];
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");
            }

            int ret = Provider.Cart.Delete(code, id);
            if (ret > 0)
            {
                return Redirect("/cart");
            }
            return Redirect("/cart/error");
        }

        public IActionResult Checkout()
        {
            string? code = Request.Cookies[cart];
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/"); // Nếu không có CartCode trong cookie, chuyển hướng về trang chủ
            }

            // Lấy MemberId từ claim
            var memberId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return Redirect("/login"); // Nếu không có MemberId, chuyển hướng đến trang đăng nhập
            }

            // Lấy giỏ hàng với CartCode và MemberId
            var cartItems = Provider.Cart.GetCarts(code, memberId);
            decimal totalAmount = cartItems.Sum(p => p.ProductPrice * p.ProductQuantity);
            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }


        [HttpPost]
        public IActionResult Checkout(Invoice obj)
        {
            // Kiểm tra nếu không có CartCode trong cookie
            string? code = Request.Cookies["cart"];
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");  // Nếu không có giỏ hàng, chuyển hướng về trang chủ
            }

            // Lấy thông tin MemberId từ người dùng đang đăng nhập
            string memberId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                // Nếu không có MemberId, có thể điều hướng đến trang đăng nhập hoặc thông báo lỗi
                return Redirect("/login");
            }

            // Tạo InvoiceId ngẫu nhiên
            Random random = new Random();
            obj.InvoiceId = random.NextInt64(9999999, 99999999999999);
            obj.CartCode = code;
            obj.InvoiceDate = DateTime.Now;
            obj.MemberId = memberId;  // Gán MemberId
            obj.Status = "Pending";  // Gán trạng thái, ví dụ: "Processing"

            // Thực hiện thêm hóa đơn vào cơ sở dữ liệu
            int ret = Provider.Invoice.Add(obj);
            if (ret > 0)
            {
                return Redirect("/cart/payment");  // Thành công, chuyển hướng đến trang thành công
            }

            return Redirect("/cart/error");  // Thất bại, chuyển hướng đến trang lỗi
        }

        public IActionResult Payment()
        {
            return View();
        }

    }



}