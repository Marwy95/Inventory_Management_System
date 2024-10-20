using Inventory_Management_System.VerticalSlice.Common.Enums;
using Inventory_Management_System.VerticalSlice.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventory_Management_System.VerticalSlice.Common.Services.TokenService
{
    public class JWTTokenService:ITokenService
    {
        public async Task<ResultDto<string>> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                             {
                    new Claim("Id", user.ID.ToString()),

                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim("UserName", user.UserName)
                             }),
                    Expires = DateTime.UtcNow.AddMinutes(90),
                    Issuer = Environment.GetEnvironmentVariable("ISSUER"),
                    Audience = Environment.GetEnvironmentVariable("AUDIENCE"),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return ResultDto<string>.Sucess(tokenHandler.WriteToken(token), "");

            }
            catch (Exception ex)
            {
                return ResultDto<string>.Faliure(ErrorCode.UnableTogenerateToken, "Unable to Create Token");
            }





        }
    }
}
