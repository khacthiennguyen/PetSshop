using System.Data;
using System.Runtime.InteropServices;
using Dapper;

namespace WebApp.Models;

public class MemberRepository : BaseRepository
{


    public MemberRepository(IDbConnection connection) : base(connection) { }


    public int Add(Member member)
    {
        if (string.IsNullOrEmpty(member.Role))
        {
            member.Role = Role.Customer.ToString();
        }
        return connection.Execute("AddMember", member, commandType: CommandType.StoredProcedure);

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
        return connection.Query<Member>(" select * from Member ");
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

    public int DeleteMember(string id)
    {
        string sql = "DELETE FROM Member WHERE MemberId = @id";
        return connection.Execute(sql, new { id });
    }

    public int UpdateMemberRole(string MemberId, string Role)
    {
        return connection.Execute("UPDATE Member SET Role = @Role WHERE MemberId = @MemberId",new{MemberId,Role});
    }


}