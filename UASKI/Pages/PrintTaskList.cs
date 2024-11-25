using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintTaskList : BasePagePrint
    {
        public PrintTaskList(int index) : base(index) { }
        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.dateTimePicker10.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker11.Value = DateTime.Today;
            Select();
            form.dateTimePicker10.Focus();

        }

        protected override void Clear()
        {
            form.textBox20.Clear();
            form.textBox30.Clear();
            form.dataGridView7.DataSource = null;
            SystemHelper.SelectButton(false, form.button34);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SystemHelper.SelectButton(false, form.button34);
        }

        public override void Select()
        {
            Select(form.dataGridView7,
                TasksService.GetModelPrintTaskList(form.dateTimePicker10.Value, form.dateTimePicker11.Value, form.textBox30.Text),
                new DataGridRowModel("Код задания" , "Срок исполнения" , "Код контролера" , "Дата закрытия" , "Оценка"));
        }

        protected override void Print()
        {
            
        }

        #region Клаваши
        public void dateTimePicker10_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox30);
            }
            else if(e.KeyCode == Keys.Right)
            {
                form.dateTimePicker11.Focus();
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker10, form.dateTimePicker11);
            }
        }

        public void dateTimePicker11_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox30);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button34);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker10, form.dateTimePicker11);
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker10.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void textBox30_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker10.Focus();
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button34);
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(form.textBox20 , new TextBox() , form.textBox30);
                f.Show();
            }
        }

        public void button34_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox30);
                SystemHelper.SelectButton(false, form.button34);
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
            }
            
        }

        #endregion
    }
}
