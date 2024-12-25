using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Core.Models;

namespace UASKI.Pages
{
    public class PrintTaskList : BasePagePrint
    {
        public PrintTaskList(int index) : base(index) { }
       
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
            form.DataGridView7.d.DataSource = null;
            SelectButton(form.button34, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SelectButton(form.button34, false);
        }

        public override void Select()
        {
            var dateFrom = form.dateTimePicker10.Value;
            var dateTo = form.dateTimePicker11.Value;
            var ispCode = form.textBox30.Text;

            var taskList = TaskModel.GetList()
                .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
                .ToList();

            var arhivList = ArhivModel.GetList()
                .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
            .ToList();

            if (int.TryParse(ispCode, out int id))
            {
                taskList = taskList.Where(c => c.IdIsp == id).ToList();
                arhivList = arhivList.Where(c => c.IdIsp == id).ToList();
            }

            var result = new List<DataGridRowModel>();
           
            foreach (var task in taskList)
            {
                var item = new DataGridRowModel(
                    task.Code,
                    task.Date.ToString("dd.MM.yyyy"),
                    task.Con.InizByCode);

                result.Add(item);
            }

            foreach (var task in arhivList)
            {
                var item = new DataGridRowModel(
                    task.Code,
                    task.Date.ToString("dd.MM.yyyy"),
                    task.Con.InizByCode,
                    task.DateClose.ToString("dd.MM.yyyy"),
                    task.Otm.ToString());

                result.Add(item);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Срок исполнения" , typeof(DateTime)),
                new DataGridColumnModel("Код контролера"),
                new DataGridColumnModel("Дата закрытия" , typeof(DateTime)),
                new DataGridColumnModel("Оценка" , typeof(int))
            };

            form.DataGridView7.PullListInDataGridView(result.ToArray(), columns);
        }

        protected override void Print()
        {
            if(form.DataGridView7.d.Columns.Count == 0)
            {
                Ai.Error();
            }  
            else
            {
                var printDocument = new PrintDocument();
                printDocument.DefaultPageSettings.Landscape = true;
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                GetPrint(printDocument);
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            string header1 = $"Перечень заданий с {form.dateTimePicker10.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker11.Value.ToString("dd.MM.yyyy")}";
            string header2 = $"Исполнитель {form.textBox30.Text} {form.textBox20.Text}";

            var model = new PrintModel(font, e, form.DataGridView7.d, header1, header2);
            SystemHelper.PrintDocument(model);
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }
        #region Клавиши
        public void dateTimePicker10_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectTextBox(form.textBox30);
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
                SelectTextBox(form.textBox30);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button34);
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
                SelectButton(form.button34);
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
                SelectDataGridView(form.DataGridView7.d);
                e.Handled = true;
            }
        }

        public void button34_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox30);
                SelectButton(form.button34, false);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker11.Focus();
                SelectButton(form.button34, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SelectDataGridView(form.DataGridView7.d))
                SelectButton(form.button34, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    Print();
                }
                else
                    Ai.Query();

            }

            e.IsInputKey = true;
        }

        public void dataGridView7_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.DataGridView7.d.SelectedRows.Count != 0
                && form.DataGridView7.d.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                SelectTextBox(form.textBox30);
                SelectDataGridView(form.DataGridView7.d, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton(form.button34);
                SelectDataGridView(form.DataGridView7.d, false);
                e.Handled = true;
            }
            else
            {
                form.DataGridView7.KeyDown(e);
                e.Handled = true;
            }
        }
        #endregion
    }
}
