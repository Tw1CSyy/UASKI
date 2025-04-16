using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Enums;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы добавления праздника
    /// </summary>
    public class AddHoliday : BasePage
    {
        public AddHoliday(int index, TypePage type) : base(index, type) { }

        protected override void Show()
        {
            form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            Select();
            form.DataGridView2.d.Columns[0].Visible = false;
            form.DataGridView2.ResizeDataGridView();

            form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            
            form.DataGridView2.d.ClearSelection();
            form.monthCalendar1.Focus();
        }

        private void Select()
        {
            var result = new List<DataGridRowModel>();
            var model = HolidayModel.GetList();

            foreach (var item in model)
            {
                var d = new DataGridRowModel(item.Id.ToString(), item.Date.ToString("dd.MM.yyyy"));
                result.Add(d);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Номер", typeof(int), false),
                new DataGridColumnModel("Дата", typeof(DateTime))
            };

            form.DataGridView2.PullListInDataGridView(result.ToArray(), columns);
        }
        protected override void Clear()
        {
            form.label27.Visible = false;
            form.DataGridView2.d.DataSource = null;
            SelectButton(form.button5, false);
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
        public void monthCalendar1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button5);
                form.button5.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public bool button5_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.monthCalendar1.Focus();
                SelectButton(form.button5, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                var date = MonthElement.New(form.monthCalendar1, form.label27);

                var result = ValidationHelper.HolidayValidation(date);

                if (!result)
                {
                    Ai.Error();
                    return false;
                }

                foreach (var data in date.DateRange)
                {
                    var holy = new HolidayModel(data.Date);
                    result = holy.Add();

                    if (!result)
                        break;
                }

                if (result)
                {
                    ClearPage();
                    Ai.Comlite("Празднечный день добавлен");
                    Show();
                }
                else
                {

                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                SelectButton(form.button5, false);
            }

            e.IsInputKey = true;
            return true;
        }
        #endregion
    }
}
