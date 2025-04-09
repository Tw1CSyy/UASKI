using Npgsql;

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

        /// <summary>
        /// Открыто ли подключение
        /// </summary>
        public static bool IsOpened { get; private set; } = false;

        /// <summary>
        /// Создает объект подключения
        /// </summary>
        /// <param name="connectionString">Строка подлкючения</param>
        public static void CreateConnection(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Открывает подключение
        /// </summary>
        public static void Open()
        {
            if(!IsOpened)
            {
                Connection.Open();
                IsOpened = true;
            }
        }

        /// <summary>
        /// Закрывает подключение
        /// </summary>
        public static void Close()
        {
            if (IsOpened)
            {
                Connection.Close();
                IsOpened = false;
            }
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
