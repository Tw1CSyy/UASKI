using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Models;
using UASKI.Models.Components;
using UASKI.StaticModels;
using UASKI.Core.Models;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра архива заданий
    /// </summary>
    public class SelectArhiv : BasePageSelect
    {
        public SelectArhiv(int index) : base(index) { }
        public override DataGridViewComponent DataGridView { get => form.DataGridView5; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            if(this.IsCleared)
            {
                form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker3.Value = DateTime.Today;
                form.panel15.Visible = true;
                form.checkBox1.Checked = true;
                FilterClose();
            }
            
            Select();
            form.DataGridView5.d.Focus();
        }

        protected override void Clear()
        {
            form.textBox31.Clear();
            form.textBox32.Clear();
            form.DataGridView5.d.DataSource = null;
        }

        public override void Select()
        {
            var list = ArhivModel.GetList();

            if(form.textBox32.Text.Length > 0)
            {
                list = list.Where(c => c.Code.ToLower().Contains(form.textBox32.Text.ToLower())).ToList();
            }

            if(form.textBox31.Text.Length > 0 && int.TryParse(form.textBox31.Text , out int j))
            {
                list = list.Where(c => c.IdIsp == Convert.ToInt32(form.textBox31.Text) || c.IdCon == Convert.ToInt32(form.textBox31.Text) ||
                c.Isp.CodePodr == Convert.ToInt32(form.textBox31.Text) || c.Con.CodePodr == Convert.ToInt32(form.textBox31.Text)).ToList();
            }

            if(form.checkBox1.Checked)
            {
                list = list.Where(c => c.Date >= form.dateTimePicker2.Value && c.Date <= form.dateTimePicker3.Value).ToList();
            }

            var model = new List<DataGridRowModel>();
           
            foreach (var item in list.OrderByDescending(c => c.DateClose))
            {
                
                var st = new DataGridRowModel(item.Id.ToString(),
                    item.Code,
                    item.Isp.InizByCode,
                    item.Con.InizByCode,
                    item.Date.ToString("dd.MM.yyyy"), item.DateClose.ToString("dd.MM.yyyy"),
                    item.Otm.ToString());

                model.Add(st);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Id" , typeof(string) , false),
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контроллер"),
                new DataGridColumnModel("Срок" , typeof(DateTime)),
                new DataGridColumnModel("Дата закрытия" , typeof(DateTime)),
                new DataGridColumnModel("Оценка" , typeof(int))
            };

            form.DataGridView5.PullListInDataGridView(model.ToArray(), columns);

        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.DataGridView5, form.panel14, form.textBox32, form.button16);
        }

        protected override void FilterClose()
        {
            FilterClose(form.DataGridView5, form.panel14, form.textBox32, form.button16);
        }

        #region Клавиши
        public void dataGridView5_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
                form.DataGridView5.d.ClearSelection();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && form.DataGridView5.d.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(form.DataGridView5.d.SelectedRows[0].Cells[0].Value);

                SystemData.Pages.EditTask.Init(false , false);
                SystemData.Pages.EditTask.Show(id, true , this);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else
            {
                form.DataGridView5.KeyDown(e, form.textBox32);
            }
        }

        public void textBox32_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectTextBox(form.textBox31);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
        }

        public void textBox31_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox32);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new IspForm(new TextBox() , new TextBox() , form.textBox31);
                f.Show();
                e.Handled = true;
            }
        }

        public void checkBox1_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox1.Checked = !form.checkBox1.Checked;
                
            }
            else if (e.KeyCode == Keys.Down && form.panel15.Visible)
            {
                form.dateTimePicker2.Focus();
                SelectCheckBox(form.checkBox1 , false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox31);
                SelectCheckBox(form.checkBox1, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox1, false);
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker2_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker3.Focus();
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox1);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker2);
                    f.Show();
                }
            }

            e.Handled = true;
        }

        public void dateTimePicker3_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Right)
            {
                FilterClose();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker2.Focus();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker2, form.dateTimePicker3);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker3);
                    f.Show();
                }
            }

            e.Handled = true;
        }
        #endregion

    }
}
