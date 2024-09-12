using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Eapproval.Models;
using Eapproval.Services.IServices;


namespace Eapproval.services;
public class JwtTokenService:IJwtTokenService
{
    private readonly string _secretKey;

    public JwtTokenService()
    {
        _secretKey = "secretKeyadfsssssssssssssssssssssssweewfewwwwwwwwwwwwwwwwwwwwwwwwwwwwwweqeqwewqeqweqweqweqweqwe";
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            // new Claim("empName", user.EmpName),
            // new Claim("designation", user.Designation),
            new Claim("mailAddress", user.MailAddress),
            new Claim("userType", user.UserType),
            // Add additional claims as needed
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(10), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public User ParseToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return new User
            {
                Id = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                EmpName = claimsPrincipal.FindFirst("empName")?.Value,
                Designation = claimsPrincipal.FindFirst("designation")?.Value,
                MailAddress = claimsPrincipal.FindFirst("mailAddress")?.Value,
                UserType = claimsPrincipal.FindFirst("userType")?.Value,
            };
        }
        catch
        {
            return null; // Invalid token
        }
    }
}
