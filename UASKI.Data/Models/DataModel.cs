using Npgsql;
namespace UASKI.Models
{
    /// <summary>
    /// Класс для подключения к БД
    /// </summary>
    public static class DataModel
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
        public static void Open()
        {
            Connection.Open();
        }

        /// <summary>
        /// Закрывает подключение
        /// </summary>
        public static void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Возвращает подключение
        /// </summary>
        public static NpgsqlConnection Get()
        {
            return Connection;
        }

        /// <summary>
        /// Выполняет команду Sql
        /// </summary>
        /// <param name="query">Строка запроса Sql</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public static bool Complite(string query)
        {
            var command = new NpgsqlCommand(query, Get());
            return command.ExecuteNonQuery() == 1;
        }
    }
}
