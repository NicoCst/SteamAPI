using System.Data.SqlClient;

namespace ToolBox.Database;

public static class DbCommands
{
    public static object CustomScalar(this SqlCommand cmd, string cs)
    {
        using(SqlConnection conn = new SqlConnection(cs))
        {
            cmd.Connection = conn;
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }
    }

    public static int CustomNonQuery(this SqlCommand cmd, string cs)
    {
        using(SqlConnection con = new SqlConnection(cs))
        {
            cmd.Connection = con;
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }

    public static IEnumerable<TEntity> CustomReader<TEntity>(this SqlCommand cmd, string cs, Func<SqlDataReader, TEntity> mapper)
    {
        using (SqlConnection conn = new SqlConnection(cs))
        {
            cmd.Connection = conn;
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()) 
            {
                yield return mapper(reader);
            }

            conn.Close();
        }
    }
}