namespace WebApp.Models
{
    public class Product
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public int ProductStar { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = null!;
        public string ProductStatus { get; set; } = null!;
        public string ProductImg { get; set; } = null!;
    }
}
