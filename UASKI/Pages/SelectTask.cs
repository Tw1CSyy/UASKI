using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using UASKI.Core.Models;
using UASKI.Enums;
using UASKI.Forms;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра задач
    /// </summary>
    public class SelectTask : BasePageSelect
    {
        public SelectTask(int index, TypePage type) : base(index, type, Ai.Form.DataGridView3) { }
       
        protected override void Show()
        {
            if (this.IsCleared)
            {
                form.panel16.Visible = form.checkBox2.Checked = form.checkBox10.Checked = false;
                form.dateTimePicker5.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker6.Value = DateTime.Today;
                FilterClose();
            }

            Select();
            form.DataGridView3.d.Focus();
        }

        protected override void Clear()
        {
            form.panel16.Visible = form.checkBox2.Checked = form.checkBox10.Checked = false;
            form.textBox19.Clear();
            form.textBox29.Clear();
            form.DataGridView3.d.DataSource = null;
        }

        public override void Select()
        {
            var list = TaskModel.GetList();
            var isps = IspModel.GetList();

            if(form.textBox19.Text.Length > 0)
            {
                list = list.Where(c => c.Code.ToLower().Contains(form.textBox19.Text.ToLower())).ToList();
            }

            if(form.textBox29.Text.Length > 0 && int.TryParse(form.textBox29.Text, out int j))
            {
                list = list.Where(c => c.GetIsp(isps).CodePodr == Convert.ToInt32(form.textBox29.Text)).ToList();
            }

            if(form.checkBox10.Checked)
            {
                list = list.Where(c => c.IsDouble == true).ToList();
            }

            if(form.checkBox2.Checked)
            {
                list = list.Where(c => c.Date >= form.dateTimePicker5.Value && c.Date <= form.dateTimePicker6.Value).ToList();
            }

            var model = new List<DataGridRowModel>();
            var holy = HolidayModel.GetList();

            foreach (var item in list
                .OrderBy(c => c.Date)
                .ThenBy(c => c.GetIsp(isps).CodePodr)
                .ThenBy(c => c.GetCon(isps).CodePodr)
                .ThenBy(c => c.Id))
            {
                int days = item.GetDaysOpz(holy);
                string daysOpz = string.Empty;

                if (days != 0)
                    daysOpz = days.ToString();

                var st = new DataGridRowModel(item.Id.ToString(), item.GetCode(), item.GetIsp(isps).InizByCode, item.GetCon(isps).InizByCode, item.Date.ToString("dd.MM.yyyy") , daysOpz);
                model.Add(st);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Id", false),
                new DataGridColumnModel("Код" , 13),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Срок", typeof(DateTime)),
                new DataGridColumnModel("ДО", true, DataGridViewAutoSizeColumnMode.ColumnHeader)
            };

            form.DataGridView3.PullListInDataGridView(model.ToArray(), columns);
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.DataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void FilterClose()
        {
            FilterClose(form.DataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            if(key.Control)
            {
                if(key.KeyCode == Keys.C && DataGridView.d.SelectedRows.Count != 0)
                {
                    var id = Convert.ToInt32(DataGridView.d.SelectedRows[0].Cells[0].Value);
                    var code = DataGridView.d.SelectedRows[0].Cells[1].Value.ToString();

                    if (Ai.TypeBuffer == Enums.TypeBuffer.AddTask)
                    {
                        Ai.GetBuffer().Clear();
                        Ai.AddMessage(Enums.TypeNotice.Default, "Буфер отчищен");
                    }

                    Ai.AddBuffer(id, $"Задача с кодом {code} добавлена в Буфер");
                    Ai.TypeBuffer = Enums.TypeBuffer.Task;
                    return true;
                }
                else if(key.KeyCode == Keys.X && DataGridView.d.SelectedRows.Count != 0)
                {
                    var id = Convert.ToInt32(DataGridView.d.SelectedRows[0].Cells[0].Value);
                    var code = DataGridView.d.SelectedRows[0].Cells[1].Value.ToString();
                    Ai.DeleteBuffer(id, $"Задача с кодом {code} удаленна из Буфера");
                    return true;
                }
            }

            return false;
        }

        #region Клавиши
        public void dataGridView3_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
                form.DataGridView3.d.ClearSelection();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && form.DataGridView3.d.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(form.DataGridView3.d.SelectedRows[0].Cells[0].Value);

                Ai.Pages.EditTask.Init(false, false);
                Ai.Pages.EditTask.Show(id, false);
                e.Handled = true;
            }
            else if(e.KeyCode == Ai.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
            else
            {
                form.DataGridView3.KeyDown(e, form.textBox19);
            }
        }

        public void textBox19_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectTextBox(form.textBox29);
                e.Handled = true;
            }
        }

        public void textBox29_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox19);
                e.Handled = true;
            }
            else if(e.KeyCode == Ai.ActionKey)
            {
                var f = new IspForm(new TextBox(), form.textBox29, new TextBox());
                f.Show();
                e.Handled = true;
            }
        }

        public void checkBox10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox10.Checked = !form.checkBox10.Checked;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox2);
                SelectCheckBox(form.checkBox10, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox29);
                SelectCheckBox(form.checkBox10, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox10, false);
            }

            e.IsInputKey = true;
        }

        public void checkBox2_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                form.checkBox2.Checked = !form.checkBox2.Checked;
            }
            else if(e.KeyCode == Keys.Down && form.panel16.Visible)
            {
                form.dateTimePicker5.Focus();
                SelectCheckBox(form.checkBox2, false);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox10);
                SelectCheckBox(form.checkBox2, false);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox2, false);
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker5_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker6.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox2);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if(e.KeyCode == Ai.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker5);
                    f.Show();
                }
            }

            e.Handled = true;
        }

        public void dateTimePicker6_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Right|| e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker5, form.dateTimePicker6);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker6);
                    f.Show();
                }
            }

            e.Handled = true;
        }

        #endregion

    }
}
