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
        public int Id { get; set; }

        /// <summary>
        /// Дата праздника
        /// </summary>
        public DateTime Date { get; set; }
    }
}
