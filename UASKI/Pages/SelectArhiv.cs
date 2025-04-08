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
            var isps = IspModel.GetList();

            if(form.textBox32.Text.Length > 0)
            {
                list = list.Where(c => c.Code.ToLower().Contains(form.textBox32.Text.ToLower())).ToList();
            }

            if(form.textBox31.Text.Length > 0 && int.TryParse(form.textBox31.Text , out int j))
            {
                list = list.Where(c => c.GetIsp(isps).CodePodr == Convert.ToInt32(form.textBox31.Text) || c.GetCon(isps).CodePodr == Convert.ToInt32(form.textBox31.Text)).ToList();
            }

            if(form.checkBox1.Checked)
            {
                list = list.Where(c => c.Date >= form.dateTimePicker2.Value && c.Date <= form.dateTimePicker3.Value).ToList();
            }

            var model = new List<DataGridRowModel>();
            model = list.OrderByDescending(c => c.DateClose).ThenBy(c => c.Id)
                .Select(c => new DataGridRowModel(
                    c.Id.ToString(),
                    c.Code,
                    c.GetIsp(isps).InizByCode,
                    c.GetCon(isps).InizByCode,
                    c.Date.ToString("dd.MM.yyyy"),
                    c.DateClose.ToString("dd.MM.yyyy"),
                    c.Otm.ToString()))
                .ToList();

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Id" , typeof(string) , false),
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("контролер"),
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
                var f = new IspForm(new TextBox(), form.textBox31, new TextBox());
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
