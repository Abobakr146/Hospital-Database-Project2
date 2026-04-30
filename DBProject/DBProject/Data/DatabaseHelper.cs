using System.Data.SqlClient;
using System.Configuration;

public static class DatabaseHelper
{
    public static SqlConnection GetConnection()
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        return new SqlConnection(connString);
    }
}