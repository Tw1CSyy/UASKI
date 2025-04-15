using System;
using System.Windows.Forms;

namespace UASKI.Models.Elements
{
    /// <summary>
    /// Модель для ТекстБокса
    /// </summary>
    public class TextBoxElement : BaseElement
    {
        /// <summary>
        /// ТекстБокс
        /// </summary>
        public TextBox TextBox { get; private set; }

        /// <summary>
        /// Значение элемента
        /// </summary>
        public string Value { get => TextBox.Text; }

        /// <summary>
        /// Число ли значение элемента
        /// </summary>
        public bool IsNumber { get => IsInt(); }

        /// <summary>
        /// Пустое значение - true
        /// </summary>
        public bool IsNull { get => IsNul(); }

        /// <summary>
        /// Число, эквивалетное строке или -1
        /// </summary>
        public int Num { get => GetInt(); }

        /// <summary>
        /// Провереят число ли значение элемента
        /// </summary>
        /// <returns></returns>
        private bool IsInt()
        {
            if (!int.TryParse(Value, out int i))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет на пустое значение
        /// </summary>
        /// <returns></returns>
        private bool IsNul()
        {
            return string.IsNullOrEmpty(Value);
        }

        /// <summary>
        /// Возвращает число, эквивалетное строке или -1
        /// </summary>
        private int GetInt()
        {
            if (!IsNull && IsNumber)
                return Convert.ToInt32(Value);
            else
                return -1;
        }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="text">TextBox</param>
        /// <param name="label">Label ошибки</param>
        private TextBoxElement(TextBox text, Label label)
        {
            TextBox = text;
            ErrorLabel = label;
        }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="text">TextBox</param>
        /// <param name="label">Label ошибки</param>
        public static TextBoxElement New(TextBox text, Label label)
        {
            return new TextBoxElement(text, label);
        }
    }
}
