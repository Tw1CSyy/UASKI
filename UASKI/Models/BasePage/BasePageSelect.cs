using System.Windows.Forms;
using UASKI.Helpers;

namespace UASKI.Models
{
    public abstract class BasePageSelect : BasePage
    {
        public BasePageSelect(int index) : base(index) { }

        /// <summary>
        /// Выводит данные в DataGridView
        /// </summary>
        public abstract void Select();

        /// <summary>
        /// Открывает панель фильтров
        /// </summary>
        protected abstract void FilterOpen();

        /// <summary>
        /// Закрывает окно фильтров
        /// </summary>
        protected abstract void FilterClose();

        /// <summary>
        /// Открывает панель фильтров
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="p">Panel фильтров</param>
        /// <param name="t">Первый TextBox для фокуса</param>
        /// <param name="but">Кнопка открытия панели</param>
        protected void FilterOpen(DataGridView d , Panel p , TextBox t , Button but)
        {
            d.Location = new System.Drawing.Point(247, 0);
            d.Size = new System.Drawing.Size(634, 560);
            SystemHelper.ResizeDataGridView(d);
            p.Visible = true;
            SystemHelper.SelectTextBox(t);
            but.Visible = false;
        }

        /// <summary>
        /// Закрывает окно фильтров
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="p">Panel фильтров</param>
        /// <param name="t">Первый TextBox для фокуса</param>
        /// <param name="but">Кнопка открытия панели</param>
        protected void FilterClose(DataGridView d , Panel p , TextBox t , Button but)
        {
            d.Location = new System.Drawing.Point(20, 0);
            d.Size = new System.Drawing.Size(861, 560);
            SystemHelper.ResizeDataGridView(d);
            p.Visible = false;
            d.Focus();
            but.Visible = true;
        }

    }
}
