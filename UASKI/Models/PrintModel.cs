using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace UASKI.Models
{
    /// <summary>
    /// Модель для передачи документа на печать
    /// </summary>
    public class PrintModel
    {
        /// <summary>
        /// Шрифт основного текста
        /// </summary>
        public Font Font { get; private set; }

        /// <summary>
        /// Аргумент события печати
        /// </summary>
        public PrintPageEventArgs Argument { get; private set; }

        /// <summary>
        /// Список заголовков документа
        /// </summary>
        public string[] Headers { get; private set; }

        /// <summary>
        /// DataGridView откуда берутся данные
        /// </summary>
        public DataGridView DataGridView { get; private set; }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        public PrintModel(Font font, PrintPageEventArgs e, DataGridView d, params string[] ars)
        {
            Font = font;
            Argument = e;
            Headers = ars;
            DataGridView = d;
        }
    }
}
