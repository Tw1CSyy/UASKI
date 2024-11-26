using System.Windows.Forms;
using UASKI.Forms;
using System;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using System.Collections.Generic;
using System.Linq;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра задач
    /// </summary>
    public class SelectTask : BasePageSelect
    {
        public SelectTask(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.panel16.Visible = form.checkBox2.Checked = false;
            form.dateTimePicker5.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker6.Value = DateTime.Today;
            Select();
            FilterClose();
        }

        protected override void Clear()
        {
            form.textBox19.Clear();
            form.textBox29.Clear();
            form.dataGridView3.DataSource = null;
        }

        public override void Select()
        {
            var list = TasksService.GetList(form.textBox19.Text, form.textBox29.Text, form.checkBox2.Checked, form.dateTimePicker5.Value, form.dateTimePicker6.Value);

            var model = new List<DataGridRowModel>();
            var listUser = IspService.GetList();

            foreach (var item in list.OrderByDescending(c => c.Date))
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                var st = new DataGridRowModel(item.Code, $"{isp.FirstName} {isp.Name} {isp.LastName}", $"{con.FirstName} {con.Name} {con.LastName}", item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            Select(form.dataGridView3,
                        model,
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
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
                Exit();
                form.dataGridView3.ClearSelection();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView3.SelectedRows.Count > 0)
            {
                var code = form.dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(code, false , 1);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView3, e.KeyCode);
                e.Handled = true;
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
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox29);
                e.Handled = true;
            }
        }

        public void textBox29_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.checkBox2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox19);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox(), new TextBox(), form.textBox29);
                f.Show();
                e.Handled = true;
            }
        }

        public void checkBox2_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                form.checkBox2.Checked = !form.checkBox2.Checked;
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Down && form.panel16.Visible)
            {
                form.dateTimePicker5.Focus();
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox29);
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.IsInputKey = true;
            }

        }

        public void dateTimePicker5_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker6.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.checkBox2.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                f.Show();
                e.Handled = true;
            }
        }

        public void dateTimePicker6_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker5.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right|| e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                f.Show();
                e.Handled = true;
            }
        }

        #endregion

    }
}
