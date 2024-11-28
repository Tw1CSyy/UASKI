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
        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            Select();
            SystemHelper.SelectButton(true, form.button37);
            SystemHelper.SelectDataGridView(false, form.dataGridView8);
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
            SystemHelper.SelectDataGridView(false, form.dataGridView8);
            SystemHelper.SelectButton(false, form.button37);
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

            SystemHelper.PullListInDataGridView(form.dataGridView8,
                result,
                new DataGridRowModel("Исполнитель", "Код задания", "Код котролера", "Срок исполнения", "Дней опозданий"));
            form.dataGridView8.ClearSelection();
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
                if (SystemHelper.SelectDataGridView(true, form.dataGridView8))
                    SystemHelper.SelectButton(false, form.button37);
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
                SystemHelper.SelectButton(true, form.button37);
                SystemHelper.SelectDataGridView(false, form.dataGridView8);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
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
