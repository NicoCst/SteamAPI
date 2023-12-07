using BLL.Models.Forms;

namespace BLL.Interfaces;

public interface IGameService
{
    GameDTO Create(GameForm form);

    bool Update(int id, GameForm form);

    IEnumerable<GameDTO> GetAllMyGames(int userId);
    float GetPrice(string Title);
    bool SetNewPrice(SetNewPriceForm form);
    bool BuyGame(BuyGameForm form);
    bool RefundGame(RefundGameForm form);
    bool SetToWishlist(AddToWishlistForm form);
}