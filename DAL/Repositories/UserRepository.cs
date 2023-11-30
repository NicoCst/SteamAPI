using System.Data.Common;
using System.Data.SqlClient;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using ToolBox.Database;
using ToolBox.Services;

namespace DAL.Repositories;

public class UserRepository : Repository, IUserRepository
{
    public UserRepository(string connectionString) : base(connectionString)
    {
    }

    public IEnumerable<User> GetAll()
    {
        using(SqlCommand cmd = new SqlCommand()) 
        {
            cmd.CommandText = "Select * FROM Users";

            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
        }
    }
    
    public User? GetById(int id)
    {
        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Users WHERE Id = @Id";

            cmd.Parameters.AddWithValue("Id", id);

            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
        }    
    }

    public IEnumerable<User> GetAllFriends(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT FirstName, LastName FROM Friends AS F JOIN Users AS U ON F.FriendId = U.Id WHERE F.UserId = @Id AND F.Validate = 1";

            cmd.Parameters.AddWithValue("Id", id);
            
            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
        }
    }
    
    public User? Create(User entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Users OUTPUT inserted.id VALUES (@FirstName, @LastName, @Email, @Password, @IsDev, @IsPlaying, @Wallet)";

            cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("LastName", entity.LastName);
            cmd.Parameters.AddWithValue("Email", entity.Email);
            cmd.Parameters.AddWithValue("Password", entity.Password);
            cmd.Parameters.AddWithValue("IsDev", entity.IsDev);
            cmd.Parameters.AddWithValue("IsPlaying", entity.IsPlaying);
            cmd.Parameters.AddWithValue("Wallet", entity.Wallet);

            entity.Id = (int)cmd.CustomScalar(ConnectionString);

            return entity;
        }
    }

    public bool Update(User entity)
    {
        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE Users " +
                              "SET FirstName = @FirstName, " +
                              "LastName = @LastName, " +
                              "Email = @Email, " +
                              "Password = @Password " +
                              "WHERE Id = @Id";

            cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("LastName", entity.LastName);
            cmd.Parameters.AddWithValue("Email", entity.Email);
            cmd.Parameters.AddWithValue("Password", entity.Password);
            cmd.Parameters.AddWithValue("IsDev", entity.IsDev);
            cmd.Parameters.AddWithValue("IsPlaying", entity.IsPlaying);
            cmd.Parameters.AddWithValue("Wallet", entity.Wallet);

            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }

    public bool Delete(User entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "DELETE FROM User WHERE Id = @Id";

            cmd.Parameters.AddWithValue("Id", entity.Id);

            return cmd.CustomNonQuery(ConnectionString) == 1;
        }    
    }
}