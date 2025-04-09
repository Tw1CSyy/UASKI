using System.Windows.Forms;
using UASKI.Models.Components;

namespace UASKI.Models
{
    public abstract class BasePageSelect : BasePage
    {
        public BasePageSelect(int index) : base(index) { }

        public abstract DataGridViewComponent DataGridView { get; protected set; }

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
        protected void FilterOpen(DataGridViewComponent d , Panel p , TextBox t , Button but)
        {
            d.d.Location = new System.Drawing.Point(247, 0);
            d.d.Size = new System.Drawing.Size(form.tabControl1.Width - 255, form.tabControl1.Height - 9);
            p.Visible = true;
            SelectTextBox(t);
            but.Visible = false;
        }

        /// <summary>
        /// Закрывает окно фильтров
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="p">Panel фильтров</param>
        /// <param name="t">Первый TextBox для фокуса</param>
        /// <param name="but">Кнопка открытия панели</param>
        protected void FilterClose(DataGridViewComponent d , Panel p , TextBox t , Button but)
        {
            d.d.Location = new System.Drawing.Point(20, 0);
            d.d.Size = new System.Drawing.Size(form.tabControl1.Width - 28, form.tabControl1.Height - 9);
            p.Visible = false;
            d.d.Focus();
            but.Visible = true;
        }
    }
}
