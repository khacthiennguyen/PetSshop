//=VNP14623806&vnp_CardType=ATM&vnp_OrderInfo=Thanh+toan+don+hang+4c70c381cb944b55be693f8cc0e2c30f+voi+so+tien+100000&vnp_PayDate=20241021103101&vnp_ResponseCode=00&vnp_TmnCode=G0NOWU5F&vnp_TransactionNo=14623806&vnp_TransactionStatus=00&vnp_TxnRef=4c70c381cb944b55be693f8cc0e2c30f&vnp_SecureHash=33576cb98a34bf7f6f25d3db7d2582a2ad3dec3a76befd15d85bd3346e1cc87d11dec2336baa39881bfdb9c48a7cd9e9159771e343456b4f506efc07c16b3261

using Microsoft.AspNetCore.Mvc;

namespace WebApp.Services;

public class VnPaymentReponse
{
    [BindProperty(Name = "vnp_Amount")]
    public int Amount { get; set; }

    [BindProperty(Name = "vnp_BankCode")]
    public string BankCode { get; set; } = null!;

    [BindProperty(Name = "vnp_BankTranNo")]
    public string BankTranNo { get; set; } = null!;


    [BindProperty(Name = "vnp_CardType")]

    public string CardType { get; set; } = null!;
    [BindProperty(Name = "vnp_OrderInfo")]

    public string OrderInfo { get; set; } = null!;
    [BindProperty(Name = "vnp_PayDate")]

    public string PayDate { get; set; } = null!;
    [BindProperty(Name = "vnp_ResponseCode")]

    public string ResponseCode { get; set; } = null!;
    [BindProperty(Name = "vnp_TmnCode")]

    public string TmnCode { get; set; } = null!;
    [BindProperty(Name = "vnp_TransactionNo")]

    public string TransactionNo { get; set; } = null!;
    [BindProperty(Name = "vnp_TransactionStatus")]

    public string TransactionStatus { get; set; } = null!;
    [BindProperty(Name = "vnp_TxnRef")]

    public string TxnRef { get; set; } = null!;
    [BindProperty(Name = "vnp_SecureHash")]

    public string SecureHash { get; set; } = null!;


}

