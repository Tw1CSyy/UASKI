using System;
using System.Windows.Forms;

namespace UASKI.Models.Elements
{
    /// <summary>
    /// Модель для DateTimePicker
    /// </summary>
    public class DateTimeElement : BaseElement
    {
        /// <summary>
        /// DateTimePicker
        /// </summary>
        public DateTimePicker DateTimePicker { get; set; }

        /// <summary>
        /// Значение элемента
        /// </summary>
        public DateTime Value { get => DateTimePicker.Value; }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="date">DateTimePicker</param>
        /// <param name="label">Label ошибки</param>
        public DateTimeElement(DateTimePicker date , Label label)
        {
            DateTimePicker = date;
            ErrorLabel = label;
        }
    }
}
