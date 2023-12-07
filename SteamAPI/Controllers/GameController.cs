using BLL.Interfaces;
using BLL.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // POST: api/Game/CreateGame
        [Authorize]
        [HttpPost("CreateGame")]
        public ActionResult<GameDTO> Create(GameForm form)
        {
            var game = _gameService.Create(form);
            return game != null ? Ok(game) : BadRequest();
        }

        // PUT: api/Game/{id}
        [Authorize]
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, GameForm form)
        {
            var result = _gameService.Update(id, form);
            return result ? Ok() : BadRequest();
        }

        // GET: api/Game/GetAllMyGames
        [HttpGet("GetAllMyGames/{id:int}")]
        public ActionResult<IEnumerable<GameDTO>> GetAllMyGames(int id)
        {
            var games = _gameService.GetAllMyGames(id);
            return Ok(games);
        }

        // POST: api/Game/BuyGame
        [HttpPost("BuyGame")]
        public IActionResult BuyGame(BuyGameForm form)
        {
            var result = _gameService.BuyGame(form);
            return result ? Ok() : NotFound("Le jeu est déjà possédé ou il manque un champ à compléter dans le form");
        }

        // DELETE: api/Game/RefundGame
        [HttpDelete("RefundGame")]
        public IActionResult RefundGame(RefundGameForm form)
        {
            var result = _gameService.RefundGame(form);
            return result ? Ok() : NotFound("Le remboursement n'est pas possible");
        }

        // PATCH: api/Game/SetToWishlist
        [HttpPatch("SetToWishlist")]
        public IActionResult SetToWishlist(AddToWishlistForm form)
        {
            var result = _gameService.SetToWishlist(form);
            return result ? Ok() : NotFound("Imposible de modifier le statut wishlist");
        }

        // POST: api/Game/SetNewPrice
        [Authorize]
        [HttpPost("SetNewPrice")]
        public IActionResult SetNewPrice(SetNewPriceForm form)
        {
            var result = _gameService.SetNewPrice(form);
            return result ? Ok() : NotFound("Impossible de mettre un nouveau prix");
        }
    }
}