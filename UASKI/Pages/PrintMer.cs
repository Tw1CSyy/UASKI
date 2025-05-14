using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Enums;

namespace UASKI.Pages
{
    public class PrintMer : BasePagePrint
    {
        public PrintMer(int index, TypePage type) : base(index, type, Ai.Form.DataGridView9) { }

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
            var taskList = TaskModel.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var arhivList = ArhivModel.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var result = new List<DataGridRowModel>();
            var holy = HolidayModel.GetList();
            var isps = IspModel.GetList();

            foreach (var item in taskList)
            {
                var date = item.GetDaysOpz(holy);

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.GetCode(),
                    item.GetIsp(isps).InizByCode,
                    item.GetCon(isps).InizByCode,
                    item.Date.ToString("dd.MM.yyyy"), "", "", date.ToString());

                result.Add(model);
            }

            foreach (var item in arhivList)
            {
                var date = item.GetDaysOpz(holy);

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.GetCode(),
                    item.GetIsp(isps).InizByCode,
                    item.GetCon(isps).InizByCode,
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
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Срок"),
                new DataGridColumnModel("Дата закрытия"),
                new DataGridColumnModel("Оценка", typeof(int)),
                new DataGridColumnModel("Дни опоздания", typeof(int))
            };

            form.DataGridView9.PullListInDataGridView(result.ToArray(), columns);
             
            form.DataGridView9.d.ClearSelection();
        }

        protected override void Print()
        {
            if (form.DataGridView9.d.Columns.Count == 0)
            {
                Ai.Error();
            }
            else
            {
                var printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                printDocument.DefaultPageSettings.Landscape = true;
                GetPrint(printDocument);
                form.DataGridView9.ClearTag();
                Ai.Settings.CountPrint++;
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 9);
            string header1 = $"Состояние выполнения мероприятия (приказа)";
            string header2 = $"{form.textBox35.Text}";
            string header3 = $"На {DateTime.Today.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.DataGridView9.d, header1, header2, header3);
            SystemHelper.PrintDocument(model, FirstPage);
            FirstPage = false;
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
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
                Print();
            }

            e.IsInputKey = true;

        }
        #endregion
    }
}
