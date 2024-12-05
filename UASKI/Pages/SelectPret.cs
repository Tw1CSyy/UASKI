using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class SelectPret : BasePageSelect
    {
        public SelectPret(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.checkBox4.Checked = false;
            form.panel22.Visible = false;
            FilterClose();
            form.dateTimePicker17.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker18.Value = DateTime.Today;
            form.checkBox5.Checked = form.checkBox6.Checked = true;
            Select();
            form.dataGridView12.Focus();
        }

        protected override void Clear()
        {
            form.textBox40.Clear();
            form.textBox41.Clear();
            form.dataGridView12.DataSource = null;
            SystemHelper.SelectCheckBox(form.checkBox5, false);
        }

        public override void Select()
        {
            var list = PretService.GetList();

            if(form.textBox40.Text.Length > 0)
            {
                list = list.Where(c => c.Code.ToLower().Contains(form.textBox40.Text.ToLower())).ToList();
            }

            if(form.textBox41.Text.Length > 0)
            {
                list = list.Where(c => c.CodeTask.ToLower().Contains(form.textBox41.Text.ToLower())).ToList();
            }

            if(form.checkBox4.Checked)
            {
                list = list.Where(c => c.Date.Date >= form.dateTimePicker17.Value && c.Date.Date <= form.dateTimePicker18.Value).ToList();
            }

            if (form.checkBox5.Checked && !form.checkBox6.Checked)
            {
                list = list.Where(c => c.Type == 1).ToList();
            }
            else if (!form.checkBox5.Checked && form.checkBox6.Checked)
            {
                list = list.Where(c => c.Type == 2).ToList();
            }
            else if (!form.checkBox5.Checked && !form.checkBox6.Checked)
                list.Clear();

            var model = new List<DataGridRowModel>();

            foreach (var pret in list.OrderBy(c => c.Date))
            {
                string type;

                if (pret.Type == 1)
                    type = "Претензия";
                else
                    type = "Рецензия";

                var item = new DataGridRowModel(type, pret.Code, pret.CodeTask, pret.Date.ToString("dd.MM.yyyy"), pret.Otm.ToString());
                model.Add(item);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Тип"),
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Задание"),
                new DataGridColumnModel("Дата"),
                new DataGridColumnModel("Оценка")
            };

            SystemHelper.PullListInDataGridView(form.dataGridView12, model.ToArray(), columns);
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.dataGridView12, form.panel21, form.textBox40, form.button54);
        }

        protected override void FilterClose()
        {
            FilterClose(form.dataGridView12, form.panel21, form.textBox40, form.button54);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }


        #region Клавиши
        public void dataGridView12_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            if ((e.KeyCode == Keys.Up
                && form.dataGridView12.SelectedRows.Count != 0
                && form.dataGridView12.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
                SystemHelper.SelectDataGridView(form.dataGridView12, false);
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView12, e.KeyCode);
                e.Handled = true;
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox40, e.KeyCode);
            }
        }

        public void textBox40_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectTextBox(form.textBox41);
                e.Handled = true;
            }
        }

        public void textBox41_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox40);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectCheckBox(form.checkBox5);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
        }

        public void checkBox5_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox41);
                SystemHelper.SelectCheckBox(form.checkBox5, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectCheckBox(form.checkBox6);
                SystemHelper.SelectCheckBox(form.checkBox5, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SystemHelper.SelectCheckBox(form.checkBox5, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                form.checkBox5.Checked = !form.checkBox5.Checked;
            }

            e.IsInputKey = true;
        }

        public void checkBox6_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectCheckBox(form.checkBox5);
                SystemHelper.SelectCheckBox(form.checkBox6, false);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectCheckBox(form.checkBox4);
                SystemHelper.SelectCheckBox(form.checkBox6, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SystemHelper.SelectCheckBox(form.checkBox6, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                form.checkBox6.Checked = !form.checkBox6.Checked;
            }

            e.IsInputKey = true;
        }

        public void checkBox4_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                form.dateTimePicker17.Focus();
                SystemHelper.SelectCheckBox(form.checkBox4 , false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectCheckBox(form.checkBox6);
                SystemHelper.SelectCheckBox(form.checkBox4, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SystemHelper.SelectCheckBox(form.checkBox4, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                form.checkBox4.Checked = !form.checkBox4.Checked;
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker17_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker18.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectCheckBox(form.checkBox4);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker17, form.dateTimePicker18);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker17);
                    f.Show();
                }
            }

            e.Handled = true;

        }

        public void dateTimePicker18_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker17.Focus();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker17, form.dateTimePicker18);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker18);
                    f.Show();
                }
            }

            e.Handled = true;
        }

        #endregion
    }
}
