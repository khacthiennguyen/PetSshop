namespace WebApp.Models;

public class Product
{
    public string ProductId {get;set;} = null!;
    public string ProductName {get;set;} = null!;
    public decimal ProductPrice {get;set;} 
    public int ProductStar {get;set;} 
    public string ProductDesciption {get;set;} = null!;
    public string ProductStatus {get;set;} = null!;
    public int ProductQuantity {get;set;} 
    public string CategoryId {get;set;} = null!;
}