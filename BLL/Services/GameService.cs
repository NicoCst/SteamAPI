using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models.Forms;
using DAL.Interfaces;

namespace BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public GameDTO Create(GameForm form)
    {
        return _gameRepository.Create(form.ToGame()).ToGameDto();
    }
    
    public IEnumerable<GameDTO> GetAllMyGames(int userId)
    {
        return _gameRepository.GetAllMyGames(userId).Select(x => x.ToGameDto());
    }
}
