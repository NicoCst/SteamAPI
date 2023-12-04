using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IGameRepository : IRepository<int, Game>
{
    IEnumerable<Game> GetAllMyGames(int userId);
    bool IsGameInUserList(User user, Game game);
    float GetPrice(string title);
    Game? GetByTitle(string title);
    bool BuyGame(User user1, User user2, Game game);
}