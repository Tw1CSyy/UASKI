using System.Drawing;
using System.Windows.Forms;

namespace UASKI.Models
{
    /// <summary>
    /// Абстрактный класс для передачи данных на валидацию
    /// </summary>
    public abstract class BaseElement
    {

        /// <summary>
        /// Label для вывода ошибки для элемента
        /// </summary>
        public Label ErrorLabel { get; protected set; }

        /// <summary>
        /// Отключает видимость текста с ошибкой
        /// </summary>
        public void Dispose()
        {
            ErrorLabel.Visible = false;
        }

        /// <summary>
        /// Выводит ошибку
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        public void Error(string text)
        {
            if(!ErrorLabel.Visible)
            {
                ErrorLabel.ForeColor = Color.Red;
                ErrorLabel.Visible = true;
                ErrorLabel.Text = text;
            }
        }
    }
}
