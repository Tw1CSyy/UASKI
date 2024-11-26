using System;
using System.Collections.Generic;
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
    public class SelectArhiv : BasePageSelect
    {
        public SelectArhiv(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker3.Value = DateTime.Today;
            form.panel15.Visible = true;
            form.checkBox1.Checked = true;
            Select();

            FilterClose();

            form.dataGridView5.Focus();
        }

        protected override void Clear()
        {
            form.textBox31.Clear();
            form.textBox32.Clear();
            form.dataGridView5.DataSource = null;
        }

        public override void Select()
        {
            var list = ArhivService.GetList(form.textBox32.Text, form.textBox31.Text, form.checkBox1.Checked, form.dateTimePicker2.Value, form.dateTimePicker3.Value);

            var model = new List<DataGridRowModel>();
            var listUser = IspService.GetList();

            foreach (var item in list.OrderByDescending(c => c.DateClose))
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                var st = new DataGridRowModel(item.Code,
                    $"{isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.",
                    $"{con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}.",
                    item.Date.ToString("dd.MM.yyyy"), item.DateClose.ToString("dd.MM.yyyy"),
                    item.Otm.ToString());

                model.Add(st);
            }

            Select(form.dataGridView5,
            model,
                new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка"));
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView5, form.panel14, form.textBox32, form.button16);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView5, form.panel14, form.textBox32, form.button16);
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
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView5.SelectedRows.Count > 0)
            {
                var code = form.dataGridView5.SelectedRows[0].Cells[0].Value.ToString();

                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(code, true , 2);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Left || e.KeyCode == SystemData.ActionKey)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView5, e.KeyCode);
                e.Handled = true;
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
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
        }

        public void textBox31_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.checkBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox32);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox() , new TextBox() , form.textBox31);
                f.Show();
                e.Handled = true;
            }
        }

        public void checkBox1_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox1.Checked = !form.checkBox1.Checked;
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Down && form.panel15.Visible)
            {
                form.dateTimePicker2.Focus();
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox31);
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.IsInputKey = true;
            }
        }

        public void dateTimePicker2_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker3.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.checkBox1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                f.Show();
                e.Handled = true;
            }
        }

        public void dateTimePicker3_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                f.Show();
                e.Handled = true;
            }
        }
        #endregion

    }
}
