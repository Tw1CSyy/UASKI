using System;
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
    public class PrintPlan : BasePagePrint
    {
        public PrintPlan(int index, TypePage type) : base(index, type, Ai.Form.DataGridView14) { }

        protected override void Show()
        {
            if(IsCleared)
            {
                form.dateTimePicker20.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker21.Value = DateTime.Today;
            }

            if (form.textBox42.Text.Length > 0 && form.textBox43.Text.Length > 0)
            {
                Select();
            }

            if (form.DataGridView14.d.Rows.Count > 0)
                form.DataGridView14.d.Focus();
            else
                form.dateTimePicker20.Focus();
        }

        protected override void Clear()
        {
            form.DataGridView14.d.DataSource = null;
            form.textBox42.Clear();
            form.textBox43.Clear();
            SelectButton(form.button56, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SelectButton(form.button56, false);
        }

        public override void Select()
        {
            if(form.textBox42.Text.Length > 0)
            {
                var codePodr = Convert.ToInt32(form.textBox42.Text);
                var dateFrom = form.dateTimePicker20.Value;
                var dateTo = form.dateTimePicker21.Value;
                var isps = IspModel.GetList();
                var isp = isps.FirstOrDefault(c => c.CodePodr == codePodr);
                var holy = HolidayModel.GetList();

                var tasks = TaskModel.GetList()
                    .Where(c => c.IdIsp == isp.Code && ((c.Date >= dateFrom && c.Date <= dateTo) || c.Date < dateFrom))
                    .OrderBy(c => c.Date);

                var model = tasks.Select(c => new DataGridRowModel(
                    c.Id.ToString(), c.GetCode(), c.Date.ToString("dd.MM.yyyy"), c.GetCon(isps).InizByCode, c.GetDaysOpz(holy).ToString()));

                var columns = new DataGridColumnModel[]
                {
                new DataGridColumnModel("" , false),
                new DataGridColumnModel("Код задания"),
                new DataGridColumnModel("Срок исполнения"),
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Дней опозданий", typeof(int))
                };

                form.DataGridView14.PullListInDataGridView(model.ToArray(), columns);
            }
        }

        protected override void Print()
        {
            if (form.DataGridView14.d.Columns.Count == 0)
            {
                Ai.Error();
            }
            else
            {
                var printDocument = new PrintDocument();
                printDocument.DefaultPageSettings.Landscape = true;
                printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                GetPrint(printDocument);
                form.DataGridView14.ClearTag();
                Ai.Settings.CountPrint++;
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            string header1 = $"Планы с {form.dateTimePicker20.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker21.Value.ToString("dd.MM.yyyy")}";
            string header2 = $"Исполнитель {IspModel.GetList().First(c => c.CodePodr == Convert.ToInt32(form.textBox42.Text)).InizByCode}";

            var model = new PrintModel(font, e, form.DataGridView14.d, header1, header2);
            SystemHelper.PrintDocument(model, FirstPage);
            FirstPage = false;
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }

        #region Клавиши
        public void dateTimePicker20_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectTextBox(form.textBox42);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                form.dateTimePicker21.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker20, form.dateTimePicker21);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker20);
                    f.Show();
                    e.Handled = true;
                }
            }
        }

        public void dateTimePicker21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectTextBox(form.textBox42);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button56);
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker20, form.dateTimePicker21);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker21);
                    f.Show();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                form.dateTimePicker20.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox42_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker20.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button56);
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                var f = new IspForm(form.textBox43, form.textBox42, new TextBox());
                f.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectDataGridView(form.DataGridView14.d);
                e.Handled = true;
            }
        }

        public void button56_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox42);
                SelectButton(form.button56, false);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker20.Focus();
                SelectButton(form.button56, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (SelectDataGridView(form.DataGridView14.d))
                    SelectButton(form.button56, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Print();
            }

            e.IsInputKey = true;
        }

        public void dataGridView14_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SelectTextBox(form.textBox42);
                SelectDataGridView(form.DataGridView14.d, false);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button56);
                SelectDataGridView(form.DataGridView14.d, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter && form.DataGridView14.d.SelectedRows.Count != 0)
            {
                var id = Convert.ToInt32(form.DataGridView14.d.SelectedRows[0].Cells[0].Value);
                Ai.Pages.EditTask.Init(false, false);
                Ai.Pages.EditTask.Show(id, false);
                e.Handled = true;
            }    
            else
            {
                form.DataGridView14.KeyDown(e);
                e.Handled = true;
            }
        }
        #endregion
    }
}
