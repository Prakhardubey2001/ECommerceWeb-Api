using ECommerce.AuthApi.Models;
using ECommerce.AuthApi.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.AuthApi.Service
{
    /// <summary>
    /// Service for handling the authentication related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly UserManager<ECommerceUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<ECommerceUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// To Generate the JWT token.
        /// </summary>
        /// <param name="role">The role of the user.</param>
        /// <param name="user">The user.</param>
        /// <returns>The JWT token.</returns>
        private string GenerateToken(string role, ECommerceUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
        /// <summary>
        /// Authenticates the user and generates the JWT token.
        /// </summary>
        /// <param name="loginDto">The login data transfer object.</param>
        /// <returns>The login response data transfer object.</returns>
        public async Task<LoginResponseDTO> Login([FromBody]LoginDTO loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result == false || user == null)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }
            string token = GenerateToken(user.Role, user);
            UserDTO userDto = new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            LoginResponseDTO loginResponseDto = new LoginResponseDTO()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        
    }
}
