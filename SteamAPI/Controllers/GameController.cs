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

    [HttpPost("BuyGame")]
    public IActionResult BuyGame(BuyGameForm form)
    {
        bool result = _gameService.BuyGame(form);
        
        return result ? Ok() : NotFound("Le jeu est déjà possédé ou il manque un champ à compléter dans le form"); 
    }

    [HttpDelete("RefundGame")]
    public IActionResult RefundGame(RefundGameForm form)
    {
        bool result = _gameService.RefundGame(form);

        return result ? Ok() : NotFound("Le remboursement n'est pas possible");
    }

    [HttpPatch("SetToWishlist")]
    public IActionResult SetToWishlist(AddToWishlistForm form)
    {
        bool result = _gameService.SetToWishlist(form);

        return result ? Ok() : NotFound("Imposible de modifier le statut wishlist");
    }
}