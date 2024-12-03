using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
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
            form.dateTimePicker10.Focus();
            
        }

        protected override void Clear()
        {
            form.textBox20.Clear();
            form.textBox30.Clear();
            form.dataGridView7.DataSource = null;
            SystemHelper.SelectButton(form.button34, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SystemHelper.SelectButton(form.button34, false);
        }

        public override void Select()
        {
            var dateFrom = form.dateTimePicker10.Value;
            var dateTo = form.dateTimePicker11.Value;
            var ispCode = form.textBox30.Text;

            var taskList = TasksService.GetList()
                .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
                .ToList();

            var arhivList = ArhivService.GetList()
                .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
            .ToList();

            if (int.TryParse(ispCode, out int id))
            {
                taskList = taskList.Where(c => c.IdIsp == id).ToList();
                arhivList = arhivList.Where(c => c.IdIsp == id).ToList();
            }

            var result = new List<DataGridRowModel>();
            var ispList = IspService.GetList();

            foreach (var task in taskList)
            {
                var con = IspService.GetByCode(task.IdCon, ispList);
                var item = new DataGridRowModel(
                    task.Code,
                    task.Date.ToString("dd.MM.yyyy"),
                    IspService.GetIniz(con));

                result.Add(item);
            }

            foreach (var task in arhivList)
            {
                var con = IspService.GetByCode(task.IdCon, ispList);
                var item = new DataGridRowModel(
                    task.Code,
                    task.Date.ToString("dd.MM.yyyy"),
                   IspService.GetIniz(con),
                    task.DateClose.ToString("dd.MM.yyyy"),
                    task.Otm.ToString());

                result.Add(item);
            }

            Select(form.dataGridView7,
                result,
                new DataGridRowModel("Код задания" , "Срок исполнения" , "Код контролера" , "Дата закрытия" , "Оценка"));
            form.dataGridView7.ClearSelection();
        }

        protected override void Print()
        {
            if(form.dataGridView7.Columns.Count == 0)
            {
                ErrorHelper.StatusError();
            }  
            else
            {
                var printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                GetPrint(printDocument);
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            string header1 = $"Перечень заданий с {form.dateTimePicker10.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker11.Value.ToString("dd.MM.yyyy")}";
            string header2 = $"Исполнитель {form.textBox30.Text} {form.textBox20.Text}";

            var model = new PrintModel(font, e, form.dataGridView7, header1, header2);
            SystemHelper.PrintDocument(model);
        }

        #region Клавиши
        public void dateTimePicker10_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox30);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                form.dateTimePicker11.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker10, form.dateTimePicker11);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker10);
                    f.Show();
                    e.Handled = true;
                }
            }
        }

        public void dateTimePicker11_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox30);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button34);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker10, form.dateTimePicker11);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker11);
                    f.Show();
                    e.Handled = true;
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker10.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox30_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker10.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(form.button34);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(form.textBox20 , new TextBox() , form.textBox30);
                f.Show();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(form.dataGridView7);
                e.Handled = true;
            }
        }

        public void button34_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox30);
                SystemHelper.SelectButton(form.button34, false);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker11.Focus();
                SystemHelper.SelectButton(form.button34, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SystemHelper.SelectDataGridView(form.dataGridView7))
                SystemHelper.SelectButton(form.button34, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    Print();
                }
                else
                    ErrorHelper.StatusQuery();

            }

            e.IsInputKey = true;
        }

        public void dataGridView7_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView7.SelectedRows.Count != 0
                && form.dataGridView7.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectTextBox(form.textBox30);
                SystemHelper.SelectDataGridView(form.dataGridView7, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button34);
                SystemHelper.SelectDataGridView(form.dataGridView7, false);
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView7, e.KeyCode);
                e.Handled = true;
            }
        }
        #endregion
    }
}
