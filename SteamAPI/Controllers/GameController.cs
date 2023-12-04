using BLL.Interfaces;
using BLL.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    // Global Games Functions
    
    [HttpPost("CreateGame")]
    public ActionResult<GameDTO> Create(GameForm form) 
    { 
        GameDTO user = _gameService.Create(form);

        return user == null ? BadRequest() : Ok(user);
    }
    
    // Gamelist Functions
    
    [HttpGet("GetAllMyGames")]
    public ActionResult<IEnumerable<GameDTO>> GetAllMyGames(int id)
    {
        return Ok(_gameService.GetAllMyGames(id));
    } 
}