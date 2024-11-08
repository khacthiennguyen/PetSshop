using System.Data;
using Dapper;

namespace WebApp.Models;

public class CartRepository : BaseRepository
{
    public CartRepository(IDbConnection connection) : base(connection)
    {
    }

    public int Edit(Cart obj)
    {
        string sql = "UPDATE Cart SET ProductQuantity = @ProductQuantity, UpdatedDate = GETDATE() WHERE CartCode = @CartCode AND ProductId = @ProductId";
        return connection.Execute(sql, new { obj.CartCode, obj.ProductId, obj.ProductQuantity });
    }

    public int Add(Cart obj)
    {
        return connection.Execute("AddCart", new { obj.CartCode, obj.ProductId, obj.MemberId, obj.ProductQuantity },
                                  commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Cart> GetCarts(string code,string MemberId)
    {
        string sql = "SELECT Cart.*, Product.ProductName, Product.ProductPrice, Product.ProductImg FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId WHERE Cart.CartCode = @code AND Cart.MemberId = @MemberId";
        return connection.Query<Cart>(sql, new { code, MemberId});
    }

    public int Delete(string cartCode, string productId)
    {
        string sql = "DELETE FROM Cart WHERE CartCode = @CartCode AND ProductId = @ProductId";
        return connection.Execute(sql, new { CartCode = cartCode, ProductId = productId });
    }
}