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
        /// Возращает значения Select запроса
        /// </summary>
        /// <param name="request">Модель запроса</param>
        /// <returns>Колекцию наборов данных</returns>
        public static List<List<string>> SelectValues(SelectRequest request)
        {
            var data = new DataModel();
            var query = GetQuery(request);
            var model = new List<List<string>>();

            data.Open();
            var command = new NpgsqlCommand(query, data.Get());
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
            data.Close();
            return model;
        }
    }
}
