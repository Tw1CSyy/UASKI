using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.StaticModels;
using System.Drawing;
using UASKI.Core.Models;

namespace UASKI.Pages
{
    public class PrintCof : BasePagePrint
    {
        public PrintCof(int index) : base(index) { }

        protected override void Show()
        {
           SelectTextBox(form.textBox36);
            form.dateTimePicker14.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker15.Value = DateTime.Today;
            form.textBox36.Focus();
        }

        protected override void Clear()
        {
            form.textBox36.Clear();
            form.textBox37.Clear();
            SelectButton(form.button42, false);
            form.DataGridView11.d.DataSource = null;
            form.DataGridView13.d.DataSource = null;

            form.DataGridView13.d.Visible = false;
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
                var isps = IspModel.GetList();
                var holy = HolidayModel.GetList();
                var isp = isps.FirstOrDefault(c => c.Code == Convert.ToInt32(form.textBox36.Text));

                var tasks = ArhivModel.GetList()
                    .Where(c => c.IdIsp == isp.Code)
                    .Where(c => c.DateClose >= form.dateTimePicker14.Value && c.DateClose <= form.dateTimePicker15.Value)
                    .ToList();

                var item = new DataGridRowModel();

                foreach (var task in tasks)
                {
                    var con = isps.FirstOrDefault(c => c.Code == task.IdCon);
                    int opzDays = task.GetDaysOpz(holy);
                    int opz = 0;

                    if (opzDays > 0)
                        opz = opzDays;

                    item = new DataGridRowModel(
                        task.Code,
                        task.Date.ToString("dd.MM.yyyy"),
                        con.InizByCode,
                        task.DateClose.ToString("dd.MM.yyyy"),
                        task.Otm.ToString(),
                        opz.ToString()
                        );

                    model.Add(item);
                }

                var columns = new DataGridColumnModel[]
                {
                    new DataGridColumnModel("Код"),
                    new DataGridColumnModel("Срок"),
                    new DataGridColumnModel("Котроллер"),
                    new DataGridColumnModel("Дата выполнения"),
                    new DataGridColumnModel("Оценка" , typeof(int)),
                    new DataGridColumnModel("Опзд" , typeof(int)),
                };

                form.DataGridView11.PullListInDataGridView(model.ToArray(), columns);
                var contextTasks = TaskModel.GetList().Where(c => c.IdIsp == isp.Code).ToList();
                var contextArhiv = ArhivModel.GetList().Where(c => c.IdIsp == isp.Code).ToList();
                
                var cof = SystemHelper.GetKofModel(form.dateTimePicker14.Value, form.dateTimePicker15.Value , isp , contextTasks , contextArhiv , holy);

                model = new List<DataGridRowModel>();

                item = new DataGridRowModel(
                    "За период",
                    cof.CountPeriod.ToString(),
                    cof.CountOpzPeriod.ToString(),
                    cof.CountDayPeriod.ToString(),
                    cof.KofPeriodString
                    );

                model.Add(item);

                item = new DataGridRowModel(
                    "За месяц",
                    cof.CountMonth.ToString(),
                    cof.CountOpzMonth.ToString(),
                    cof.CountDayMonth.ToString(),
                    cof.KofMonthString
                    );

                model.Add(item);

                columns = new DataGridColumnModel[]
                {
                    new DataGridColumnModel("#"),
                    new DataGridColumnModel("Заданий"),
                    new DataGridColumnModel("Случ.опзд"),
                    new DataGridColumnModel("К-во Дн.опзд"),
                    new DataGridColumnModel("Качество")
                };

                form.DataGridView13.PullListInDataGridView(model.ToArray(), columns);

                form.DataGridView13.d.Visible = true;
                form.label99.Visible = true;
                form.label100.Visible = true;

                form.label99.Text = $"Уважаемый товарищ {isp.InizNotCode}";
                form.DataGridView11.d.ClearSelection();
                form.DataGridView13.d.ClearSelection();
           }
        }

        protected override void Print()
        {
            if (form.DataGridView11.d.Columns.Count == 0)
            {
                Ai.Error();
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
            var font = new Font("Arial", 10);
            string header1 = $"Справка о составляющих коэффициента качества";
            string header2 = $"C {form.dateTimePicker14.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker15.Value.ToString("dd.MM.yyyy")}";
            string header3 = form.label99.Text;
            string header4 = form.label100.Text;

            var model = new PrintModel(font, e, form.DataGridView11.d, header1, header2);
            var model2 = new PrintModel(font, e, form.DataGridView13.d, header3, header4);
            var y = SystemHelper.PrintDocument(model);
            SystemHelper.PrintDocument(model2 , y);
        }

        public void ClearTime()
        {
            form.textBox37.Clear();
            form.DataGridView11.d.DataSource = null;
            form.DataGridView13.d.DataSource = null;

            form.DataGridView13.d.Visible = false;
            form.label99.Visible = false;
            form.label100.Visible = false;
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }
        #region Клавиши
        public void textBox36_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                SelectButton(form.button42);
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
               SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectDataGridView(form.DataGridView11.d);
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
                SelectButton(form.button42);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
               SelectTextBox(form.textBox36);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectDataGridView(form.DataGridView11.d);
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
                SelectButton(form.button42 , false);
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker15.Focus();
                SelectButton(form.button42, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SelectDataGridView(form.DataGridView11.d))
                    SelectButton(form.button42, false);
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

        public void dataGridView11_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.DataGridView11.d.SelectedRows.Count != 0
                && form.DataGridView11.d.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.dateTimePicker14.Focus();
                SelectDataGridView(form.DataGridView11.d , false);
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.Down
                && form.DataGridView11.d.SelectedRows.Count != 0
                && form.DataGridView11.d.SelectedRows[0].Index == form.DataGridView11.d.Rows.Count - 1))
            {
                SelectDataGridView(form.DataGridView13.d);
                e.Handled = true;
            }
            else
            {
                form.DataGridView11.KeyDown(e);
                e.Handled = true;
            }
        }

        public void dataGridView13_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
               && form.DataGridView13.d.SelectedRows.Count != 0
               && form.DataGridView13.d.SelectedRows[0].Index == 0)
               || e.KeyCode == Keys.Escape)
            {
                SelectDataGridView(form.DataGridView11.d);
                SelectDataGridView(form.DataGridView13.d, false);
            }
            else
            {
                form.DataGridView13.KeyDown(e);
                e.Handled = true;
            }
        }
        #endregion
    }
}
