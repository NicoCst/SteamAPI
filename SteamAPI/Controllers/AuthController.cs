using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

public class AuthController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}