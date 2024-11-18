using System;
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
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public AddHoliday(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;
        
        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            SystemHelper.PullListInDataGridView(form.dataGridView2,
                HolidaysService.GetListByDataGrid(),
                new DataGridRowModel("Номер" , "Дата"));

            form.dataGridView2.Columns[0].Visible = false;
            SystemHelper.ResizeDataGridView(form.dataGridView2);

            form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
            form.monthCalendar1.Focus();

            form.dataGridView2.ClearSelection();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        protected override void Clear()
        {
            form.label27.Visible = false;
            form.dataGridView2.DataSource = null;
            SystemHelper.SelectButton(false, form.button5);
        }

        /// <summary>
        /// Выход с страницы
        /// </summary>
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
                SystemHelper.SelectButton(true, form.button5);
                form.button5.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
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
                SystemHelper.SelectButton(false, form.button5);
            }

            e.IsInputKey = true;
        }
        #endregion
    }
}
