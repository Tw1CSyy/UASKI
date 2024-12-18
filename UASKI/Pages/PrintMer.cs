using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintMer : BasePagePrint
    {
        public PrintMer(int index) : base(index) { }

        protected override void Show()
        {
            form.label79.Text = "На " + DateTime.Today.ToString("dd.MM.yyyy");
            form.textBox35.Focus();
        }

        protected override void Clear()
        {
            form.textBox35.Clear();
            SelectButton(form.button38, false);
            form.DataGridView9.d.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SelectDataGridView(form.DataGridView9.d, false);
            SelectButton(form.button38, false);
        }

        public override void Select()
        {
            var taskList = TasksService.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var arhivList = ArhivService.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var ispList = IspService.GetList();
            var result = new List<DataGridRowModel>();

            foreach (var item in taskList)
            {
                var isp = IspService.GetByCode(item.IdIsp, ispList);
                var con = IspService.GetByCode(item.IdCon, ispList);
                var date = (DateTime.Today - item.Date).Days;

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.Code,
                    IspService.GetIniz(isp),
                    IspService.GetIniz(con),
                    item.Date.ToString("dd.MM.yyyy"), "", "", date.ToString());

                result.Add(model);
            }

            foreach (var item in arhivList)
            {
                var isp = IspService.GetByCode(item.IdIsp, ispList);
                var con = IspService.GetByCode(item.IdCon, ispList);
                var date = (item.DateClose - item.Date).Days;

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.Code,
                    $"{isp.Code} {isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.",
                    $"{con.Code} {con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}.",
                    item.Date.ToString("dd.MM.yyyy"),
                    item.DateClose.ToString("dd.MM.yyyy"),
                    item.Otm.ToString(),
                    date.ToString());

                result.Add(model);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контролер"),
                new DataGridColumnModel("Срок"),
                new DataGridColumnModel("Дата закрытия"),
                new DataGridColumnModel("Оценка"),
                new DataGridColumnModel("Дни опоздания")
            };

            SystemHelper.PullListInDataGridView(form.DataGridView9.d, result.ToArray(), columns);
             
            form.DataGridView9.d.ClearSelection();
        }

        protected override void Print()
        {
            if (form.DataGridView9.d.Columns.Count == 0)
            {
                ErrorHelper.StatusError();
            }
            else
            {
                var printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                printDocument.DefaultPageSettings.Landscape = true;
                GetPrint(printDocument);
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 9);
            string header1 = $"Состояние выполнения мероприятия (приказа)";
            string header2 = $"{form.textBox35.Text}";
            string header3 = $"На {DateTime.Today.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.DataGridView9.d, header1, header2 , header3);
            SystemHelper.PrintDocument(model);
        }

        #region Клавиши

        public void textBox35_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectDataGridView(form.DataGridView9.d);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button38);
                e.Handled = true;
            }
        }

        public void dataGridView9_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Exit();
                SelectDataGridView(form.DataGridView9.d, false);
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.Up
                && form.DataGridView9.d.SelectedRows.Count != 0
                && form.DataGridView9.d.SelectedRows[0].Index == 0))
            {
                SelectTextBox(form.textBox35);
                e.Handled = true;
                SelectDataGridView(form.DataGridView9.d, false);
            }
            else
            {
                form.DataGridView9.KeyDown(e);
                e.Handled = true;
            }

        }

        public void button38_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox35);
                SelectButton(form.button38, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SelectDataGridView(form.DataGridView9.d))
                    SelectButton(form.button38, false);
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
        #endregion
    }
}
