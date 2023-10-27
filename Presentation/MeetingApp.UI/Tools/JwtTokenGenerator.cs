using MeetingApp.Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetingApp.UI.Tools
{
    public class JwtTokenGenerator
    {
        public static string GenerateToken(CheckUserResponseDto dto)
        {
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            var claims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(dto.Id))
                claims.Add(new Claim (ClaimTypes.NameIdentifier , dto.Id.ToString()));
            if (!string.IsNullOrWhiteSpace(dto.Name))
                claims.Add(new Claim(ClaimTypes.Name , dto.Name + " " + dto.Surname));
            claims.Add(new Claim(ClaimTypes.Email, dto.Email));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler tokenHandler = new();

            JwtSecurityToken token = new(issuer:JwtTokenDefaults.ValidIssuer,audience:JwtTokenDefaults.ValidAudience,claims:claims,expires:expireDate,notBefore:DateTime.UtcNow,signingCredentials:signInCredentials);

            return tokenHandler.WriteToken(token);
        }  
    }
}
