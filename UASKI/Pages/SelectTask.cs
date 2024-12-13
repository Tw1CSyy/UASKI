using System.Windows.Forms;
using UASKI.Forms;
using System;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using System.Collections.Generic;
using System.Linq;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра задач
    /// </summary>
    public class SelectTask : BasePageSelect
    {
        public SelectTask(int index) : base(index) { }
        public override DataGridView DataGridView { get => form.dataGridView3; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            if(this.IsCleared)
            {
                form.panel16.Visible = form.checkBox2.Checked = false;
                form.dateTimePicker5.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker6.Value = DateTime.Today;
                FilterClose();
            }

            Select();
            form.dataGridView3.Focus();
        }

        protected override void Clear()
        {
            form.textBox19.Clear();
            form.textBox29.Clear();
            form.dataGridView3.DataSource = null;
        }

        public override void Select()
        {
            var list = TasksService.GetList(form.textBox19.Text, form.textBox29.Text, form.checkBox2.Checked, form.dateTimePicker5.Value, form.dateTimePicker6.Value);

            var model = new List<DataGridRowModel>();
            var listUser = IspService.GetList();

            foreach (var item in list.OrderByDescending(c => c.Date))
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                var st = new DataGridRowModel(item.Id.ToString(), item.Code, IspService.GetIniz(isp), IspService.GetIniz(con), item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Id" , false),
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контроллер"),
                new DataGridColumnModel("Срок"),
            };

            SystemHelper.PullListInDataGridView(form.dataGridView3, model.ToArray(), columns);
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView3, form.panel13, form.textBox19, form.button20);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        #region Клавиши
        public void dataGridView3_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
                form.dataGridView3.ClearSelection();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView3.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(form.dataGridView3.SelectedRows[0].Cells[0].Value);

                SystemData.Pages.EditTask.Init(false , false);
                SystemData.Pages.EditTask.Show(id, false , this);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.DataGridDownSelect(form.dataGridView3);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.DataGridUpSelect(form.dataGridView3);
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView3, e.KeyCode);
                e.Handled = true;
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox19, e.KeyCode);
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
                SystemHelper.SelectTextBox(form.textBox29);
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
                SystemHelper.SelectCheckBox(form.checkBox2);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox19);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox(), new TextBox(), form.textBox29);
                f.Show();
                e.Handled = true;
            }
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
                SystemHelper.SelectCheckBox(form.checkBox2, false);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox29);
                SystemHelper.SelectCheckBox(form.checkBox2, false);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SystemHelper.SelectCheckBox(form.checkBox2, false);
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
                SystemHelper.SelectCheckBox(form.checkBox2);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if(e.KeyCode == SystemData.ActionKey)
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
            else if (e.KeyCode == SystemData.ActionKey)
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
