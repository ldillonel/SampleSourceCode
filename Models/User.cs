using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    // string Id is part of IdentityUser
    public string Token { get; set; }
    public string Role { get; set; }
}