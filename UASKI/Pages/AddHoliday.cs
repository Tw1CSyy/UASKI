using System;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class AddHoliday : BasePage
    {
        private Gl_Form form = SystemData.Form;

        public override void Show()
        {
            form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            SystemHelper.PullListInDataGridView(form.dataGridView2,
                HolidaysService.GetListByDataGrid(),
                new DataGridRowModel("Дата"));

            form.monthCalendar1.Focus();
        }

        public override void Clear()
        {
            SystemHelper.PullListInDataGridView(form.dataGridView2,
                       HolidaysService.GetListByDataGrid(),
                       new DataGridRowModel("Дата"));

            form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            form.monthCalendar1.Focus();
            SystemHelper.SelectButton(false, form.button5);
        }

        public void monthCalendar1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button5);
                form.button5.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public void button5_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.monthCalendar1.Focus();
                SystemHelper.SelectButton(false, form.button5);

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
                        NavigationHelper.ClearForm();
                        ErrorHelper.StatusComlite();
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
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button5);
            }

            e.IsInputKey = true;
        }
    }
}
