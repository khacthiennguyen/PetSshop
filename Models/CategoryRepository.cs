using System.Data;
using Dapper;
using WebApp.Controllers;

namespace WebApp.Models;

public class CategoryRepository : BaseRepository
{
    public CategoryRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Category> GetCategories()
    {
        return connection.Query<Category>("SELECT * FROM Category");
    }

    public int Add(Category obj)
    {
        var existingCategory = connection.QueryFirstOrDefault<Category>(
            "SELECT * FROM Category WHERE CategoryId = @CategoryId", new { obj.CategoryId });

        if (existingCategory != null)
        {
            return -1;
        }

        return connection.Execute("AddCategory", obj, commandType: CommandType.StoredProcedure);

    }


    public int Delete(string id)
    {

        return connection.Execute("DELETE FROM Category WHERE CategoryId = @id", new { id });

    }

    public int Update(Category obj)
    {
        var existingCategory = connection.QueryFirstOrDefault<Category>(
            "SELECT * FROM Category WHERE CategoryId = @CategoryId", new { obj.CategoryId });

        if (existingCategory == null)
        {
            return -1;
        }

        return connection.Execute(
            "UPDATE Category SET CategoryName = @CategoryName, CategoryDescription = @CategoryDescription WHERE CategoryId = @CategoryId",
            obj);
    }





}