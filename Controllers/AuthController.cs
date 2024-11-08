using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;


public class AuthController : BaseController
{


    IEmailSender sender;

    public AuthController(IEmailSender sender)
    {
        this.sender = sender;
    }

    public IActionResult Forgot()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Forgot(string email)
    {

        // string token = Guid.NewGuid().ToString().Replace("-", string.Empty);
        string? token = Provider.Member.GenerateToken(email);
        if (string.IsNullOrEmpty(token))
        {
            return View();
        }
        string? url = Url.Action("confirm", "auth", new { token }, protocol: Request.Scheme);
        if (string.IsNullOrEmpty(url))
        {
            return View();
        }
        string content = $"Please click <a href=\"{url}\">link</a> a to Change Password";
        await sender.SendEmailAsync(email, "Forgot Password", content);
        TempData["Msg"] = $"Please Check your email:{email}";
        return Redirect("/");
    }
    public IActionResult Confirm(string token)
    {
        return View();
    }
    [HttpPost]
    public IActionResult Confirm(string token, string password)
    {
        int ret = Provider.Member.ChangePassword(password, token);
        if (ret > 0)
        {
            TempData["Msg"] = "Change Password Success";
            return Redirect("/auth");
        }
        ModelState.AddModelError("Error", "Change Password failed");
        return View();
    }

    public IActionResult Success()
    {
        return View();
    }

    [HttpGet]
    public IActionResult FacebookSignin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("FacebookResponse")
        };
        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookResponse(string? returnUrl = null)
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (result.Succeeded && result.Principal != null)
        {
            var claims = result.Principal.Claims;

            var member = new Member
            {
                MemberId = claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value ?? "",
                Name = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "",
                SurName = claims.FirstOrDefault(p => p.Type == ClaimTypes.Surname)?.Value ?? "",
                GivenName = claims.FirstOrDefault(p => p.Type == ClaimTypes.GivenName)?.Value ?? "",
                Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "",
                Role = claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value ?? "",
                Password = "defaultPassword" // Bạn có thể thay đổi giá trị mật khẩu mặc định hoặc xử lý mật khẩu khác
            };

            // Kiểm tra nếu email có giá trị hợp lệ trước khi thêm vào database
            if (!string.IsNullOrEmpty(member.Email))
            {
                Provider.Member.Add(member);
                return Redirect(returnUrl ?? "/home");
            }
            else
            {
                // Trường hợp lỗi không có email
                TempData["Error"] = "Unable to retrieve user email from Facebook login.";
                return Redirect("/auth/error");
            }
        }

        return Redirect("/auth/error");
    }


    public IActionResult GoogleLogin()
    {
        string? uri = Url.Action("googleresponse", "auth", null, protocol: Request.Scheme);
        if (string.IsNullOrEmpty(uri))
        {
            return Redirect("/auth/error");
        }
        AuthenticationProperties properties = new AuthenticationProperties
        {
            RedirectUri = uri
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result != null && result.Principal != null)
        {
            var claims = result.Principal.Claims;

            var member = new Member
            {
                MemberId = claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value ?? "",
                Name = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "",
                SurName = claims.FirstOrDefault(p => p.Type == ClaimTypes.Surname)?.Value ?? "",
                GivenName = claims.FirstOrDefault(p => p.Type == ClaimTypes.GivenName)?.Value ?? "",
                Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "",
                Role = claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value ?? "",
                Password = "asfdasfasd" // Mật khẩu giả định, cần cập nhật trong môi trường thực
            };

            // Kiểm tra xem member có hợp lệ không trước khi thêm vào cơ sở dữ liệu
            if (!string.IsNullOrEmpty(member.Email))
            {
                Provider.Member.Add(member);
            }
            else
            {
                // Xử lý nếu thông tin không hợp lệ (chẳng hạn chuyển hướng đến trang lỗi)
                return Redirect("/auth/error");
            }
        }

        return Redirect("/home");
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginModel obj)

    {
        var member = Provider.Member.FindMemberByEmail(obj.Eml);
        if (member != null && member.Password == obj.Pwd)
        {
            TempData["MemberId"] = member.MemberId;
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, member.MemberId),
                new Claim(ClaimTypes.Name, member.Name),
                new Claim(ClaimTypes.Email, member.Email),
                new Claim(ClaimTypes.GivenName, member.GivenName),
                new Claim(ClaimTypes.Surname,member.SurName),
                new Claim(ClaimTypes.Role, member.Role)
                };

            // Tạo ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Đăng nhập với ClaimsPrincipal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = obj.Rem });

            return Redirect("/home");
        }
        ModelState.AddModelError("Error", "Login Fail");
        return View(obj);

    }



    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(Member obj)
    {
        var member = Provider.Member.FindMemberByEmail(obj.Email.ToLower());
        if (member == null)
        {
            obj.MemberId = Guid.NewGuid().ToString().Replace("-", string.Empty);
            obj.Name = obj.GivenName + " " + obj.SurName;
            obj.Role = Role.Customer.ToString();
            int ret = Provider.Member.Add(obj);
            if (ret > 0)
            {
                return Redirect("/auth/login");
            }
        }

        ModelState.AddModelError("Error", "Email Exist");
        return View(obj);
    }



    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/home");
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }



    public IActionResult Denied()
    {
        return View();
    }



}
