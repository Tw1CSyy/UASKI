using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UASKI.Models.Elements
{
    /// <summary>
    /// Модель для MonthCalendar
    /// </summary>
    public class MonthElement : BaseElement
    {
        /// <summary>
        /// MonthCalendar
        /// </summary>
        public MonthCalendar MonthCalendar { get; private set; }

        /// <summary>
        /// Первая дата
        /// </summary>
        public DateTime Date { get => MonthCalendar.SelectionRange.Start.Date; }

        /// <summary>
        /// Список дат в указанном диапозоне
        /// </summary>
        public List<DateTime> DateRange { get => GetRange(); }

        /// <summary>
        /// Возращает список дат из диапазона
        /// </summary>
        private List<DateTime> GetRange()
        {
            var result = new List<DateTime>();

            for(var date = Date; date <= MonthCalendar.SelectionRange.End;)
            {
                result.Add(date.Date);
                date = date.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// Возращает модель класса
        /// </summary>
        /// <param name="calendar">MonthCalendar</param>
        /// <param name="error">Label ошибки</param>
        private MonthElement(MonthCalendar calendar, Label error)
        {
            MonthCalendar = calendar;
            ErrorLabel = error;
        }

        /// <summary>
        /// Возращает модель класса
        /// </summary>
        /// <param name="calendar">MonthCalendar</param>
        /// <param name="error">Label ошибки</param>
        public static MonthElement New(MonthCalendar calendar, Label error)
        {
            return new MonthElement(calendar, error);
        }
    }
}
