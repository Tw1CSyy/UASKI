using System;

namespace UASKI.Data.Entityes
{
    /// <summary>
    /// Модель элемента таблицы Holidays
    /// </summary>
    public class HolidayEntity
    {
        /// <summary>
        /// Идентификатор праздника
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Дата праздника
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="date">Дата</param>
        public HolidayEntity(int id , DateTime date)
        {
            Id = id;
            Date = date;
        }
    }
}
