using System.Data;
using Dapper;

namespace WebApp.Models;

public class ProductRepository : BaseRepository
{
    public ProductRepository(IDbConnection connection) : base(connection)
    {
    }

    // Lấy danh sách tất cả sản phẩm
    public IEnumerable<Product> GetProducts()
    {
        return connection.Query<Product>("SELECT * FROM Product");
    }


    // Thêm sản phẩm mới
    public int Add(Product obj)
    {
        // Kiểm tra sản phẩm đã tồn tại hay chưa
        var existingProduct = connection.QueryFirstOrDefault<Product>(
            "SELECT * FROM Product WHERE ProductId = @ProductId", new { ProductId = obj.ProductId });

        if (existingProduct != null)
        {
            return -1; // Sản phẩm đã tồn tại
        }

        // Thêm sản phẩm qua stored procedure
        return connection.Execute("AddProduct", obj, commandType: CommandType.StoredProcedure);
    }

    // Xóa sản phẩm
    public int Delete(string id)
    {
        return connection.Execute("DELETE FROM Product WHERE ProductId = @id", new { id });
    }

    // Cập nhật sản phẩm
    public int Update(Product obj)
    {
        // Kiểm tra sản phẩm tồn tại
        var existingProduct = connection.QueryFirstOrDefault<Product>(
            "SELECT * FROM Product WHERE ProductId = @ProductId", new { ProductId = obj.ProductId });

        if (existingProduct == null)
        {
            return -1; // Sản phẩm không tồn tại
        }

        // Cập nhật thông tin sản phẩm
        return connection.Execute(
            "UPDATE Product SET ProductName = @ProductName, ProductStar = @ProductStar, ProductPrice = @ProductPrice, " +
            "ProductDescription = @ProductDescription, ProductStatus = @ProductStatus, ProductQuantity = @ProductQuantity, " +
            "CategoryId = @CategoryId, ProductImg = @ProductImg WHERE ProductId = @ProductId",
            obj);
    }


    public IEnumerable<Product> GetAllClothes()
    {
        return connection.Query<Product>("SELECT * FROM Product WHERE CategoryId IN ('CAT-CLO', 'DOG-CLO')");
    }

    public IEnumerable<Product> GetAllFood()
    {
        return connection.Query<Product>("SELECT * FROM Product WHERE CategoryId IN ('CATFOOD', 'DOGFOOD')");
    }



    public Product? GetProduct(string ProductId)
    {
        string sql = "SELECT * from Product WHERE ProductId = @ProductId";
        return connection.QuerySingleOrDefault<Product>(sql, new { ProductId });

    }

    //get product relation , sản phẩm liên quan
    public IEnumerable<Product> GetProductsRelation(string CategoryId, string ProductId)
    {
        string sql = "SELECT * FROM Product WHERE CategoryId = @CategoryId AND ProductId<> @ProductId";
        return connection.Query<Product>(sql, new { CategoryId, ProductId });
    }





}
