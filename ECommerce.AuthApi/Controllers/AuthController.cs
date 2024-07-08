using ECommerce.AuthApi.Models;
using ECommerce.AuthApi.Models.DTO;
using ECommerce.AuthApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AuthApi.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthService authService;
        private readonly UserManager<ECommerceUser> userManager;
        private ResponseDTO response;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="authService">The authentication service.</param>
        public AuthController(UserManager<ECommerceUser> userManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
            response = new ResponseDTO();
        }

        /// <summary>
        /// To Handle user login.
        /// </summary>
        /// <param name="loginDto">The login data transfer object.</param>
        /// <returns>The action result.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Either Username or password is not properly defined");
                }
                var loginResponseDto = await authService.Login(loginDto);
                if (loginResponseDto.User == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Username or Password is incorrect";
                    return BadRequest(response);
                }
                
                response.Result = loginResponseDto;
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
        /// <summary>
        /// Retrieves the details of the user.
        /// </summary>
        /// <returns>The action result.</returns>
        [HttpGet("user-details")]
        [Authorize]
        public async Task<IActionResult> UserDetails()
        {
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return Unauthorized("Please login first!!");
                }
                var userDto = new UserDetailsDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role
                    
                };
                
                return Ok(userDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }

        }
    }
}
