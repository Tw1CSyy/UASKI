using System;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using System.Collections.Generic;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра праздников
    /// </summary>
    public class SelectHoliday : BasePageSelect
    {
        public SelectHoliday(int index) : base(index) { }
        public override DataGridView DataGridView { get => form.dataGridView6; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            Select();
            form.dataGridView6.Focus();
            SystemHelper.SelectButton(form.button13, false);
        }

        protected override void Clear()
        {
            form.dataGridView6.DataSource = null;
            SystemHelper.SelectButton(form.button13, false);
        }

        public override void Select()
        {
            var result = new List<DataGridRowModel>();
            var model = HolidaysService.GetList();

            foreach (var item in model)
            {
                var d = new DataGridRowModel(item.Id.ToString(), item.Date.ToString("dd.MM.yyyy"));
                result.Add(d);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Номер" , false),
                new DataGridColumnModel("Дата")
            };

            SystemHelper.PullListInDataGridView(form.dataGridView6, result.ToArray(), columns);

        }

        protected override void Exit()
        {
            form.dataGridView6.ClearSelection();
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SystemHelper.SelectButton(form.button13, false);
        }

        protected override void FilterOpen()
        {
            
        }

        protected override void FilterClose()
        {

        }


        #region Клавиши
        public void dataGridView6_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(form.button13);
            }
            else if ((e.KeyCode == Keys.Up
               && form.dataGridView6.SelectedRows.Count != 0
               && form.dataGridView6.SelectedRows[0].Index == 0)
               || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView6, e.KeyCode);
                e.Handled = true;
            }
        }

        public void button13_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                form.dataGridView6.Focus();
                SystemHelper.SelectButton(form.button13, false);
            }
            else if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    ErrorHelper.StatusWait();

                    var list = new List<int>();

                    foreach (DataGridViewRow item in form.dataGridView6.SelectedRows)
                    {
                        list.Add(Convert.ToInt32(item.Cells[0].Value));
                    }

                    var result = HolidaysService.Delete(list , HolidaysService.GetList());

                    if (result)
                    {
                        ErrorHelper.StatusComlite();
                        Show();
                    }
                    else
                        ErrorHelper.StatusError();
                }
                else
                    ErrorHelper.StatusQuery();

            }

            e.IsInputKey = true;
        }

        #endregion
    }
}
