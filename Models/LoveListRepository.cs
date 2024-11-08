using System.Data;
using Dapper;

namespace WebApp.Models;

public class LoveListRepository : BaseRepository
{
    public LoveListRepository(IDbConnection connection) : base(connection)
    {
    }

    public int Add(string MemberId, string ProductId)
    {
        string sql = "INSERT INTO LoveList (MemberId, ProductId) VALUES (@MemberId, @ProductId)";
        return connection.Execute(sql, new { MemberId, ProductId });
    }

    public IEnumerable<LoveList> GetLoveList(string MemberId)
    {
        return connection.Query<LoveList>("SELECT * FROM LoveList WHERE MemberId = @MemberId");
    }

}