using System.Collections.Generic;
using UASKI.Data.Requests;
using UASKI.Models;
using Npgsql;

namespace UASKI.Data.Context
{
    public class ContextService
    {
        /// <summary>
        /// Формирует Select строчку запроса
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <returns>Строку запроса Select</returns>
        private static string GetQuery(SelectRequest request)
        {
            string query = "SELECT ";

            foreach (var name in request.ColumnsName)
            {
                query += $"\"{name}\", ";
            }

            query = query.Remove(query.Length - 2, 2);
            query += $" FROM \"{request.TableName}\"";

            return query;
        }

        /// <summary>
        /// Формирует Add строчку запроса
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <returns>Строку запроса Add</returns>
        public static string GetQuery(AddRequest request)
        {
            string query = $"INSERT INTO \"{request.TableName}\" ( ";

            foreach (var col in request.Columns)
            {
                query += $"{col.Name}, ";
            }

            query = query.Remove(query.Length - 2, 2);
            query += ") VALUES ( ";

            foreach (var col in request.Columns)
            {
                query += $"{col.Value}, ";
            }

            query = query.Remove(query.Length - 2, 2);
            query += ")";
            return query;
        }



        /// <summary>
        /// Возращает значения Select запроса
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <returns>Колекцию наборов данных</returns>
        public static List<List<string>> GetData(SelectRequest request)
        {
            var query = GetQuery(request);
            var model = new List<List<string>>();

            var command = new NpgsqlCommand(query, DataModel.Get());
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new List<string>();

                for (int i = 0; i < request.ColumnsName.Length; i++)
                {
                    item.Add(reader.GetValue(i).ToString());
                }
                model.Add(item);
            }

            reader.Close();
            return model;
        }

        /// <summary>
        /// Выполняет SQL запрос
        /// </summary>
        /// <param name="query">Строка запроса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public static bool Complite(string query)
        {
            var command = new NpgsqlCommand(query , DataModel.Get());

            if(command.ExecuteNonQuery() == 1)
            {
                return true;
            }

            return false;
        }

        
    }
}
