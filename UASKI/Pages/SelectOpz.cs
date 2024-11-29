using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра опозданий
    /// </summary>
    public class SelectOpz : BasePageSelect
    {
        public SelectOpz(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            FilterClose();
            form.checkBox3.Checked = true;
            form.dateTimePicker7.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker8.Value = DateTime.Today;
            form.panel18.Visible = true;
            Select();
            form.dataGridView1.Focus();
        }

        protected override void Clear()
        {
            form.dataGridView1.DataSource = null;
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

            var listTask = TasksService
                .GetList()
                .Where(c => c.Date < DateTime.Today)
                .OrderByDescending(c => c.Date)
                .ToList();

            var listArhiv = ArhivService.GetList()
                .Where(c => c.DateClose > c.Date)
                .OrderByDescending(c => c.Date)
                .ToList();

            var listUser = IspService.GetList();

            foreach (var item in listTask)
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                if (isDate)
                {
                    if (item.Date.Date < dateFrom.Date || item.Date.Date > dateTo.Date)
                        continue;
                }

                if (!string.IsNullOrEmpty(isp1) && int.TryParse(isp1, out int i))
                {
                    if (isp.Code != Convert.ToInt32(isp1) && con.Code != Convert.ToInt32(isp1))
                        continue;
                }

                if (!string.IsNullOrEmpty(search))
                {
                    if (!item.Code.ToLower().Contains(search.ToLower()))
                        continue;
                }

                var task = new DataGridRowModel(item.Code,
                     IspService.GetIniz(isp),
                     IspService.GetIniz(con),
                     item.Date.ToString("dd.MM.yyyy"), "", "");

                model.Add(task);
            }

            foreach (var item in listArhiv)
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                if (isDate)
                {
                    if (item.DateClose.Date < dateFrom.Date || item.DateClose.Date > dateTo.Date)
                        continue;
                }

                if (!string.IsNullOrEmpty(isp1) && int.TryParse(isp1, out int i))
                {
                    if (isp.Code != Convert.ToInt32(isp1) && con.Code != Convert.ToInt32(isp1))
                        continue;
                }

                if (!string.IsNullOrEmpty(search))
                {
                    if (!item.Code.ToLower().Contains(search.ToLower()))
                        continue;
                }

                var task = new DataGridRowModel(item.Code,
                     $"{isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}",
                     $"{con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}",
                     item.Date.ToString("dd.MM.yyyy"),
                     item.DateClose.ToString("dd.MM.yyyy"),
                     item.Otm.ToString());

                model.Add(task);
            }

            Select(form.dataGridView1,
                model,
                new DataGridRowModel("Код", "Исполнитель", "Котроллер", "Срок", "Дата закрытия", "Оценка"));
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView1, form.panel17, form.textBox19, form.button22);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            form.dataGridView1.ClearSelection();
        }

        #region Клаваши
        public void dataGridView1_KeyDown_1(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == SystemData.ActionKey)
            {
                FilterOpen();
                SystemHelper.SelectTextBox(form.textBox33);
            }
            else if ((e.KeyCode == Keys.Up
                && form.dataGridView1.SelectedRows.Count != 0
                && form.dataGridView1.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter)
            {
                var d = form.dataGridView1;

                if (d.SelectedRows.Count > 0)
                {
                    var code = d.SelectedRows[0].Cells[0].Value.ToString();
                    var IsArhiv = d.SelectedRows[0].Cells[5].Value != null && !string.IsNullOrEmpty(d.SelectedRows[0].Cells[5].Value.ToString());
                    SystemData.Pages.EditTask.Init(false);
                    SystemData.Pages.EditTask.Show(code, IsArhiv , 3);
                }

                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView1, e.KeyCode);
                e.Handled = true;
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox33, e.KeyCode);
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
                SystemHelper.SelectTextBox(form.textBox34);
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
                SystemHelper.SelectCheckBox(form.checkBox3);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox33);
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox() , new TextBox() , form.textBox34);
                f.Show();
                e.Handled = true;
            }
        }

        public void checkBox3_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SystemHelper.SelectCheckBox(form.checkBox3, false);
            }
            else if (e.KeyCode == Keys.Down && form.panel18.Visible)
            {
                form.dateTimePicker7.Focus();
                SystemHelper.SelectCheckBox(form.checkBox3, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox34);
                SystemHelper.SelectCheckBox(form.checkBox3, false);
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
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker8.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectCheckBox(form.checkBox3);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                f.Show();
                e.Handled = true;
            }
        }
        public void dateTimePicker8_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker7.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker7, form.dateTimePicker8);
                f.Show();
                e.Handled = true;
            }
        }
        #endregion
    }
}
