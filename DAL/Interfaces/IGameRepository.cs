using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IGameRepository : IRepository<int, Game>
{
    IEnumerable<Game> GetAllMyGames(int userId);
    bool IsGameInUserList(User user, Game game);
    bool IsGameInWishList(User user, Game game);
    float GetPrice(string title);
    bool SetNewPrice(Game game, float price);
    Game? GetByTitle(string title);
    bool BuyGame(User user1, User user2, Game game);
    bool BuyWishedGame(User user1, User user2, Game game);
    bool RefundGame(User user, Game game);
    bool AddToWishlist(User user, Game game);
    bool SetToWishlist(User user, Game game);
}