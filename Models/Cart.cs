namespace WebApp.Models;

public class Cart
{
    public string CartCode { get; set; } = null!;

    public string ProductId { get; set; } = null!;
    public short ProductQuantity { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImg { get; set; } = null!;


}
