using BLL.Models.Forms;

namespace BLL.Interfaces;

public interface IGameService
{
    GameDTO Create(GameForm form);
}