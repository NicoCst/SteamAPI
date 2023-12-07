using BLL.Interfaces;
using BLL.Models.Forms;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ASteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Authenticate user and generate a JWT token.
        /// </summary>
        /// <param name="form">Login information.</param>
        /// <returns>
        ///  - 200 OK with a JWT token if authentication is successful.
        ///  - 400 Bad Request if authentication fails.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginForm form)
        {
            // Attempt to authenticate the user
            User? user = _authService.Login(form);

            // Check if authentication failed
            if (user is null)
            {
                return BadRequest("Invalid credentials");
            }

            // Check if the user is a developer (IsDev == 1)
            if (user.IsDev == 1)
            {
                // Generate a JWT token for developers
                string token = _jwtService.CreateToken(user);
                return Ok(new { Token = token });
            }

            // Authentication successful for non-developer users
            return Ok("Authentication successful");
        }
    }
}
