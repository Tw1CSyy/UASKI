using System;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра опозданий
    /// </summary>
    public class SelectOpz : BasePageSelect
    {
        public SelectOpz(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            FilterClose();
            form.checkBox3.Checked = true;
            form.dateTimePicker7.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker8.Value = DateTime.Today;
            form.panel18.Visible = true;
            Select();
            form.dataGridView1.Focus();
        }

        protected override void Clear()
        {
            form.dataGridView1.DataSource = null;
            form.textBox33.Clear();
            form.textBox34.Clear();
        }

        public override void Select()
        {
            Select(form.dataGridView1,
                ArhivService.GetOpzListDataGrid(form.textBox33.Text, form.textBox34.Text, form.checkBox3.Checked, form.dateTimePicker7.Value, form.dateTimePicker8.Value),
                new DataGridRowModel("Код", "Исполнитель", "Котроллер", "Срок", "Дата закрытия", "Оценка"));
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            form.dataGridView1.ClearSelection();
        }

        #region Клаваши
        public void dataGridView1_KeyDown_1(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == SystemData.ActionKey)
            {
                FilterOpen();
                SystemHelper.SelectTextBox(form.textBox33);
            }
            else if ((e.KeyCode == Keys.Up
                && form.dataGridView1.SelectedRows.Count != 0
                && form.dataGridView1.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                var d = form.dataGridView1;

                if (d.SelectedRows.Count > 0)
                {
                    var code = d.SelectedRows[0].Cells[0].Value.ToString();
                    var IsArhiv = d.SelectedRows[0].Cells[5].Value != null && !string.IsNullOrEmpty(d.SelectedRows[0].Cells[5].Value.ToString());
                    SystemData.Pages.EditTask.Init(false);
                    SystemData.Pages.EditTask.Show(code, IsArhiv , 3);
                }
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView1, e.KeyCode);
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox33, e.KeyCode);
            }

        }
        public void textBox33_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox34);
            }
            
        }
        public void textBox34_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.checkBox3.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox33);
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox() , new TextBox() , form.textBox34);
                f.Show();
            }
        }

        public void checkBox3_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Down && form.panel18.Visible)
            {
                form.dateTimePicker7.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox34);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                form.checkBox3.Checked = !form.checkBox3.Checked;
            }

            e.IsInputKey = true;
        }
        public void dateTimePicker7_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker8.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.checkBox3.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                f.Show();
            }
            e.Handled = true;
        }
        public void dateTimePicker8_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker7.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                f.Show();
            }
            e.Handled = true;
        }
        #endregion
    }
}
