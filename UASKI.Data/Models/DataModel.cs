using Npgsql;
using System.Runtime.InteropServices;

namespace UASKI
{
    /// <summary>
    /// Класс для подключения к БД
    /// </summary>
    public static class DataModel
    {
        
        /// <summary>
        /// Объект подключения
        /// </summary>
        private static NpgsqlConnection Connection { get; set; }
        
        public static void CreateConnection(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

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
