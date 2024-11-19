using System.Windows.Forms;
using System;
using UASKI.Helpers;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Models.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра исполнителей
    /// </summary>
    public class SelectIsp : BasePage
    {
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public SelectIsp(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            SystemHelper.PullListInDataGridView(form.IspDataGridView
                        , IspService.GetListByDataGrid(IspService.GetList())
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));

            FilterClose();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        protected override void Clear()
        {
            form.textBox13.Clear();
            form.IspDataGridView.DataSource = null;
        }

        /// <summary>
        /// Выход с страницы
        /// </summary>
        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();

        }

        /// <summary>
        /// Открывает панель фильтров
        /// </summary>
        public void FilterOpen()
        {
            form.IspDataGridView.Location = new System.Drawing.Point(247, 0);
            form.IspDataGridView.Size = new System.Drawing.Size(634, 560);
            SystemHelper.ResizeDataGridView(form.IspDataGridView);
            form.panel12.Visible = true;
            SystemHelper.SelectTextBox(form.textBox13);
            form.button19.Visible = false;
        }

        /// <summary>
        /// Закрывает панель фильтров
        /// </summary>
        public void FilterClose()
        {
            form.IspDataGridView.Location = new System.Drawing.Point(20, 0);
            form.IspDataGridView.Size = new System.Drawing.Size(861, 560);
            SystemHelper.ResizeDataGridView(form.IspDataGridView);
            form.panel12.Visible = false;
            form.button19.Visible = true;
            SystemHelper.SelectDataGridView(true, form.IspDataGridView);
        }

        #region Клавиши
        public void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.IspDataGridView.SelectedRows.Count != 0
                && form.IspDataGridView.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
                form.IspDataGridView.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (form.IspDataGridView.SelectedRows.Count > 0)
                {
                    var code = Convert.ToInt32(form.IspDataGridView.SelectedRows[0].Cells[0].Value);
                    SystemData.Pages.EditIsp.Init(false);
                    SystemData.Pages.EditIsp.Show(code);
                }
            }
            else if(e.KeyCode == SystemData.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox13, e.KeyCode);
            }

        }

        public void textBox13_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
        }
        #endregion

    }
}
