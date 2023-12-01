using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}