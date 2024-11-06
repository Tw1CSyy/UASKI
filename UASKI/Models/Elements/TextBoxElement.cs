using System.Drawing;
using System.Runtime.CompilerServices;
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
        /// Создает модель класса
        /// </summary>
        /// <param name="text">TextBox</param>
        /// <param name="label">Label ошибки</param>
        private TextBoxElement(TextBox text , Label label)
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
