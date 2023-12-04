using System.Data.SqlClient;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using ToolBox.Database;
using ToolBox.Services;

namespace DAL.Repositories;

public class GameRepository : Repository, IGameRepository
{
    public GameRepository(string connectionString) : base(connectionString)
    {
    }
    
    // Global Games Functions
    public IEnumerable<Game> GetAll()
    {
        using(SqlCommand cmd = new SqlCommand()) 
        {
            cmd.CommandText = "Select * FROM Games";

            return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x));
        }
    }

    public Game? GetById(int id)
    {
        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Games WHERE Id = @Id";

            cmd.Parameters.AddWithValue("Id", id);

            return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x)).SingleOrDefault();
        }
    }

    public Game? GetByTitle(string title)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Games WHERE Name = @Name";

            cmd.Parameters.AddWithValue("Name", title);

            return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x)).SingleOrDefault();
        }
    }

    public Game? Create(Game entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Games OUTPUT inserted.id VALUES (@Name, @Genre, @Version, @UserDevId)";

            cmd.Parameters.AddWithValue("Name", entity.Name);
            cmd.Parameters.AddWithValue("Genre", entity.Genre);
            cmd.Parameters.AddWithValue("Version", entity.Version);
            cmd.Parameters.AddWithValue("UserDevId", entity.UserDevId);

            entity.Id = (int)cmd.CustomScalar(ConnectionString);

            return entity;
        }
    }

    public bool Update(Game entity)
    {
        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE Games " +
                              "SET Name = @Name, " +
                              "Genre = @Genre, " +
                              "Version = @Version, " +
                              "UserDevId = @UserDevId " +
                              "WHERE Id = @Id";

            cmd.Parameters.AddWithValue("Name", entity.Name);
            cmd.Parameters.AddWithValue("Genre", entity.Genre);
            cmd.Parameters.AddWithValue("Version", entity.Version);
            cmd.Parameters.AddWithValue("UserDevId", entity.UserDevId);

            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }

    public bool Delete(Game entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "DELETE FROM Games WHERE Id = @Id";

            cmd.Parameters.AddWithValue("Id", entity.Id);

            return cmd.CustomNonQuery(ConnectionString) == 1;
        }    
    }
    
    // GamesList Functions
    public IEnumerable<Game> GetAllMyGames(int userId)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText =
                "SELECT Games.* FROM Games INNER JOIN GamesList ON Games.Id = GamesList.GameId WHERE GamesList.UserId = @UserId";

            cmd.Parameters.AddWithValue("UserId", userId);
            return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x));
        }
    }

    public bool IsGameInUserList(User user, Game game)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT COUNT(*) FROM GamesList WHERE UserId = @UserId AND GameId = @GameId";
            cmd.Parameters.AddWithValue("UserId", user.Id);
            cmd.Parameters.AddWithValue("GameId", game.Id);

            int count = (int)cmd.CustomScalar(ConnectionString);

            return count > 0;
        }
    }
    
    public bool BuyGame(User user1, User user2, Game game)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO GamesList VALUES(@UserId, @GameId, @PurchaseDate, @PlayTime, @GiftId, @IsWished)";

            cmd.Parameters.AddWithValue("UserId", user2.Id);
            cmd.Parameters.AddWithValue("GameId", game.Id);
            cmd.Parameters.AddWithValue("PurchaseDate", DateTime.Now);
            cmd.Parameters.AddWithValue("PlayTime", 0);
            cmd.Parameters.AddWithValue("GiftId", user1.Id);
            cmd.Parameters.AddWithValue("IsWished", 0);

            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }

    public float GetPrice(string title)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT Price FROM PricesList JOIN Games G on G.Id = PricesList.GameId WHERE Name = @Name";

            cmd.Parameters.AddWithValue("Name", title);
            
            return (float)cmd.CustomScalar(ConnectionString);
        }
    }
}