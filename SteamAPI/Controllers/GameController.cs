using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

public class GameController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}