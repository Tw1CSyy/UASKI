using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintCof : BasePagePrint
    {
        public PrintCof(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            SystemHelper.SelectTextBox(form.textBox36);
            form.dateTimePicker14.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker15.Value = DateTime.Today;
            form.textBox36.Focus();
        }

        protected override void Clear()
        {
            form.textBox36.Clear();
            form.textBox37.Clear();
            SystemHelper.SelectButton(false, form.button42);
            form.dataGridView11.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override void Select()
        {
            form.dataGridView11.ClearSelection();
        }

        protected override void Print()
        {
            
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            
        }

        #region Клавиши
        public void textBox36_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button42);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker14.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(form.textBox37, new TextBox(), form.textBox36);
                f.Show();
                e.Handled = true;
            }
        }

        public void dateTimePicker14_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                form.dateTimePicker15.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView11);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker14, form.dateTimePicker15);
                f.Show();
                e.Handled = true;
            }
        }

        public void dateTimePicker15_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button42);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView11);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker14, form.dateTimePicker15);
                f.Show();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker14.Focus();
                e.Handled = true;
            }
        }

        public void button42_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                SystemHelper.SelectButton(false, form.button42);
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker15.Focus();
                SystemHelper.SelectButton(false, form.button42);
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SystemHelper.SelectDataGridView(true , form.dataGridView11))
                    SystemHelper.SelectButton(false, form.button42);
                e.IsInputKey = true;
            }
        }

        public void dataGridView11_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView11.SelectedRows.Count != 0
                && form.dataGridView11.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker14.Focus();
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView11, e.KeyCode);
                e.Handled = true;
            }
        }
        #endregion
    }
}
