using System.Collections.Generic;

namespace UASKI.Data.Requests
{
    /// <summary>
    /// Класс запроса для Select
    /// </summary>
    public class SelectRequest
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// Поля таблицы
        /// </summary>
        public string[] ColumnsName { get; private set; }


        /// <summary>
        /// Создает объект класса для Select
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <param name="columnsName">Названия полей</param>
        public SelectRequest(string name, params string[] columnsName)
        {
            TableName = name;
            ColumnsName = columnsName;
        }
    }
}
