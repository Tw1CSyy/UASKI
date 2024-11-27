using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
        public Font Font { get; set; }

        /// <summary>
        /// Аргумент события печати
        /// </summary>
        public PrintPageEventArgs Argument { get; set; }

        /// <summary>
        /// Список заголовков документа
        /// </summary>
        public string[] Headers { get; set; }

        /// <summary>
        /// DataGridView откуда берутся данные
        /// </summary>
        public DataGridView DataGridView { get; set; }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        public PrintModel(Font font , PrintPageEventArgs e , DataGridView d , params string[] ars)
        {
            Font = font;
            Argument = e;
            Headers = ars;
            DataGridView = d;
        }
    }
}
