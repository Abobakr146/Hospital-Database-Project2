using System.Data.SqlClient;
using System.Configuration;

public class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        return new SqlConnection(connString);
    }
}