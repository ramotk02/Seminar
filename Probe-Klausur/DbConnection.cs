
using MySql.Data.MySqlClient;


public class DbConnection
{
    private static string  connectionString = "server=localhost;port=3306;uid=root;pwd=root;database=probeklausur;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

}