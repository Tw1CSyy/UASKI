using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра архива заданий
    /// </summary>
    public class SelectArhiv : BasePage
    {
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public SelectArhiv(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            var list = ArhivService.GetList();

            SystemHelper.PullListInDataGridView(form.dataGridView5,
                ArhivService.GetListByDataGrid(list),
                new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка"));

            form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker3.Value = DateTime.Today;
            form.panel15.Visible = false;
            form.checkBox1.Checked = false;

            FilterClose();

            form.dataGridView5.Focus();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        protected override void Clear()
        {
           
            form.textBox31.Clear();
            form.textBox32.Clear();

            form.panel15.Visible = false;
            form.checkBox1.Checked = false;

            form.dataGridView5.DataSource = null;
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
        private void FilterOpen()
        {
            form.dataGridView5.Location = new System.Drawing.Point(247, 0);
            form.dataGridView5.Size = new System.Drawing.Size(634, 560);
            SystemHelper.ResizeDataGridView(form.dataGridView5);
            form.panel14.Visible = true;
            SystemHelper.SelectTextBox(form.textBox32);
            form.button16.Visible = false;
        }

        /// <summary>
        /// Закрывает панель фильтров
        /// </summary>
        private void FilterClose()
        {
            form.dataGridView5.Location = new System.Drawing.Point(20, 0);
            form.dataGridView5.Size = new System.Drawing.Size(861, 560);
            SystemHelper.ResizeDataGridView(form.dataGridView5);
            form.panel14.Visible = false;
            form.dataGridView5.Focus();
            form.button16.Visible = true;
        }

        #region Клавиши
        public void dataGridView5_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.dataGridView5.SelectedRows.Count != 0
                && form.dataGridView5.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
                form.dataGridView5.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView5.SelectedRows.Count > 0)
            {
                var code = form.dataGridView5.SelectedRows[0].Cells[0].Value.ToString();

                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(code, true , 2);
            }
            else if(e.KeyCode == Keys.Left || e.KeyCode == SystemData.ActionKey)
            {
                FilterOpen();
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox32, e.KeyCode);
            }
        }


        public void textBox32_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox31);
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
        }

        public void textBox31_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox31);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox32);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox() , new TextBox() , form.textBox31);
                f.Show();
            }
        }

        public void checkBox1_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox1.Checked = !form.checkBox1.Checked;
                form.panel15.Visible = form.checkBox1.Checked;
            }
            else if (e.KeyCode == Keys.Down && form.panel15.Visible)
            {
                form.dateTimePicker2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox31);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker2_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker3.Focus();
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.checkBox1.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                f.Show();
            }
        }

        public void dateTimePicker3_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker2.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                f.Show();
            }
        }
        #endregion

    }
}
