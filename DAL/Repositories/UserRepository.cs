using System.Data.Common;
using System.Data.SqlClient;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using ToolBox.Database;
using ToolBox.Services;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for User entities.
    /// </summary>
    public class UserRepository : Repository, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        public IEnumerable<User> GetAll()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve all users
                cmd.CommandText = "SELECT * FROM Users";
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
            }
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>The user with the specified ID.</returns>
        public User? GetById(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve a user by ID
                cmd.CommandText = "SELECT * FROM Users WHERE Id = @Id";
                cmd.Parameters.AddWithValue("Id", id);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets a user by their nickname.
        /// </summary>
        /// <param name="nickname">User nickname.</param>
        /// <returns>The user with the specified nickname.</returns>
        public User? GetByNickname(string nickname)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve a user by nickname
                cmd.CommandText = "SELECT * FROM Users WHERE NickName = @NickName";
                cmd.Parameters.AddWithValue("NickName", nickname);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>The user with the specified email.</returns>
        public User? GetByEmail(string email)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve a user by email
                cmd.CommandText = "SELECT * FROM Users WHERE Email = @Email";
                cmd.Parameters.AddWithValue("Email", email);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x)).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates the wallet amount for a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="newWalletAmount">New wallet amount.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        public bool UpdateWallet(int userId, float newWalletAmount)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to update user wallet amount
                cmd.CommandText = "UPDATE Users SET Wallet = @NewWalletAmount WHERE Id = @UserId";
                cmd.Parameters.AddWithValue("NewWalletAmount", newWalletAmount);
                cmd.Parameters.AddWithValue("UserId", userId);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Gets all friends for a user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>A collection of user's friends.</returns>
        public IEnumerable<User> GetAllFriends(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve all friends for a user
                cmd.CommandText = "SELECT u.* FROM Users u INNER JOIN Friends f ON u.Id = f.UserId OR u.Id = f.FriendId WHERE ((f.UserId = @Id AND u.Id != @Id) OR (f.FriendId = @Id AND u.Id != @Id)) AND f.Validate = 1";
                cmd.Parameters.AddWithValue("Id", id);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
            }
        }

        /// <summary>
        /// Gets friend requests for a user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>A collection of user's friend requests.</returns>
        public IEnumerable<User> GetFriendsRequests(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to retrieve friend requests for a user
                cmd.CommandText = "SELECT u.* FROM Users u INNER JOIN Friends f ON u.Id = f.UserId OR u.Id = f.FriendId WHERE ((f.UserId = @Id AND u.Id != @Id) OR (f.FriendId = @Id AND u.Id != @Id)) AND f.Validate = 0";
                cmd.Parameters.AddWithValue("Id", id);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToUser(x));
            }
        }

        /// <summary>
        /// Accepts a friend request.
        /// </summary>
        /// <param name="entity1">User who sent the request.</param>
        /// <param name="entity2">User who received the request.</param>
        /// <returns>True if the friend request is accepted, otherwise false.</returns>
        public bool AcceptFriendRequest(User entity1, User entity2)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to accept a friend request
                cmd.CommandText = "UPDATE Friends SET Validate = 1 WHERE (UserId = @UserId AND FriendId = @FriendId) AND Validate = 0";
                cmd.Parameters.AddWithValue("@UserId", entity1.Id);
                cmd.Parameters.AddWithValue("@FriendId", entity2.Id);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Deletes a friend request.
        /// </summary>
        /// <param name="entity1">User who sent the request.</param>
        /// <param name="entity2">User who received the request.</param>
        /// <returns>True if the friend request is deleted, otherwise false.</returns>
        public bool DeleteFriendRequest(User entity1, User entity2)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to delete a friend request
                cmd.CommandText = "UPDATE Friends SET Validate = 0 WHERE (UserId = @UserId AND FriendId = @FriendId) AND Validate = 1";
                cmd.Parameters.AddWithValue("@UserId", entity1.Id);
                cmd.Parameters.AddWithValue("@FriendId", entity2.Id);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="entity">User entity to create.</param>
        /// <returns>The created user entity.</returns>
        public User? Create(User entity)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to insert a new user
                cmd.CommandText = "INSERT INTO Users OUTPUT inserted.id VALUES (@FirstName, @LastName, @NickName, @Email, @Password, @IsDev, @IsPlaying, @Wallet)";
                cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("LastName", entity.LastName);
                cmd.Parameters.AddWithValue("NickName", entity.NickName);
                cmd.Parameters.AddWithValue("Email", entity.Email);
                cmd.Parameters.AddWithValue("Password", entity.Password);
                cmd.Parameters.AddWithValue("IsDev", entity.IsDev);
                cmd.Parameters.AddWithValue("IsPlaying", entity.IsPlaying);
                cmd.Parameters.AddWithValue("Wallet", entity.Wallet);
                entity.Id = (int)cmd.CustomScalar(ConnectionString);
                return entity;
            }
        }

        /// <summary>
        /// Creates a friend request.
        /// </summary>
        /// <param name="entity1">User sending the request.</param>
        /// <param name="entity2">User receiving the request.</param>
        /// <returns>True if the friend request is created, otherwise false.</returns>
        public bool CreateFriendRequest(User entity1, User entity2)
        {
            if (entity1 == null || entity2 == null)
            {
                throw new ArgumentNullException("entity1 and entity2 cannot be null");
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to insert a friend request
                cmd.CommandText = "INSERT INTO Friends VALUES (@UserId, @FriendId, @Date, @Validate)";
                cmd.Parameters.AddWithValue("UserId", entity1.Id);
                cmd.Parameters.AddWithValue("FriendId", entity2.Id);
                cmd.Parameters.AddWithValue("Date", DateTime.Now);
                cmd.Parameters.AddWithValue("Validate", 0);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="entity">User entity to update.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        public bool Update(User entity)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to update user information
                cmd.CommandText = "UPDATE Users " +
                                  "SET FirstName = @FirstName, " +
                                  "LastName = @LastName, " +
                                  "NickName = @NickName, " +
                                  "Email = @Email, " +
                                  "Password = @Password, " +
                                  "IsDev = @IsDev, " +
                                  "IsPlaying = @IsPlaying, " +
                                  "Wallet = @Wallet " +
                                  "WHERE Id = @Id";

                cmd.Parameters.AddWithValue("FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("LastName", entity.LastName);
                cmd.Parameters.AddWithValue("NickName", entity.NickName);
                cmd.Parameters.AddWithValue("Email", entity.Email);
                cmd.Parameters.AddWithValue("Password", entity.Password);
                cmd.Parameters.AddWithValue("IsDev", entity.IsDev);
                cmd.Parameters.AddWithValue("IsPlaying", entity.IsPlaying);
                cmd.Parameters.AddWithValue("Wallet", entity.Wallet);
                cmd.Parameters.AddWithValue("Id", entity.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Adds money to a user's wallet.
        /// </summary>
        /// <param name="entity">User entity.</param>
        /// <param name="money">Amount of money to add.</param>
        /// <returns>True if the wallet is updated, otherwise false.</returns>
        public bool AddMoney(User entity, float money)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to update user wallet by adding money
                cmd.CommandText = "UPDATE Users SET Wallet = @Wallet WHERE Id = @Id";
                cmd.Parameters.AddWithValue("Id", entity.Id);
                cmd.Parameters.AddWithValue("Wallet", money);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Toggles the playing status for a user.
        /// </summary>
        /// <param name="user">User entity.</param>
        /// <param name="statut">New playing status.</param>
        /// <returns>True if the status is updated, otherwise false.</returns>
        public bool TogglePlayingStatut(User user, int statut)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to update user playing status
                cmd.CommandText = "UPDATE Users SET Statut = @Statut WHERE Id = @UserId";
                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("Statut", statut);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="entity">User entity to delete.</param>
        /// <returns>True if the user is deleted, otherwise false.</returns>
        public bool Delete(User entity)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SQL query to delete a user
                cmd.CommandText = "DELETE FROM Users WHERE Id = @Id";
                cmd.Parameters.AddWithValue("Id", entity.Id);
                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }
    }
}
