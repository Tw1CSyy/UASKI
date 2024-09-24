using System.Collections.Generic;
using UASKI.Data.Models;

namespace UASKI.Data.Requests
{
    /// <summary>
    /// Класс запроса для Add
    /// </summary>
    public class AddRequest
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Список полей с значениями
        /// </summary>
        public ColumnValueModel[] Columns { get; set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="columns">Список полей - значений</param>
        public AddRequest(string tableName , List<ColumnValueModel> columns)
        {
            TableName = tableName;
            Columns = columns.ToArray();
        }
    }
}
