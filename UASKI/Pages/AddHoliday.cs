using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы добавления праздника
    /// </summary>
    public class AddHoliday : BasePage
    {
        public AddHoliday(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;
        
        protected override void Show()
        {
            form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            Select();
            form.dataGridView2.Columns[0].Visible = false;
            SystemHelper.ResizeDataGridView(form.dataGridView2);

            form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            form.monthCalendar1.Focus();

            form.dataGridView2.ClearSelection();
        }

        private void Select()
        {
            var result = new List<DataGridRowModel>();
            var model = HolidaysService.GetList();

            foreach (var item in model)
            {
                var d = new DataGridRowModel(item.Id.ToString(), item.Date.ToString("dd.MM.yyyy"));
                result.Add(d);
            }

            SystemHelper.PullListInDataGridView(form.dataGridView2,
                result,
                new DataGridRowModel("Номер", "Дата"));

        }
        protected override void Clear()
        {
            form.label27.Visible = false;
            form.dataGridView2.DataSource = null;
            SystemHelper.SelectButton(form.button5, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        #region Клавиши
        public void monthCalendar1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(form.button5);
                form.button5.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void button5_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.monthCalendar1.Focus();
                SystemHelper.SelectButton(form.button5, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = HolidaysService.Add
                    (
                        MonthElement.New(form.monthCalendar1, form.label27)
                    );

                    if (result)
                    {
                        ClearPage();
                        ErrorHelper.StatusComlite();
                        Show();
                    }
                    else
                    {
                        ErrorHelper.StatusError();
                    }
                }
                else
                {
                    ErrorHelper.StatusQuery();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                SystemHelper.SelectButton(form.button5, false);
            }

            e.IsInputKey = true;
        }
        #endregion
    }
}
