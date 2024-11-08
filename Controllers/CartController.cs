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
                return Redirect("/");
            }
            return View(Provider.Cart.GetCarts(code));
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
                return Redirect("/");
            }
            return View(Provider.Cart.GetCarts(code));

        }

        [HttpPost]
        public IActionResult Checkout(Invoice obj)
        {
            string? code = Request.Cookies[cart];
            if (string.IsNullOrEmpty(code))
            {
                return Redirect("/");
            }

            Random random = new Random();
            obj.InvoiceId = random.NextInt64(99999999, 9999999999999);
            obj.CartCode = code;
            obj.InvoiceDate = DateTime.Now;
            int ret = Provider.Invoice.Add(obj);
            if (ret > 0)
            {
                obj.Amount = Provider.Invoice.GetAmountInvoice(obj.InvoiceId) * 1000; //Dùng VND
                string url = vnPayment.ToUrl(obj);
                return Redirect(url);

            }
            return Redirect("/cart/error");

        }
    }



}