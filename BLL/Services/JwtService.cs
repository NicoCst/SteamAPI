using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class JwtService : IJwtService
    {
        private readonly string SecretKey;
        private readonly string ExpiresDays;

        public JwtService(string secretKey, string expiresDays)
        {
            SecretKey = secretKey;
            ExpiresDays = expiresDays;
        }

        /// <summary>
        /// Create a JWT token for the given user.
        /// </summary>
        public string CreateToken(User user)
        {
            // Create a symmetric security key using the provided secret key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Create signing credentials using the key and the HmacSha256 algorithm
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Create a list of claims for the user
            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            // Create a JWT token with the specified claims, expiration date, and signing credentials
            var token = new JwtSecurityToken(claims: Claims, expires: DateTime.Now.AddDays(Convert.ToUInt32(ExpiresDays)), signingCredentials: Credentials);

            // Write the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
