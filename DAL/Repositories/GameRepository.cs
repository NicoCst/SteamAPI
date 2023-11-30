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
}