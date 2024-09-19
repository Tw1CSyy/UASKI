using Npgsql;
namespace UASKI.Models
{
    /// <summary>
    /// Класс для подключения к БД
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        private readonly static string ConnectionString = "Host=localhost;UserName=uaski;Password=0404;Database=UASKI";

        /// <summary>
        /// Объект подключения
        /// </summary>
        private readonly static NpgsqlConnection Connection = new NpgsqlConnection(ConnectionString);

        /// <summary>
        /// Открывает подключение
        /// </summary>
        public void Open()
        {
            Connection.Open();
        }

        /// <summary>
        /// Закрывает подключение
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Возвращает подключение
        /// </summary>
        public NpgsqlConnection Get()
        {
            return Connection;
        }
    }
}
