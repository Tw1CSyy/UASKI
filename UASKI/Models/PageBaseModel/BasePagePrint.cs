using System.Drawing.Printing;
using System.Windows.Forms;

using UASKI.Enums;
using UASKI.Models.Components;
using UASKI.StaticModels;

namespace UASKI.Models
{
    public abstract class BasePagePrint : BasePage
    {
        public BasePagePrint(int index, TypePage type) : base(index, type) { }

        /// <summary>
        /// Печатается первая страница
        /// </summary>
        protected bool FirstPage = true;

        /// <summary>
        /// Вывести данные в DataGridView
        /// </summary>
        public abstract void Select();

        /// <summary>
        /// Напечатать данные
        /// </summary>
        protected abstract void Print();

        /// <summary>
        /// Печать страницы
        /// </summary>
        protected abstract void PrintPage(object sender, PrintPageEventArgs e);

        /// <summary>
        /// Показывает окна с настройками печати
        /// </summary>
        /// <param name="document">Документ на печать</param>
        /// <returns></returns>
        protected bool GetPrint(PrintDocument document)
        {
            var form = Ai.Form;

            var printDialog = new PrintDialog();
            printDialog.Document = document;

            if (printDialog.ShowDialog() != DialogResult.OK)
                return false;

            document.Print();
            FirstPage = true;
            return true;
        }
    }
}
