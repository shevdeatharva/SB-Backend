using Microsoft.IdentityModel.Tokens;
using Shubham_Bhawtu.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shubham_Bhawtu.Authentication.Filters
{
    public class JwtAuthentcationManager:IJwtAuthentcationManager
    {
        private readonly string key;
        public JwtAuthentcationManager(string key)
        {
            this.key = key;
        }

        string IJwtAuthentcationManager.Authenticate(LoginInfoModel user, bool SamalFlag)
        {

            var claims = new List<Claim>
            {
              new Claim("UserName", user.Username)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor();
            if (SamalFlag == true)
            {
                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),

                };
            }
            else
            {
                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),

                };
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string RefreshTokenExpirationTime(string token, bool SamalFlag)
        {
            if (SamalFlag == true)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                var existingClaims = claimsPrincipal.Claims.ToList();

                var now = DateTime.UtcNow;
                var newExpirationTime = now.AddMinutes(30);

                var existingExpirationTimeClaim = existingClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
                if (existingExpirationTimeClaim != null)
                {
                    existingClaims.Remove(existingExpirationTimeClaim);
                }

                // Add the new expiration time claim
                existingClaims.Add(new Claim(JwtRegisteredClaimNames.Exp, ToUnixTimeSeconds(newExpirationTime).ToString(), ClaimValueTypes.Integer64));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(existingClaims),
                    Expires = newExpirationTime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var newToken = tokenHandler.CreateToken(tokenDescriptor);
                var newTokenString = tokenHandler.WriteToken(newToken);
                return newTokenString;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                var existingClaims = claimsPrincipal.Claims.ToList();

                var now = DateTime.UtcNow;
                var newExpirationTime = now.AddMinutes(30);

                var existingExpirationTimeClaim = existingClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
                if (existingExpirationTimeClaim != null)
                {
                    existingClaims.Remove(existingExpirationTimeClaim);
                }

                existingClaims.Add(new Claim(JwtRegisteredClaimNames.Exp, ToUnixTimeSeconds(newExpirationTime).ToString(), ClaimValueTypes.Integer64));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(existingClaims),
                    Expires = newExpirationTime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var newToken = tokenHandler.CreateToken(tokenDescriptor);
                var newTokenString = tokenHandler.WriteToken(newToken);
                return newTokenString;
            }
        }

        private static long ToUnixTimeSeconds(DateTime dt)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(dt.ToUniversalTime() - epoch).TotalSeconds;
        }
    }
}
