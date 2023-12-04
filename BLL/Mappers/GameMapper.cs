using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Mappers;

public static class GameMapper
{
    public static Game ToGame(this GameForm form)
    {
        return new Game()
        {
            Id = 0,
            Name = form.Name,
            Genre = form.Genre,
            Version = form.Version,
            UserDevId = form.UserDevId
        };
    }

    public static GameDTO ToGameDto(this Game entity)
    {
        return new GameDTO()
        {
            Id = entity.Id,
            Name = entity.Name,
            Genre = entity.Genre,
            Version = entity.Version,
            UserDevId = entity.UserDevId
        };
    }
}