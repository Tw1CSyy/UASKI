using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintPoc : BasePagePrint
    {
        public PrintPoc(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.dateTimePicker12.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker13.Value = DateTime.Today;
            form.dateTimePicker12.Focus();
        }

        protected override void Clear()
        {
            SystemHelper.SelectButton(false, form.button40);
            form.dataGridView10.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override void Select()
        {

            form.dataGridView10.ClearSelection();
        }

        protected override void Print()
        {
            
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {

        }

        #region Клавиши
        public void dateTimePicker12_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                form.dateTimePicker13.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView10);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker12, form.dateTimePicker13);
                f.Show();
            }
        }

        public void dateTimePicker13_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker12.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker12, form.dateTimePicker13);
                f.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView10);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button40);
                e.Handled = true;
            }
        }

        public void button40_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                SystemHelper.SelectButton(false, form.button40);
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (SystemHelper.SelectDataGridView(true, form.dataGridView10))
                    SystemHelper.SelectButton(false, form.button40);

                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker13.Focus();
                SystemHelper.SelectButton(false, form.button40);
                e.IsInputKey = true;
            }
        }

        public void dataGridView10_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView10.SelectedRows.Count != 0
                && form.dataGridView10.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker12.Focus();
                SystemHelper.SelectDataGridView(false, form.dataGridView10);
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView10, e.KeyCode);
                e.Handled = true;
            }
        }
        #endregion
    }
}
