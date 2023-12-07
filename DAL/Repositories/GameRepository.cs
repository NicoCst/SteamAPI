using System.Data.SqlClient;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Mappers;
using ToolBox.Database;
using ToolBox.Services;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository for handling Game-related database operations.
    /// </summary>
    public class GameRepository : Repository, IGameRepository
    {
        public GameRepository(string connectionString) : base(connectionString)
        {
        }

        #region Global Games Functions

        /// <summary>
        /// Get all games from the database.
        /// </summary>
        public IEnumerable<Game> GetAll()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * FROM Games";
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x));
            }
        }

        /// <summary>
        /// Get a game by its ID.
        /// </summary>
        public Game? GetById(int id)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * FROM Games WHERE Id = @Id";
                cmd.Parameters.AddWithValue("Id", id);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x)).SingleOrDefault();
            }
        }

        /// <summary>
        /// Get a game by its title.
        /// </summary>
        public Game? GetByTitle(string title)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * FROM Games WHERE Name = @Name";
                cmd.Parameters.AddWithValue("Name", title);
                return cmd.CustomReader(ConnectionString, x => DbMapper.ToGame(x)).SingleOrDefault();
            }
        }

        /// <summary>
        /// Create a new game and return the created entity.
        /// </summary>
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

        /// <summary>
        /// Update an existing game.
        /// </summary>
        public bool Update(Game entity)
        {
            using (SqlCommand cmd = new SqlCommand())
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
                cmd.Parameters.AddWithValue("Id", entity.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Delete a game from the database.
        /// </summary>
        public bool Delete(Game entity)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "DELETE FROM Games WHERE Id = @Id";
                cmd.Parameters.AddWithValue("Id", entity.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        #endregion

        #region GamesList Functions

        /// <summary>
        /// Get all games associated with a specific user.
        /// </summary>
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

        /// <summary>
        /// Check if a game is in the user's game list.
        /// </summary>
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

        /// <summary>
        /// Check if a game is in the user's wishlist.
        /// </summary>
        public bool IsGameInWishList(User user, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "SELECT COUNT(*) FROM GamesList WHERE UserId = @UserId AND GameId = @GameId AND IsWhished = @IsWhished";

                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);
                cmd.Parameters.AddWithValue("IsWhished", 1);

                int count = (int)cmd.CustomScalar(ConnectionString);

                return count > 0;
            }
        }

        /// <summary>
        /// Buy a game for a user.
        /// </summary>
        public bool BuyGame(User user1, User user2, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "INSERT INTO GamesList VALUES(@UserId, @GameId, @PurchaseDate, @PlayTime, @GiftId, @IsWished)";

                cmd.Parameters.AddWithValue("UserId", user2.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);
                cmd.Parameters.AddWithValue("PurchaseDate", DateTime.Now);
                cmd.Parameters.AddWithValue("PlayTime", 0);
                cmd.Parameters.AddWithValue("GiftId", user1.Id);
                cmd.Parameters.AddWithValue("IsWished", 0);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Buy a wished game for a user.
        /// </summary>
        public bool BuyWishedGame(User user1, User user2, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "UPDATE GamesList SET PurchaseDate = @PurchaseDate, PlayTime = @PlayTime, GiftId = @GiftId, IsWhished = @IsWhished WHERE UserId = @UserId AND GameId = @GameId";

                cmd.Parameters.AddWithValue("UserId", user2.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);
                cmd.Parameters.AddWithValue("PurchaseDate", DateTime.Now);
                cmd.Parameters.AddWithValue("PlayTime", 0);
                cmd.Parameters.AddWithValue("GiftId", user1.Id);
                cmd.Parameters.AddWithValue("IsWhished", 0);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Refund a game for a user.
        /// </summary>
        public bool RefundGame(User user, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "DELETE FROM GamesList WHERE ((UserId = @UserId AND GiftId IS NULL) OR (UserId = @UserId AND GiftId = @UserId)) AND PlayTime < 120 AND PurchaseDate >= DATEADD(DAY, -14, GETDATE()) AND GameId = @GameId;";

                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Add a game to the user's wishlist.
        /// </summary>
        public bool AddToWishlist(User user, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "INSERT INTO GamesList VALUES(@UserId, @GameId, @PurchaseDate, @PlayTime, @GiftId, @IsWhished)";

                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);
                cmd.Parameters.AddWithValue("PurchaseDate", DBNull.Value);
                cmd.Parameters.AddWithValue("PlayTime", 0);
                cmd.Parameters.AddWithValue("GiftId", user.Id);
                cmd.Parameters.AddWithValue("IsWhished", 1);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Set a game to the user's wishlist.
        /// </summary>
        public bool SetToWishlist(User user, Game game)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "UPDATE GamesList SET IsWhished = @IsWhished WHERE UserId = @UserId AND GameId = @GameId";

                cmd.Parameters.AddWithValue("IsWhished", 1);
                cmd.Parameters.AddWithValue("UserId", user.Id);
                cmd.Parameters.AddWithValue("GameId", game.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        /// <summary>
        /// Get the price of a game by its title.
        /// </summary>
        public float GetPrice(string title)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText =
                    "SELECT TOP 1 Price FROM PricesList JOIN Games G ON G.Id = PricesList.GameId WHERE Name = @Name ORDER BY PriceDate DESC";

                cmd.Parameters.AddWithValue("Name", title);

                return Convert.ToSingle(cmd.CustomScalar(ConnectionString));
            }
        }

        #endregion

        #region PriceList Functions

        /// <summary>
        /// Set a new price for a game.
        /// </summary>
        public bool SetNewPrice(Game game, float price)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "INSERT INTO PricesList (PriceDate, Price, GameId) VALUES (@PriceDate, @Price, @GameId)";

                cmd.Parameters.AddWithValue("PriceDate", DateTime.Now);
                cmd.Parameters.AddWithValue("Price", price);
                cmd.Parameters.AddWithValue("GameId", game.Id);

                return cmd.CustomNonQuery(ConnectionString) == 1;
            }
        }

        #endregion
    }
}
