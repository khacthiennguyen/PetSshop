using System.Net;
using System.Web;
using Microsoft.Extensions.Options;
using WebApp.Models;

namespace WebApp.Services;

public class VnPaymentService
{
    VnPayment payment;
    IHttpContextAccessor accessor;
    public VnPaymentService(IOptions<VnPayment> options, IHttpContextAccessor accessor)
    {
        payment = options.Value;
        this.accessor = accessor;

    }
    public string ToUrl(Invoice obj)
    {
        IDictionary<string, string> dict = new SortedDictionary<string, string>{
            {"vnp_Amount", (obj.Amount * 100).ToString("#.")},
            {"vnp_Command", payment.Command},
            {"vnp_CreateDate", obj.InvoiceDate.ToString("yyyyMMddHHmmss")},
            {"vnp_CurrCode", payment.CurrCode},
            // {"vnp_IpAddr", accessor.HttpContext!.Connection.RemoteIpAddress!.AddressFamily.ToString()},
            {"vnp_IpAddr", "127.0.0.1"},
            {"vnp_Locale", payment.Locale},
            {"vnp_OrderInfo", $"Thanh toan don hang {obj.InvoiceId} voi so tien {obj.Amount}"},
            {"vnp_OrderType", payment.OrderType},
            {"vnp_ReturnUrl", payment.ReturnUrl},
            {"vnp_TmnCode", payment.TmnCode},
            {"vnp_TxnRef", obj.InvoiceId.ToString()},
            {"vnp_Version", payment.Version},
        };

        string query = string.Join("&", dict.Select(p => $"{p.Key}={WebUtility.UrlEncode(p.Value)}"));
        string secureHash = Helper.HmacSha512(query, payment.HashSecret);
        string url = $"{payment.BaseUrl}?{query}&vnp_SecureHash={secureHash}";
        System.Console.WriteLine(url);
        return url;
    }
}