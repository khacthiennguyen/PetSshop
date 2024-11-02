using System.Data;
using Dapper;

namespace WebApp.Models;

public class ProductRepository : BaseRepository
{
    public ProductRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Product> GetProducts()
    {
        return connection.Query<Product>("SELECT * FROM Category");
    }

    public int Add(Product obj)
    {
        var existingProduct = connection.QueryFirstOrDefault<Product>(
            "SELECT * FROM Product WHERE ProdcutId = @ProdcutId", new { obj.ProductId});

        if (existingProduct != null)
        {
            return -1;
        }

        return connection.Execute("AddProduct", obj, commandType: CommandType.StoredProcedure);

    }


    public int Delete(string id)
    {

        return connection.Execute("DELETE FROM Product WHERE ProductId = @id", new { id });

    }

    public int Update(Product obj)
    {
        var existingCategory = connection.QueryFirstOrDefault<Category>(
            "SELECT * FROM Product WHERE ProductId = @ProductId", new { obj.ProductId });

        if (existingCategory == null)
        {
            return -1;
        }

        return connection.Execute(
            "UPDATE Category SET CategoryName = @CategoryName, CategoryDescription = @CategoryDescription WHERE CategoryId = @CategoryId",
            obj);
    }




}