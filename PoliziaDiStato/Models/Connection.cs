using System.Configuration;
using System.Data.SqlClient;

namespace PoliziaDiStato.Models
{
    public class Connection
    {
        public static SqlConnection GetConnection()
        {
            string connection = ConfigurationManager.ConnectionStrings["PoliziaStatoDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            return conn;
        }
    }
}