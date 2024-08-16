using APIWeb.Entities;
using APIWeb.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIWeb.Services
{
    public class ServiceRepository
        (
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        public record class LoginResponse(bool Flag, string Token, string Message);
        async Task<LoginResponse> LoginAccount(Login login)
        {
            if (login == null)
                return new LoginResponse(false, null!, "Login containe is empty");
            var getUser = await userManager.FindByEmailAsync(login.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");
            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, login.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Sai tài khoản hoặc mật khẩu");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Email, getUserRole.First());
            string token = GenerateJwtToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }
        private string GenerateJwtToken(UserSession user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));//config["Jwt:Key"] lấy key bên appsetting

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),               
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)  // Ensure Role is valid
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString.ToString();
        }
    }
    }
