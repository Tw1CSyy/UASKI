using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;
using System.Drawing;

namespace UASKI.Pages
{
    public class PrintCof : BasePagePrint
    {
        public PrintCof(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            SystemHelper.SelectTextBox(form.textBox36);
            form.dateTimePicker14.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker15.Value = DateTime.Today;
            form.textBox36.Focus();
        }

        protected override void Clear()
        {
            form.textBox36.Clear();
            form.textBox37.Clear();
            SystemHelper.SelectButton(form.button42, false);
            form.dataGridView11.DataSource = null;
            form.dataGridView13.DataSource = null;

            form.dataGridView13.Visible = false;
            form.label99.Visible = false;
            form.label100.Visible = false;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override void Select()
        {
           if(form.textBox37.Text.Length != 0)
            {
                var model = new List<DataGridRowModel>();
                var ispList = IspService.GetList();

                var isp = IspService.GetByCode(Convert.ToInt32(form.textBox36.Text), ispList);
                var tasks = ArhivService.GetList()
                    .Where(c => c.IdIsp == isp.Code)
                    .Where(c => c.DateClose >= form.dateTimePicker14.Value && c.DateClose <= form.dateTimePicker15.Value)
                    .ToList();

                var item = new DataGridRowModel();

                foreach (var task in tasks)
                {
                    var con = IspService.GetByCode(task.IdCon, ispList);
                    int opzDays = (task.DateClose - task.Date).Days;
                    string opz = string.Empty;

                    if (opzDays > 0)
                        opz = opzDays.ToString();

                    item = new DataGridRowModel(
                        task.Code,
                        task.Date.ToString("dd.MM.yyyy"),
                        IspService.GetIniz(con),
                        task.DateClose.ToString("dd.MM.yyyy"),
                        task.Otm.ToString(),
                        opz
                        );

                    model.Add(item);
                }

                SystemHelper.PullListInDataGridView(form.dataGridView11, model,
                    new DataGridRowModel("Код", "Срок", "Котроллер", "Дата выполнения", "Оценка", "Опзд"));

                var cof = SystemHelper.GetKofModel(isp, form.dateTimePicker14.Value, form.dateTimePicker15.Value);

                model = new List<DataGridRowModel>();

                item = new DataGridRowModel(
                    "За период",
                    cof.CountPeriod.ToString(),
                    cof.CountOpzPeriod.ToString(),
                    cof.CountDayPeriod.ToString(),
                    cof.KofPeriod.ToString()
                    );

                model.Add(item);

                item = new DataGridRowModel(
                    "За месяц",
                    cof.CountMonth.ToString(),
                    cof.CountOpzMonth.ToString(),
                    cof.CountDayMonth.ToString(),
                    cof.KofMonth.ToString()
                    );

                model.Add(item);

                SystemHelper.PullListInDataGridView(form.dataGridView13, model,
                    new DataGridRowModel("#", "Заданий", "Случ.опзд", "К-во Дн.опзд", "Качество"));

                form.dataGridView13.Visible = true;
                form.label99.Visible = true;
                form.label100.Visible = true;

                form.label99.Text = $"Уважаемый товарищ {IspService.GetIniz(isp, false)}";
                form.dataGridView11.ClearSelection();
                form.dataGridView13.ClearSelection();
           }
        }

        protected override void Print()
        {
            if (form.dataGridView11.Columns.Count == 0)
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
            string header1 = $"Справка о составляющих коэффициента качества";
            string header2 = $"C {form.dateTimePicker14.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker15.Value.ToString("dd.MM.yyyy")}";
            string header3 = form.label99.Text;
            string header4 = form.label100.Text;

            var model = new PrintModel(font, e, form.dataGridView11, header1, header2);
            var model2 = new PrintModel(font, e, form.dataGridView13, header3, header4);
            SystemHelper.PrintDocument(model , model2);
        }

        public void ClearTime()
        {
            form.textBox37.Clear();
            form.dataGridView11.DataSource = null;
            form.dataGridView13.DataSource = null;

            form.dataGridView13.Visible = false;
            form.label99.Visible = false;
            form.label100.Visible = false;
        }

        #region Клавиши
        public void textBox36_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button42);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker14.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(form.textBox37, new TextBox(), form.textBox36);
                f.Show();
                e.Handled = true;
            }
        }

        public void dateTimePicker14_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                form.dateTimePicker15.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(form.dataGridView11);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                if(e.Control)
                {
                    var f = new DateForm(form.dateTimePicker14, form.dateTimePicker15);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker14);
                    f.Show();
                    e.Handled = true;
                }
            }
        }

        public void dateTimePicker15_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button42);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(form.dataGridView11);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker14, form.dateTimePicker15);
                    f.Show();
                    e.Handled = true;
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker15);
                    f.Show();
                    e.Handled = true;
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker14.Focus();
                e.Handled = true;
            }
        }

        public void button42_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                SystemHelper.SelectButton(form.button42 , false);
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker15.Focus();
                SystemHelper.SelectButton(form.button42, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SystemHelper.SelectDataGridView(form.dataGridView11))
                    SystemHelper.SelectButton(form.button42, false);
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

        public void dataGridView11_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView11.SelectedRows.Count != 0
                && form.dataGridView11.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker14.Focus();
                SystemHelper.SelectDataGridView(form.dataGridView11 , false);
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.Down
                && form.dataGridView11.SelectedRows.Count != 0
                && form.dataGridView11.SelectedRows[0].Index == form.dataGridView11.Rows.Count - 1))
            {
                SystemHelper.SelectDataGridView(form.dataGridView13);
                SystemHelper.SelectDataGridView(form.dataGridView11, false);
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView11, e.KeyCode);
                e.Handled = true;
            }
        }

        public void dataGridView13_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
               && form.dataGridView13.SelectedRows.Count != 0
               && form.dataGridView13.SelectedRows[0].Index == 0)
               || e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectDataGridView(form.dataGridView11);
                SystemHelper.SelectDataGridView(form.dataGridView13, false);
            }
        }
        #endregion
    }
}
