using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using UASKI.ViewModels;

namespace UASKI.Pages
{
    public class PrintPoc : BasePagePrint
    {
        public PrintPoc(int index) : base(index) { }

        protected override void Show()
        {
            form.dateTimePicker12.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker13.Value = DateTime.Today;
            form.dateTimePicker12.Focus();
            Select();
        }

        protected override void Clear()
        {
            SystemHelper.SelectButton(form.button40, false);
            form.dataGridView10.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override void Select()
        {
            var ispList = IspService.GetList(true);
           
            var result = new List<PrintPocViewModel>();

            foreach (var isp in ispList)
            {
                var item = SystemHelper.GetKofModel(isp, form.dateTimePicker12.Value, form.dateTimePicker13.Value);
                result.Add(item);
            }

            var dataRowModels = result.Select(c => new DataGridRowModel(
                c.Isp,
                c.CountPeriod.ToString() + " / " + c.CountMonth.ToString(),
                c.CountOpzPeriod.ToString() + " / " + c.CountOpzMonth.ToString(),
                c.CountDayPeriod.ToString() + " / " + c.CountDayMonth.ToString(),
                c.KofPeriod.ToString() + " / " + c.KofMonth.ToString()
                )).ToList();

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Кол-во Заданий"),
                new DataGridColumnModel("Кол-во Опзд"),
                new DataGridColumnModel("Кол-во Дней Опзд"),
                new DataGridColumnModel("Кооф")
            };

            SystemHelper.PullListInDataGridView(form.dataGridView10, dataRowModels.ToArray(), columns);

            form.dataGridView10.ClearSelection();
        }

        protected override void Print()
        {
            if (form.dataGridView10.Columns.Count == 0)
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
            string header1 = $"Текущие значения показателей работы подразделений";
            string header2 = $"C {form.dateTimePicker12.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker13.Value.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.dataGridView10, header1, header2);
            SystemHelper.PrintDocument(model);
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
                SystemHelper.SelectDataGridView(form.dataGridView10);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker12, form.dateTimePicker13);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker12);
                    f.Show();
                    e.Handled = true;
                }
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
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker12, form.dateTimePicker13);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker13);
                    f.Show();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(form.dataGridView10);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button40);
                e.Handled = true;
            }
        }

        public void button40_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                SystemHelper.SelectButton(form.button40, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (SystemHelper.SelectDataGridView(form.dataGridView10))
                    SystemHelper.SelectButton(form.button40, false);

            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker13.Focus();
                SystemHelper.SelectButton(form.button40, false);
            }
            else if (e.KeyCode == Keys.Enter)
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

        public void dataGridView10_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView10.SelectedRows.Count != 0
                && form.dataGridView10.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker12.Focus();
                SystemHelper.SelectDataGridView(form.dataGridView10, false);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.DataGridDownSelect(form.dataGridView10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.DataGridUpSelect(form.dataGridView10);
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
