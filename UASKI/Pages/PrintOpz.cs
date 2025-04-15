using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Enums;

namespace UASKI.Pages
{
    public class PrintOpz : BasePagePrint
    {
        public PrintOpz(int index, TypePage type) : base(index, type, Ai.Form.DataGridView8) { }
       
        protected override void Show()
        {
            if (form.DataGridView8.d.Rows.Count > 0)
                form.DataGridView8.d.Focus();
            else
                form.dateTimePicker19.Focus();

            Select();
            SelectButton(form.button37, false);
            SelectDataGridView(form.DataGridView8.d, false);
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
            var holy = HolidayModel.GetList();
            var list = TaskModel.GetList().Where(c => c.GetDaysOpz(holy, DateTime.MinValue, form.dateTimePicker19.Value.Date) != 0).ToList();
            var result = new List<DataGridRowModel>();
            var isps = IspModel.GetList();

            foreach (var item in list)
            {
                var day = item.GetDaysOpz(holy, DateTime.MinValue, form.dateTimePicker19.Value.Date);

                var model = new DataGridRowModel(
                    item.Id.ToString(),
                    item.GetIsp(isps).InizByCode,
                    item.GetCode(),
                    item.GetCon(isps).InizByCode,
                    item.Date.ToString("dd.MM.yyyy"),
                    day.ToString());

                result.Add(model);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("" , false),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Срок исполнения"),
                new DataGridColumnModel("Дней опозданий", typeof(int))
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
                form.DataGridView8.ClearTag();
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            string header1 = $"Не выполнили задание в срок";
            string header2 = $"На {form.dateTimePicker19.Value.ToString("dd.MM.yyyy")}";

            var model = new PrintModel(font, e, form.DataGridView8.d, header1, header2);
            SystemHelper.PrintDocument(model, FirstPage);
            FirstPage = false;
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }
        #region Клавиши

        public void dateTimePicker19_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Ai.ActionKey)
            {
                var form1 = new DateForm(form.dateTimePicker19);
                form1.Show();
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.DataGridView8.d.Focus();
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton(form.button37);
            }
        }
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
                if (Ai.IsQuery)
                {
                    Print();
                }
                else
                    Ai.Query();

            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker19.Focus();
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
            else if(e.KeyCode == Keys.Enter && form.DataGridView8.d.SelectedRows.Count != 0)
            {
                var id = Convert.ToInt32(form.DataGridView8.d.SelectedRows[0].Cells[0].Value);
                Ai.Pages.EditTask.Init(false, false);
                Ai.Pages.EditTask.Show(id, false);
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
