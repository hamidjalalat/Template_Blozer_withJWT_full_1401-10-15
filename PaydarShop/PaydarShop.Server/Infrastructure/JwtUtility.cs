using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Models;
using PaydarShop.Server.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaydarShop.Server.Infrastructure
{
    public static class JwtUtility
    {
        static JwtUtility()
        {

        }
        public static string GenerateJwtToken(Models.User user,applicationsettings.Main mainSettings)
        {
            byte[] key = System.Text.Encoding.ASCII.GetBytes(mainSettings.SecretKey);

            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);

            var securityAlgorithm = Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;

            var signigCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(nameof(user.FullName), user.FullName),
                                                     new Claim(ClaimTypes.Name, user.Username),
                                                     new Claim(ClaimTypes.Email, user.EmailAddress),
                                                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),     }),
                Expires = DateTime.UtcNow.AddDays(mainSettings.TokenExpireInMinutes),
                SigningCredentials = signigCredentials
            };
            var tokenHandler =new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            Microsoft.IdentityModel.Tokens.SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            string token = tokenHandler.WriteToken(token:securityToken);

            return token;
        }

        public static void attachUserToContextByToken(HttpContext context, IUserService userService, string token,string secretKey)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(secretKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken==null)
                {
                    return;
                }
                Claim userIdClaim = jwtToken.Claims.Where(C => C.Type.ToLower() == "NameId".ToLower()).FirstOrDefault();
                if (userIdClaim==null)
                {
                    return;
                }
                var userId = Guid.Parse(userIdClaim.Value);
                //User foundedUser = userService.GetById(userId);
                User foundedUser = userService.GetById(userId);
                if (foundedUser == null)
                {
                    return;
                }

                context.Items["User"] = foundedUser;

            }
            catch(Exception ex)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

    }
}
