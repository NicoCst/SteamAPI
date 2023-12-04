using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IGameRepository : IRepository<int, Game>
{
    IEnumerable<Game> GetAllMyGames(int userId);
    
}