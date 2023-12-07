using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services;

public class JwtService : IJwtService
{
    private readonly string SecretKey;

    private readonly string ExpiresDays;

    public JwtService(string secretKey, string expiresDays)
    {
        SecretKey = secretKey;
        ExpiresDays = expiresDays;
    }

    public string CreateToken(User user)
    {
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        List<Claim> Claims = new List<Claim>();

        Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

        var token = new JwtSecurityToken(claims: Claims, expires: DateTime.Now.AddDays(Convert.ToUInt32(ExpiresDays)),signingCredentials: Credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}