using System.Collections.Generic;
using System.Linq;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;
using System.Windows.Forms.VisualStyles;
using System.Reflection;
using System;
using UASKI.Data.Entityes;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для навигации между табами (далее формы)
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Расчитывает индекс страницы для перехода
        /// </summary>
        public static void Start()
        {
            var form = SystemData.Form;
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(form.Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(form.Menu_Step2.SelectedItem.ToString()));

            GetView(el.NumberTabPage);
        }

        /// <summary>
        /// Загружает контент на форму
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        /// <param name="id">Id когда необходим</param>
        private static void GetView(int index , int id = 0)
        {
            var form = SystemData.Form;

            if (SystemData.Index != index)
            {
                ClearForm(SystemData.Index);
            }

            form.tabControl1.SelectedIndex = index;
            SystemData.Index = index;

            switch (index)
            {
                case 1:
                    SystemHelper.PullListInDataGridView(form.IspDataGridView
                        , IspService.GetListByDataGrid(form.textBox13.Text)
                        , new DataGridRowModel("Табельный номер" , "Фамилия" , "Имя" , "Отчество" , "Код подразделения"));
                    form.IspDataGridView.Focus();
                    break;

                case 4:
                    form.textBox1.Focus();
                break;

                case 5:
                    SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid()
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
                    form.IspDataGridView.Focus();
                    form.textBox8.Focus();
                    form.dataGridView1.ClearSelection();
                    break;
                case 6:
                    form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
                    SystemHelper.PullListInDataGridView(form.dataGridView2,
                        HolidaysService.GetListByDataGrid(),
                        new DataGridRowModel("Дата"));

                    form.monthCalendar1.Focus();
                    break;
                case 7:
                    var isp = IspService.GetByCode(id);

                    form.textBox18.Text = isp.FirstName;
                    form.textBox17.Text = isp.Name;
                    form.textBox16.Text = isp.LastName;
                    form.textBox15.Text = isp.Code.ToString();
                    form.textBox14.Text = isp.CodePodr.ToString();
                    form.label38.Text = isp.Code.ToString();

                    form.textBox18.Focus();
                    form.textBox18.SelectionStart = form.textBox18.Text.Length;
                    break;
            }

            form.Menu_Step2.Enabled = false;
        }

        /// <summary>
        /// Отчищает форму по индексу или по текущей странице
        /// </summary>
        /// <param name="index">Индекс формы</param>
        public static void ClearForm(int index = -1)
        {
            var form = SystemData.Form;

            if(index == -1)
            {
                index = SystemData.Index;
            }

            switch (index)
            {
                case 1:
                    form.textBox13.Clear();
                    form.IspDataGridView.DataSource = null;
                    break;

                case 4:
                    form.textBox1.Clear();
                    form.textBox2.Clear();
                    form.textBox3.Clear();
                    form.textBox4.Clear();
                    form.textBox5.Clear();
                    form.textBox6.Clear();
                    form.textBox7.Clear();

                    SystemHelper.SelectButton(false, form.button1);
                    form.dateTimePicker1.Value = System.DateTime.Today;
                    form.textBox1.Focus();
                    break;
                case 5:
                    SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid()
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));

                    form.textBox8.Clear();
                    form.textBox9.Clear();
                    form.textBox10.Clear();
                    form.textBox11.Clear();
                    form.textBox12.Clear();

                    form.textBox8.Focus();
                    SystemHelper.SelectButton(false, form.button4);
                    break;
                case 6:
                    SystemHelper.PullListInDataGridView(form.dataGridView2,
                        HolidaysService.GetListByDataGrid(),
                        new DataGridRowModel("Дата"));

                    form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
                    form.monthCalendar1.Focus();
                    SystemHelper.SelectButton(false , form.button5);
                    break;
                case 7:
                    form.textBox18.Clear();
                    form.textBox17.Clear();
                    form.textBox16.Clear();
                    form.textBox15.Clear();
                    form.textBox14.Clear();
                    SystemHelper.SelectButton(false, form.button6);
                    SystemHelper.SelectButton(false, form.button7);
                    break;
            }
        }

        public static void GetIspView(int code)
        {
            GetView(7 , code);
        }
    }
}
