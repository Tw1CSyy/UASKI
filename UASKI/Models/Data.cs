using Npgsql;
namespace UASKI.Models
{
    public class Data
    {
        private readonly static string ConnectionString = "";
        private readonly static NpgsqlConnection Connection = new NpgsqlConnection(ConnectionString);

        public void Open()
        {
            Connection.Open();
        }

        public void Close()
        {
            Connection.Close();
        }

        public NpgsqlConnection Get()
        {
            return Connection;
        }
    }
}
