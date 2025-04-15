using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Models;
using UASKI.Models.Components;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра опозданий
    /// </summary>
    public class SelectOpz : BasePageSelect
    {
        public SelectOpz(int index) : base(index) { }
        public override DataGridViewComponent DataGridView { get => form.DataGridView1; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            if(this.IsCleared)
            {
                FilterClose();
                form.checkBox3.Checked = true;
                form.dateTimePicker7.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker8.Value = DateTime.Today;
                form.panel18.Visible = true;
                form.checkBox12.Checked = false;
            }

            Select();
            form.DataGridView1.d.Focus();
        }

        protected override void Clear()
        {
            form.DataGridView1.d.DataSource = null;
            form.textBox33.Clear();
            form.textBox34.Clear();
        }

        public override void Select()
        {
            var search = form.textBox33.Text;
            var isp1 = form.textBox34.Text;
            bool isDate = form.checkBox3.Checked;
            var dateFrom = form.dateTimePicker7.Value;
            var dateTo = form.dateTimePicker8.Value;

            var model = new List<DataGridRowModel>();
            var holy = HolidayModel.GetList();
            var isps = IspModel.GetList();

            var listTask = TaskModel
                .GetList()
                .Where(c => c.GetDaysOpz(holy) != 0);

            var listArhiv = ArhivModel.GetList()
                .Where(c => c.GetDaysOpz(holy) != 0);

            if(!string.IsNullOrEmpty(search))
            {
                listTask = listTask.Where(c => c.Code.ToLower().Contains(search.ToLower()));
                listArhiv = listArhiv.Where(c => c.Code.ToLower().Contains(search.ToLower()));
            }

            if(!string.IsNullOrEmpty(isp1) && int.TryParse(isp1 , out int id))
            {
                listTask = listTask.Where(c => c.GetIsp(isps).CodePodr == id);
                listArhiv = listArhiv.Where(c => c.GetIsp(isps).CodePodr == id);
            }

            if(form.checkBox12.Checked)
            {
                listTask = listTask.Where(c => c.IsDouble == true).ToList();
                listArhiv = listArhiv.Where(c => c.IsDouble == true).ToList();
            }

            if(isDate)
            {
                listTask = listTask.Where(c => c.Date >= dateFrom && c.Date <= dateTo);
                listArhiv = listArhiv.Where(c => c.Date >= dateFrom && c.Date <= dateTo);
            }

            var taskModel = listTask
                .OrderBy(c => c.Date)
                .ThenBy(c => c.Id)
                .Select(c => new DataGridRowModel(c.Id.ToString() , c.GetCode(), c.GetIsp(isps).InizByCode, c.GetCon(isps).InizByCode, c.Date.ToString("dd.MM.yyyy"), "", "", c.GetDaysOpz(holy).ToString()))
                .ToList();

            var arhivModel = listArhiv
                .OrderBy(c => c.Date)
                .ThenBy(c => c.Id)
                .Select(c => new DataGridRowModel(c.Id.ToString() , c.GetCode(), c.GetIsp(isps).InizByCode, c.GetCon(isps).InizByCode, c.Date.ToString("dd.MM.yyyy"), c.DateClose.ToString("dd.MM.yyyy") , c.Otm.ToString(), c.GetDaysOpz(holy).ToString()))
                .ToList();

            model.AddRange(taskModel);
            model.AddRange(arhivModel);

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Id" , false),
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Котролёр"),
                new DataGridColumnModel("Срок" , typeof(DateTime)),
                new DataGridColumnModel("Дата закрытия" , typeof(DateTime)),
                new DataGridColumnModel("Оценка" , typeof(int)),
                new DataGridColumnModel("Дней опозданий" , typeof(int))
            };

            form.DataGridView1.PullListInDataGridView(model.ToArray(), columns);
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.DataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void FilterClose()
        {
            FilterClose(form.DataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            form.DataGridView1.d.ClearSelection();
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            if (key.Control)
            {
                if (key.KeyCode == Keys.C && DataGridView.d.SelectedRows.Count != 0)
                {
                    var id = Convert.ToInt32(DataGridView.d.SelectedRows[0].Cells[0].Value);
                    var code = DataGridView.d.SelectedRows[0].Cells[1].Value.ToString();

                    if (Ai.TypeBuffer == Enums.TypeBuffer.AddTask)
                    {
                        Ai.GetBuffer().Clear();
                        Ai.AddMessage(Enums.TypeNotice.Default, "Буффер отчищен");
                    }

                    Ai.AddBuffer(id, $"Задача с кодом {code} добавлена в буффер");
                    Ai.TypeBuffer = Enums.TypeBuffer.Task;
                    return true;
                }
                else if (key.KeyCode == Keys.X && DataGridView.d.SelectedRows.Count != 0)
                {
                    var id = Convert.ToInt32(DataGridView.d.SelectedRows[0].Cells[0].Value);
                    var code = DataGridView.d.SelectedRows[0].Cells[1].Value.ToString();
                    Ai.DeleteBuffer(id, $"Задача с кодом {code} удаленна из буффера");
                    return true;
                }
            }

            return false;
        }
        #region Клаваши
        public void dataGridView1_KeyDown_1(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                FilterOpen();
                SelectTextBox(form.textBox33);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter)
            {
                var d = form.DataGridView1.d;

                if (d.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(d.SelectedRows[0].Cells[0].Value);

                    var IsArhiv = d.SelectedRows[0].Cells[5].Value != null && !string.IsNullOrEmpty(d.SelectedRows[0].Cells[5].Value.ToString());
                    SystemData.Pages.EditTask.Init(false , false);
                    SystemData.Pages.EditTask.Show(id, IsArhiv , this);
                }

                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
            else
            {
                form.DataGridView1.KeyDown(e, form.textBox33);
            }

        }
        public void textBox33_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox34);
                e.Handled = true;
            }
            
        }
        public void textBox34_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectCheckBox(form.checkBox12);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox33);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox(), form.textBox34, new TextBox());
                f.Show();
                e.Handled = true;
            }
        }
        public void checkBox12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox12, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox3);
                SelectCheckBox(form.checkBox12, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox34);
                SelectCheckBox(form.checkBox12, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                form.checkBox12.Checked = !form.checkBox12.Checked;
            }

            e.IsInputKey = true;
        }
        public void checkBox3_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox3, false);
            }
            else if (e.KeyCode == Keys.Down && form.panel18.Visible)
            {
                form.dateTimePicker7.Focus();
                SelectCheckBox(form.checkBox3, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox12);
                SelectCheckBox(form.checkBox3, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                form.checkBox3.Checked = !form.checkBox3.Checked;
            }

            e.IsInputKey = true;
        }
        public void dateTimePicker7_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker8.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox3);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker7);
                    f.Show();
                }
            }

            e.Handled = true;
        }
        public void dateTimePicker8_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker7.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker8);
                    f.Show();
                }
            }

            e.Handled = true;
        }
        #endregion
    }
}
