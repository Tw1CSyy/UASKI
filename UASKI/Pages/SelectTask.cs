using System.Windows.Forms;
using UASKI.Forms;
using System;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра задач
    /// </summary>
    public class SelectTask : BasePage
    {
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public SelectTask(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            var list = TasksService.GetList();

            SystemHelper.PullListInDataGridView(form.dataGridView3,
                        TasksService.GetListByDataGrid(list),
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));

            form.panel16.Visible = form.checkBox2.Checked = false;

            form.dateTimePicker5.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker6.Value = DateTime.Today;

            FilterClose();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        protected override void Clear()
        {
            form.textBox19.Clear();
            form.textBox29.Clear();
            form.textBox30.Clear();

            form.panel16.Visible = form.checkBox2.Checked = false;

            form.dataGridView3.DataSource = null;
        }

        /// <summary>
        /// Открывает панель фильтров
        /// </summary>
        private void FilterOpen()
        {
            form.dataGridView3.Location = new System.Drawing.Point(247, 0);
            form.dataGridView3.Size = new System.Drawing.Size(634, 560);
            SystemHelper.ResizeDataGridView(form.dataGridView3);
            form.panel13.Visible = true;
            SystemHelper.SelectTextBox(form.textBox19);
        }

        /// <summary>
        /// Закрывает панель фильтров
        /// </summary>
        private void FilterClose()
        {
            form.dataGridView3.Location = new System.Drawing.Point(0, 0);
            form.dataGridView3.Size = new System.Drawing.Size(881, 560);
            SystemHelper.ResizeDataGridView(form.dataGridView3);
            form.panel13.Visible = false;
            form.dataGridView3.Focus();
        }


        #region Клавиши
        public void dataGridView3_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.dataGridView3.SelectedRows.Count != 0
                && form.dataGridView3.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.dataGridView3.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView3.SelectedRows.Count > 0)
            {
                var code = form.dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(code, false);
            }
            else if(e.KeyCode == SystemData.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox19, e.KeyCode);
            }
        }

        public void textBox19_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox29);
            }
        }

        public void textBox29_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox30);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox19);
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox(), new TextBox(), form.textBox29);
                f.Show();
            }
        }

        public void textBox30_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox29);
            }
            else if(e.KeyCode == Keys.Down)
            {
                form.checkBox2.Focus();
            }
        }

        public void checkBox2_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                form.checkBox2.Checked = !form.checkBox2.Checked;
                form.panel16.Visible = form.checkBox2.Checked;
            }
            else if(e.KeyCode == Keys.Down && form.panel16.Visible)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox30);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker5_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker6.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.checkBox2.Focus();
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                f.Show();
            }
        }

        public void dateTimePicker6_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Right|| e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                f.Show();
            }
        }

        #endregion

    }
}
