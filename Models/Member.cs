namespace WebApp.Models;

public class Member
{
    public string MemberId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string GivenName { get; set; } = null!;
    public string? SurName { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }

}
