using System.Drawing;
using System.Windows.Forms;

namespace UASKI.Models
{
    /// <summary>
    /// Класс для передачи данных на валидацию
    /// </summary>
    public abstract class BaseElement
    {
        /// <summary>
        /// Цвет заднего плана при ошибке
        /// </summary>
        public readonly Color ErrorColor = Color.Tomato;

        /// <summary>
        /// Label для вывода ошибки для элемента
        /// </summary>
        public Label ErrorLabel { get; set; }

        /// <summary>
        /// Фокусировать ли данный элемент 
        /// </summary>
        public bool IsSelected { get; set; } = false;

        /// <summary>
        /// Открючает видимость текста с ошибкой
        /// </summary>
        public void Dispose()
        {
            ErrorLabel.Visible = false;
        }
    }
}
