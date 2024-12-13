using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintOpz : BasePagePrint
    {
        public PrintOpz(int index) : base(index) { }
       
        protected override void Show()
        {
            Select();
            SystemHelper.SelectButton(form.button37);
            SystemHelper.SelectDataGridView(form.dataGridView8, false);
            form.label77.Text = "На " + DateTime.Today.ToString("dd.MM.yyyy");
        }

        protected override void Clear()
        {
            form.dataGridView8.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SystemHelper.SelectDataGridView(form.dataGridView8, false);
            SystemHelper.SelectButton(form.button37, false);
        }

        public override void Select()
        {
            var list = TasksService.GetList().Where(c => c.Date < DateTime.Today).ToList();
            var ispList = IspService.GetList();
            var result = new List<DataGridRowModel>();

            foreach (var item in list)
            {
                var isp = IspService.GetByCode(item.IdIsp, ispList);
                var con = IspService.GetByCode(item.IdCon, ispList);
                var day = (DateTime.Today - item.Date).Days;

                var model = new DataGridRowModel($"{isp.Code} {isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}",
                    item.Code,
                    $"{con.Code} {con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}",
                    item.Date.ToString("dd.MM.yyyy"),
                    day.ToString());

                result.Add(model);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Код котролера"),
                new DataGridColumnModel("Срок исполнения"),
                new DataGridColumnModel("Дней опозданий")
            };

            SystemHelper.PullListInDataGridView(form.dataGridView8, result.ToArray(), columns);
        }

        protected override void Print()
        {
            if (form.dataGridView8.Columns.Count == 0)
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
            string header1 = $"Не выполнили задание в срок";
            string header2 = $"На {DateTime.Today.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.dataGridView8, header1, header2);
            SystemHelper.PrintDocument(model);
        }

        #region Клавиши

        public void button37_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                if (SystemHelper.SelectDataGridView(form.dataGridView8))
                    SystemHelper.SelectButton(form.button37, false);
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
                    ErrorHelper.StatusQuery();

            }

            e.IsInputKey = true;
        }

        public void dataGridView8_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView8.SelectedRows.Count != 0
                && form.dataGridView8.SelectedRows[0].Index == 0))
            {
                SystemHelper.SelectButton(form.button37);
                SystemHelper.SelectDataGridView(form.dataGridView8, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.DataGridDownSelect(form.dataGridView8);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.DataGridUpSelect(form.dataGridView8);
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView8, e.KeyCode);
                e.Handled = true;
            }
        }

        #endregion

    }
}
