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
        public TextBox TextBox { get; set; }

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
        /// Отчищает элемент и Label ошибки
        /// </summary>
        public new void Dispose()
        {
            ErrorLabel.Visible = false;
            TextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="text">TextBox</param>
        /// <param name="label">Label ошибки</param>
        public TextBoxElement(TextBox text , Label label)
        {
            TextBox = text;
            ErrorLabel = label;
        }
    }
}
