using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Core.Models;

namespace UASKI.Pages
{
    public class PrintOpz : BasePagePrint
    {
        public PrintOpz(int index) : base(index) { }
       
        protected override void Show()
        {
            Select();
            SelectButton(form.button37);
            SelectDataGridView(form.DataGridView8.d, false);
            form.label77.Text = "На " + DateTime.Today.ToString("dd.MM.yyyy");
        }

        protected override void Clear()
        {
            form.DataGridView8.d.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SelectDataGridView(form.DataGridView8.d, false);
            SelectButton(form.button37, false);
        }

        public override void Select()
        {
            var list = TaskModel.GetList().Where(c => c.DaysOpz != 0).ToList();
            var result = new List<DataGridRowModel>();

            foreach (var item in list)
            {
                var day = item.DaysOpz;

                var model = new DataGridRowModel(
                    item.Isp.InizByCode,
                    item.Code,
                    item.Con.InizByCode,
                    item.Date.ToString("dd.MM.yyyy"),
                    day.ToString());

                result.Add(model);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Код котролера"),
                new DataGridColumnModel("Срок исполнения" , typeof(DateTime)),
                new DataGridColumnModel("Дней опозданий" , typeof(int))
            };

            form.DataGridView8.PullListInDataGridView(result.ToArray(), columns);
        }

        protected override void Print()
        {
            if (form.DataGridView8.d.Columns.Count == 0)
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
            string header1 = $"Не выполнили задание в срок";
            string header2 = $"На {DateTime.Today.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.DataGridView8.d, header1, header2);
            SystemHelper.PrintDocument(model);
        }

        #region Клавиши

        public void button37_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                if (SelectDataGridView(form.DataGridView8.d))
                    SelectButton(form.button37, false);
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
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

        public void dataGridView8_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.DataGridView8.d.SelectedRows.Count != 0
                && form.DataGridView8.d.SelectedRows[0].Index == 0))
            {
                SelectButton(form.button37);
                SelectDataGridView(form.DataGridView8.d, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else
            {
                form.DataGridView8.KeyDown(e);
                e.Handled = true;
            }
        }

        #endregion

    }
}
