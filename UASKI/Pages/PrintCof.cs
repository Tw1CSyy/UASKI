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
    public class PrintCof : BasePagePrint
    {
        public PrintCof(int index, TypePage type) : base(index, type, Ai.Form.DataGridView11) { }

        protected override void Show()
        {
           SelectTextBox(form.textBox36);
            form.dateTimePicker14.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker15.Value = DateTime.Today;

            if (form.textBox36.Text.Length > 0 && form.textBox37.Text.Length > 0)
            {
                Select();
            }

            if (form.DataGridView11.d.Rows.Count > 0)
                form.DataGridView11.d.Focus();
            else
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
            if (form.textBox37.Text.Length != 0)
            {
                var isps = IspModel.GetList();
                var isp = isps.FirstOrDefault(c => c.CodePodr == Convert.ToInt32(form.textBox36.Text));
                
                if (isp != null)
                {
                    var model = new List<DataGridRowModel>();
                    var holy = HolidayModel.GetList();
                    var arhivContext = ArhivModel.GetList();
                    var arhivTasks = new List<ArhivModel>();
                    var tasks = new List<TaskModel>();

                    arhivTasks = arhivContext
                           .Where(c => c.IdIsp == isp.Code)
                           .Where(c => (c.DateClose >= form.dateTimePicker14.Value && c.DateClose <= form.dateTimePicker15.Value) || (c.Date >= form.dateTimePicker14.Value && c.Date <= form.dateTimePicker15.Value && c.GetDaysOpz(holy, form.dateTimePicker14.Value, form.dateTimePicker15.Value) > 0))
                           .ToList();

                    tasks = TaskModel.GetList()
                        .Where(c => c.IdIsp == isp.Code)
                        .Where(c => c.Date < DateTime.Today)
                        .Where(c => c.GetDaysOpz(holy, form.dateTimePicker14.Value, form.dateTimePicker15.Value) > 0)
                        .ToList();

                    foreach (var task in arhivTasks)
                    {
                        var con = isps.FirstOrDefault(c => c.Code == task.IdCon);
                        int opzDays = task.GetDaysOpz(holy, form.dateTimePicker14.Value, form.dateTimePicker15.Value);
                        int opz = 0;

                        if (opzDays > 0)
                            opz = opzDays;

                        var item = new DataGridRowModel(
                            task.Id.ToString(),
                            task.GetCode(),
                            task.Date.ToString("dd.MM.yyyy"),
                            con.InizByCode,
                            task.DateClose.ToString("dd.MM.yyyy"),
                            task.Otm.ToString(),
                            opz.ToString()
                        );

                        model.Add(item);
                    }

                    foreach (var task in tasks)
                    {
                        var con = isps.FirstOrDefault(c => c.Code == task.IdCon);
                        int opz = task.GetDaysOpz(holy, form.dateTimePicker14.Value, form.dateTimePicker15.Value);

                        var item = new DataGridRowModel(
                            task.Id.ToString(),
                            task.GetCode(),
                            task.Date.ToString("dd.MM.yyyy"),
                            con.InizByCode,
                            string.Empty,
                            string.Empty,
                            opz.ToString()
                            );

                        model.Add(item);
                    }

                    var columns = new DataGridColumnModel[]
                    {
                        new DataGridColumnModel("" , false),
                        new DataGridColumnModel("Код"),
                        new DataGridColumnModel("Срок"),
                        new DataGridColumnModel("Контролёр"),
                        new DataGridColumnModel("Дата выполнения"),
                        new DataGridColumnModel("Оценка", typeof(int)),
                        new DataGridColumnModel("Кол-во дней опозданий", typeof(int)),
                    };

                    form.DataGridView11.PullListInDataGridView(model.ToArray(), columns);

                    var arhivTasksMonth = arhivContext
                       .Where(c => c.IdIsp == isp.Code)
                       .Where(c => (c.DateClose < c.Date && c.DateClose.Month == form.dateTimePicker14.Value.Month && c.DateClose.Year == form.dateTimePicker14.Value.Year)
                       || (c.DateClose >= c.Date && c.Date.Month == form.dateTimePicker14.Value.Month && c.Date.Year == form.dateTimePicker14.Value.Year))
                       .ToList();

                    var pretList = PretModel.GetList();
                    var cof1 = isp.GetKofModel(tasks, arhivTasks, holy, pretList, form.dateTimePicker14.Value, form.dateTimePicker15.Value);
                    var cof2 = isp.GetKofModel(tasks, arhivTasksMonth, holy, pretList, form.dateTimePicker14.Value, form.dateTimePicker15.Value);

                    model = new List<DataGridRowModel>();

                    var item1 = new DataGridRowModel(
                         "За период",
                         cof1.Count.ToString(),
                         cof1.CountOpz.ToString(),
                         cof1.CountDay.ToString(),
                         cof1.KofString
                    );

                    model.Add(item1);

                    item1 = new DataGridRowModel(
                        "За месяц",
                        cof2.Count.ToString(),
                        cof2.CountOpz.ToString(),
                        cof2.CountDay.ToString(),
                        cof2.KofString
                    );

                    model.Add(item1);

                    columns = new DataGridColumnModel[]
                    {
                        new DataGridColumnModel("#"),
                        new DataGridColumnModel("Заданий"),
                        new DataGridColumnModel("Кол-во случаев опозданий"),
                        new DataGridColumnModel("Кол-во дней опозданий"),
                        new DataGridColumnModel("Коэффициент")
                    };

                    form.DataGridView13.PullListInDataGridView(model.ToArray(), columns);

                    form.DataGridView13.d.Visible = true;
                    form.label99.Visible = true;
                    form.label100.Visible = true;

                    form.label99.Text = $"Уважаемый товарищ {isp.InizNotCode}";
                }
                else
                {
                    form.DataGridView13.d.Visible = false;
                    form.label99.Visible = false;
                    form.label100.Visible = false;
                }

                form.DataGridView11.d.ClearSelection();
                form.DataGridView13.d.ClearSelection();
            }
            else
            {
                form.DataGridView11.d.DataSource = false;
                form.DataGridView13.d.Visible = false;
                form.label99.Visible = false;
                form.label100.Visible = false;
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
                
                if (GetPrint(printDocument))
                {
                    var printDocument2 = new PrintDocument();
                    printDocument2.PrintPage += new PrintPageEventHandler(PrintPage2);
                    printDocument2.DefaultPageSettings.Landscape = true;
                    printDocument2.Print();
                    FirstPage = true;
                }

                form.DataGridView11.ClearTag();
                form.DataGridView13.ClearTag();
            }
        }

        protected override void PrintPage(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == Convert.ToInt32(form.textBox36.Text));

            string otdelString = "Отдел(цех) " + isp.CodePodr.ToString();
            string nameString = form.textBox37.Text;

            string header1 = $"Справка о составляющих коэффициента качества";
            string header2 = $"C {form.dateTimePicker14.Value.ToString("dd.MM.yyyy")} по {form.dateTimePicker15.Value.ToString("dd.MM.yyyy")}";
            
            var model = new PrintModel(font, e, form.DataGridView11.d, otdelString, header1, nameString, header2);
            SystemHelper.PrintDocument(model, FirstPage);
            FirstPage = false;
        }

        protected void PrintPage2(object sender, PrintPageEventArgs e)
        {
            var font = new Font("Arial", 10);
            var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == Convert.ToInt32(form.textBox36.Text));

            string otdelString = "Отдел(цех) " + isp.CodePodr.ToString();
            string nameString = form.textBox37.Text;

            string header3 = form.label99.Text;
            string header4 = form.label100.Text;

            var model = new PrintModel(font, e, form.DataGridView13.d, otdelString, header3, header4);
            SystemHelper.PrintDocument(model, true);
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
            else if(e.KeyCode == Ai.ActionKey)
            {
                var f = new IspForm(form.textBox37, form.textBox36, new TextBox());
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
            else if(e.KeyCode == Ai.ActionKey)
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
            else if (e.KeyCode == Ai.ActionKey)
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
                SelectButton(form.button42, false);
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
                if (Ai.IsQuery)
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
                SelectDataGridView(form.DataGridView11.d, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter && form.DataGridView11.d.SelectedRows.Count != 0)
            {
                var id = Convert.ToInt32(form.DataGridView11.d.SelectedRows[0].Cells[0].Value);
                var isArhiv = form.DataGridView11.d.SelectedRows[0].Cells[6].Value.ToString().Length > 0;
                Ai.Pages.EditTask.Init(false, false);
                Ai.Pages.EditTask.Show(id, isArhiv);
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
