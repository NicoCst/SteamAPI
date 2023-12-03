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

    public User? GetByNickname(string nickname)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Users WHERE NickName = @NickName";

            cmd.Parameters.AddWithValue("NickName", nickname);
            
            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
        }
    }

    public IEnumerable<User> GetAllFriends(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT u.* FROM Users u INNER JOIN Friends f ON u.Id = f.UserId OR u.Id = f.FriendId WHERE ((f.UserId = @Id AND u.Id != @Id) OR (f.FriendId = @Id AND u.Id != @Id)) AND f.Validate = 1";

            cmd.Parameters.AddWithValue("Id", id);
            
            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
        }
    }

    public IEnumerable<User> GetFriendsRequests(int id)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText =
                "SELECT u.* FROM Users u INNER JOIN Friends f ON u.Id = f.UserId OR u.Id = f.FriendId WHERE ((f.UserId = @Id AND u.Id != @Id) OR (f.FriendId = @Id AND u.Id != @Id)) AND f.Validate = 0";
                
            cmd.Parameters.AddWithValue("Id", id);
            
            return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
        }
    }

    public bool AcceptFriendRequest(User entity1, User entity2)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText =
                "UPDATE Friends SET Validate = 1 WHERE (UserId = @UserId AND FriendId = @FriendId) AND Validate = 0";
            
            cmd.Parameters.AddWithValue("@UserId", entity1.Id);
            cmd.Parameters.AddWithValue("@FriendId", entity2.Id);
                
            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }

    public bool DeleteFriendRequest(User entity1, User entity2)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText =
                "UPDATE Friends SET Validate = 0 WHERE (UserId = @UserId AND FriendId = @FriendId) AND Validate = 1";
                
            cmd.Parameters.AddWithValue("@UserId", entity1.Id);
            cmd.Parameters.AddWithValue("@FriendId", entity2.Id);
                
            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }
    
    public User? Create(User entity)
    {
        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Users OUTPUT inserted.id VALUES (@FirstName, @LastName, @NickName, @Email, @Password, @IsDev, @IsPlaying, @Wallet)";

            cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("LastName", entity.LastName);
            cmd.Parameters.AddWithValue("NickName", entity.LastName);
            cmd.Parameters.AddWithValue("Email", entity.Email);
            cmd.Parameters.AddWithValue("Password", entity.Password);
            cmd.Parameters.AddWithValue("IsDev", entity.IsDev);
            cmd.Parameters.AddWithValue("IsPlaying", entity.IsPlaying);
            cmd.Parameters.AddWithValue("Wallet", entity.Wallet);

            entity.Id = (int)cmd.CustomScalar(ConnectionString);

            return entity;
        }
    }

    public bool CreateFriendRequest(User entity1, User entity2)
    {
        if (entity1 == null || entity2 == null)
        {
            throw new ArgumentNullException("entity1 and entity2 cannot be null");
        }

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "INSERT INTO Friends VALUES (@UserId, @FriendId, @Date, @Validate)";
        
            cmd.Parameters.AddWithValue("UserId", entity1.Id);
            cmd.Parameters.AddWithValue("FriendId", entity2.Id);
            cmd.Parameters.AddWithValue("Date", DateTime.Now);
            cmd.Parameters.AddWithValue("Validate", 0);
            
            return cmd.CustomNonQuery(ConnectionString) == 1;
        }
    }
     
    public bool Update(User entity)
    {
        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "UPDATE Users " +
                              "SET FirstName = @FirstName, " +
                              "LastName = @LastName, " +
                              "NickName = @NickName, " +
                              "Email = @Email, " +
                              "Password = @Password " +
                              "WHERE Id = @Id";

            cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
            cmd.Parameters.AddWithValue("LastName", entity.LastName);
            cmd.Parameters.AddWithValue("NickName", entity.LastName);
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