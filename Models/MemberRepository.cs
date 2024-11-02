using System.Data;
using Dapper;

namespace WebApp.Models;

public class MemberRepository : BaseRepository
{


    public MemberRepository(IDbConnection connection) : base(connection) { }


    public int Add(Member obj)
    {
        return connection.Execute("AddMember", obj, commandType: CommandType.StoredProcedure);

    }

    public Member? FindMemberByEmail(string email)
    {
        var member = connection.QuerySingleOrDefault<Member>(
            "FindMemberByEmail",
            new { Email = email },
            commandType: CommandType.StoredProcedure);

        return member;
    }


    public IEnumerable<Member> GetMembers()
    {
        return connection.Query<Member>(" select MemberId, Email, SurName, GivenName, Roles.RoleName from Member  join Roles on Member.RoleId = Roles.RoleId");
    }

    public string? GenerateToken(string email)
    {
        string token = Guid.NewGuid().ToString().Replace("-", string.Empty);
        string sql = "UPDATE Member SET Token = @Token WHERE Email=@Email";
        int ret = connection.Execute(sql, new { token, email });
        if (ret > 0)
        {
            return token;
        }
        return null;
    }

    public int ChangePassword(string password, string token)
    {
        string sql = "UPDATE Member SET Password = @Password WHERE Token=@Token";
        return connection.Execute(sql, new { password, token });
    }


    public Member? Login(LoginModel obj)
    {
        string sql = "SELECT * FROM Member WHERE  Email = @Eml AND Password = @Pwd";
        return connection.QueryFirstOrDefault<Member>(sql, new { obj.Eml, obj.Pwd });
    }



}