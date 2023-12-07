using BLL.Interfaces;
using BLL.Models.Forms;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

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

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Login(LoginForm form)
    {

        User? user = _authService.Login(form);

        if (user is null) 
        {
            return BadRequest();
        }

        if (user.IsDev == 1)
        {
            return Ok(_jwtService.CreateToken(user));
        }

        return Ok();
    }
}