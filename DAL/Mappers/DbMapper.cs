using System.Data.SqlClient;
using DAL.Entities;

namespace DAL.Mappers;

public static class DbMapper
{
    public static User ToUser(this SqlDataReader reader)
    {
        return new User
        {
            Id = Convert.ToInt32(reader["Id"]),
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            NickName = reader["NickName"].ToString(),
            Email = reader["Email"].ToString(),
            Password = reader["Password"].ToString(),
            IsDev = Convert.ToInt32(reader["IsDev"]),
            Wallet = Convert.ToSingle(Convert.ToDecimal(reader["Wallet"]))
        };
    }

    public static Game ToGame(this SqlDataReader reader)
    {
        return new Game
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString(),
            Genre = reader["Genre"].ToString(),
            Version = reader["Version"].ToString(),
            UserDevId = Convert.ToInt32(reader["UserDevId"])
        };
    }
}