using System.Data;

namespace WebApp.Models;
public abstract class BaseRepository
{
    protected IDbConnection connection;
    public BaseRepository(IDbConnection connection) => this.connection = connection;
}