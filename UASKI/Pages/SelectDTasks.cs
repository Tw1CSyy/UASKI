using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Models;
using UASKI.Models.Components;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class SelectDTasks : BasePageSelect
    {
        public SelectDTasks(int index) : base(index) { }
        public override DataGridViewComponent DataGridView { get => form.DataGridView15; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            if (IsCleared)
            {
                form.panel31.Visible = false;
                form.dateTimePicker22.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                form.dateTimePicker23.Value = DateTime.Today;
                FilterClose();
            }

            Select();
            form.DataGridView15.d.Focus();
        }

        protected override void Clear()
        {
            form.textBox44.Clear();
            form.DataGridView15.d.DataSource = null;
        }

        public override void Select()
        {
            var arhiv = ArhivModel.GetList().Where(c => c.IsDouble).ToList();
            var isps = IspModel.GetList();

            if(form.textBox44.Text.Length > 0)
            {
                arhiv = arhiv.Where(c => c.Code.ToLower().Contains(form.textBox44.Text.ToLower())).ToList();
            }

            if(form.checkBox9.Checked)
            {
                arhiv = arhiv.Where(c => c.DateClose >= form.dateTimePicker22.Value && c.DateClose <= form.dateTimePicker23.Value).ToList();
            }

            var model = arhiv.OrderBy(c => c.Date).ThenBy(c => c.Id)
                .GroupBy(g => new { g.Code, g.IdCon })
                .Select(g => new DataGridRowModel(g.Key.Code, g.Key.IdCon.ToString(), g.Count().ToString()))
                .ToList();

            for(int i = 0; i < model.Count; i++)
            {
                var id = Convert.ToInt32(model[i].Values[1]);
                var con = isps.FirstOrDefault(c => c.Code == id);

                if (con != null)
                    model[i].Values[1] = con.InizByCode;
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Контролёр"),
                new DataGridColumnModel("Кол-Во")
            };

            form.DataGridView15.PullListInDataGridView(model.ToArray(), columns);
            Ai.AddMessage(Enums.TypeNotice.Default, $"Количество: {model.Count}");
        }

        protected override void FilterOpen()
        {
            FilterOpen(DataGridView, form.panel30, form.textBox44, form.button57);
        }

        protected override void FilterClose()
        {
            FilterClose(DataGridView, form.panel30, form.textBox44, form.button57);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }

        #region Клавиши
        public void dataGridView15_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == SystemData.ActionKey)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                form.DataGridView15.d.ClearSelection();
                e.Handled = true;
            }
            else
            {
                form.DataGridView15.KeyDown(e, form.textBox44);
            }
        }

        public void textBox44_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox9);
                e.Handled = true;
            }
        }

        public void checkBox9_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox9.Checked = !form.checkBox9.Checked;
            }
            else if (e.KeyCode == Keys.Down && form.panel31.Visible)
            {
                form.dateTimePicker22.Focus();
                SelectCheckBox(form.checkBox9, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox44);
                SelectCheckBox(form.checkBox9, false);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                SelectCheckBox(form.checkBox9, false);
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker22_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker23.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectCheckBox(form.checkBox9);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker22, form.dateTimePicker23);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker22);
                    f.Show();
                }
            }

            e.Handled = true;
        }

        public void dateTimePicker23_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker22.Focus();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                if (e.Control)
                {
                    var f = new DateForm(form.dateTimePicker22, form.dateTimePicker23);
                    f.Show();
                }
                else
                {
                    var f = new DateForm(form.dateTimePicker23);
                    f.Show();
                }
            }

            e.Handled = true;
        }
        #endregion
    }
}
