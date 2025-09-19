using System;
using System.Dynamic;

using Npgsql;

namespace UASKI
{
    /// <summary>
    /// Класс для подключения к БД
    /// </summary>
    public class DataModel
    {

        /// <summary>
        /// Создает объект подключения
        /// </summary>
        /// <param name="connectionString">Строка подлкючения</param>
        public DataModel(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Объект подключения
        /// </summary>
        private NpgsqlConnection Connection { get; set; }

        /// <summary>
        /// Открыто ли подключение
        /// </summary>
        public bool IsOpened { get; private set; } = false;

        /// <summary>
        /// Открывает подключение
        /// </summary>
        public void Open()
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
        public void Close()
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
        public NpgsqlConnection Get()
        {
            return Connection;
        }

        /// <summary>
        /// Выполняет команду Sql
        /// </summary>
        /// <param name="query">Строка запроса Sql</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Complite(string query , int count = 1)
        {
            var command = new NpgsqlCommand(query, Get());

            try
            {
                var result = command.ExecuteNonQuery();
                return result == 1;
            }
            catch (Exception)
            {
                if (count == 10)
                    throw;

                Close();
                Open();
                count++;
                return Complite(query, count);
            }
        }

        /// <summary>
        /// Возвращает максимальное id в таблице
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <returns>int или 0</returns>
        public int GetMaxId(string tableName)
        {
            var command = new NpgsqlCommand($"SELECT MAX(\"Id\") FROM \"{tableName}\"", Get());
            var reader = command.ExecuteReader();
            int result = 0;

            while (reader.Read())
            {
                result = Convert.ToInt32(reader.GetValue(0));
            }

            reader.Close();
            return result;
        }
    }
}
