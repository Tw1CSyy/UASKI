using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Enums;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintTaskList : BasePagePrint
    {
        public PrintTaskList(int index, TypePage type) : base(index, type, Ai.Form.DataGridView7) { }
       
        protected override void Show()
        {
            form.dateTimePicker10.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker11.Value = DateTime.Today;

            if (form.textBox20.Text.Length > 0 && form.textBox30.Text.Length > 0)
            {
                Select();
            }

            if (form.DataGridView7.d.Rows.Count != 0)
                form.DataGridView7.d.Focus();
            else
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
            var isps = IspModel.GetList();

            var taskList = new List<TaskModel>();
            var arhivList = new List<ArhivModel>();

            if (int.TryParse(ispCode, out int id))
            {
                var isp = isps.FirstOrDefault(c => c.CodePodr == id);

                if (isp != null)
                {
                    taskList = TaskModel.GetList()
                        .Where(c => c.IdIsp == isp.Code)
                        .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
                        .ToList();

                    arhivList = ArhivModel.GetList()
                        .Where(c => c.IdIsp == isp.Code)
                        .Where(c => c.Date >= dateFrom && c.Date <= dateTo.Date)
                        .ToList();
                }
                else
                    form.DataGridView7.d.DataSource = null;
            }
            else
                form.DataGridView7.d.DataSource = null;

            var result = new List<DataGridRowModel>();
           
            foreach (var task in taskList)
            {
                var item = new DataGridRowModel(
                    task.Id.ToString(),
                    task.GetCode(),
                    task.Date.ToString("dd.MM.yyyy"),
                    task.GetCon(isps).InizByCode);

                result.Add(item);
            }

            foreach (var task in arhivList)
            {
                var item = new DataGridRowModel(
                    task.Id.ToString(),
                    task.GetCode(),
                    task.Date.ToString("dd.MM.yyyy"),
                    task.GetCon(isps).InizByCode,
                    task.DateClose.ToString("dd.MM.yyyy"),
                    task.Otm.ToString());

                result.Add(item);
            }

            // Сортируем по дате
            result = result.OrderBy(c => Convert.ToDateTime(c.Values[2])).ThenBy(c => Convert.ToInt32(c.Values[0])).ToList();

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("" , false),
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Срок исполнения"),
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Дата закрытия"),
                new DataGridColumnModel("Оценка", typeof(int))
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
                form.DataGridView7.ClearTag();
                Ai.Settings.CountPrint++;
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
           
            string header1 = $"Перечень заданий с {form.dateTimePicker10.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker11.Value.ToString("dd.MM.yyyy")}";
            string header2 = $"Исполнитель {form.textBox30.Text} {form.textBox20.Text}";

            var model = new PrintModel(font, e, form.DataGridView7.d, header1, header2);
            SystemHelper.PrintDocument(model, FirstPage);
            FirstPage = false;
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
            else if(e.KeyCode == Ai.ActionKey)
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
            else if (e.KeyCode == Ai.ActionKey)
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
            else if(e.KeyCode == Ai.ActionKey)
            {
                var f = new IspForm(form.textBox20, form.textBox30, new TextBox());
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
                Print();
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
            else if(e.KeyCode == Keys.Enter && form.DataGridView7.d.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(form.DataGridView7.d.SelectedRows[0].Cells[0].Value);
                bool isArhiv = form.DataGridView7.d.SelectedRows[0].Cells[5].Value.ToString().Length > 0;
                Ai.Pages.EditTask.Init(false, false);
                Ai.Pages.EditTask.Show(id, isArhiv);
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
