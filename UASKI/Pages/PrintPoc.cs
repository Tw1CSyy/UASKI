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
using UASKI.Core.SystemModels;

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
            SelectButton(form.button40, false);
            form.DataGridView10.d.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override void Select()
        {
            var ispList = IspModel.GetList();
           
            var result = new List<KofModel>();

            foreach (var isp in ispList.OrderBy(c => c.CodePodr))
            {
                var item = isp.GetKofModel(form.dateTimePicker12.Value, form.dateTimePicker13.Value);
                result.Add(item);
            }

            var dataRowModels = result.Select(c => new DataGridRowModel(
                c.Isp,
                c.CountPeriod.ToString(),
                c.CountOpzPeriod.ToString(),
                c.CountDayPeriod.ToString(),
                c.KofPeriod.ToString()
                )).ToList();

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Кол-во Выполненных Заданий" , typeof(int)),
                new DataGridColumnModel("Кол-во Случаев Опзд" , typeof(int)),
                new DataGridColumnModel("Кол-во Дней Опзд" , typeof(int)),
                new DataGridColumnModel("Кооф" , typeof(float))
            };

            form.DataGridView10.PullListInDataGridView(dataRowModels.ToArray(), columns);

            form.DataGridView10.d.ClearSelection();
        }

        protected override void Print()
        {
            if (form.DataGridView10.d.Columns.Count == 0)
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
            string header1 = $"Текущие значения показателей работы подразделений";
            string header2 = $"C {form.dateTimePicker12.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker13.Value.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.DataGridView10.d, header1, header2);
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
                SelectDataGridView(form.DataGridView10.d);
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
                SelectDataGridView(form.DataGridView10.d);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton(form.button40);
                e.Handled = true;
            }
        }

        public void button40_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                SelectButton(form.button40, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if (SelectDataGridView(form.DataGridView10.d))
                    SelectButton(form.button40, false);

            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker13.Focus();
                SelectButton(form.button40, false);
            }
            else if (e.KeyCode == Keys.Enter)
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

        public void dataGridView10_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.DataGridView10.d.SelectedRows.Count != 0
                && form.DataGridView10.d.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker12.Focus();
                SelectDataGridView(form.DataGridView10.d, false);
                e.Handled = true;
            }
            else
            {
                form.DataGridView10.KeyDown(e);
                e.Handled = true;
            }
        }
        #endregion
    }
}
