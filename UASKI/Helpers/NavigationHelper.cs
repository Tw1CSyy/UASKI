using System.Collections.Generic;
using System.Linq;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;
using System.Windows.Forms.VisualStyles;
using System.Reflection;
using System;
using UASKI.Data.Entityes;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для навигации между табами (далее формы)
    /// </summary>
    public static class NavigationHelper
    {
        private const int IndexIsp = 9;
        private const int IndexTasks = 10;

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
        private static void GetView(int index)
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
                // Просмотр Исп-Кон
                case 1:
                    SystemHelper.PullListInDataGridView(form.IspDataGridView
                        , IspService.GetListByDataGrid(IspService.GetList(form.textBox13.Text))
                        , new DataGridRowModel("Табельный номер" , "Фамилия" , "Имя" , "Отчество" , "Код подразделения"));
                    form.IspDataGridView.Focus();
                    break;
                // Просмотр планов
                case 2:
                     SystemHelper.PullListInDataGridView(form.dataGridView3,
                        TasksService.GetListByDataGrid(TasksService.GetListTask(form.textBox19.Text)),
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));

                    form.dataGridView3.Focus();
                    break;
                // Просмотр Архива
                case 3:
                    form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    form.dateTimePicker3.Value = DateTime.Today;

                    SystemHelper.PullListInDataGridView(form.dataGridView5,
                        TasksService.GetListByDataGrid(TasksService.GetListArhiv(form.dateTimePicker2.Value, form.dateTimePicker3.Value)),
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок" , "Дата закрытия" , "Оценка" , "Номер"));
                    form.dataGridView5.Focus();
                    break;

                //Добавление карточек
                case 6:
                    form.textBox1.Focus();
                break;

                // Добавление исполнителя
                case 7:
                    SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid(IspService.GetList())
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
                    form.textBox8.Focus();
                    SystemHelper.SelectDataGridView(false, form.dataGridView1);
                    break;
                // Добавление праздника
                case 8:
                    form.label17.Text = form.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
                    SystemHelper.PullListInDataGridView(form.dataGridView2,
                        HolidaysService.GetListByDataGrid(),
                        new DataGridRowModel("Дата"));

                    form.monthCalendar1.Focus();
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
                case 2:
                    form.textBox18.Clear();
                    form.dataGridView3.DataSource = null;
                    break;
                case 3:
                    form.dataGridView5.DataSource = null;
                    break;
                case 6:
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
                case 7:
                    SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid(IspService.GetList())
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));

                    form.textBox8.Clear();
                    form.textBox9.Clear();
                    form.textBox10.Clear();
                    form.textBox11.Clear();
                    form.textBox12.Clear();

                    form.textBox8.Focus();
                    SystemHelper.SelectButton(false, form.button4);
                    break;
                case 8:
                    SystemHelper.PullListInDataGridView(form.dataGridView2,
                        HolidaysService.GetListByDataGrid(),
                        new DataGridRowModel("Дата"));

                    form.monthCalendar1.SetSelectionRange(DateTime.Today, DateTime.Today);
                    form.monthCalendar1.Focus();
                    SystemHelper.SelectButton(false , form.button5);
                    break;
                case IndexIsp:
                    form.textBox18.Clear();
                    form.textBox17.Clear();
                    form.textBox16.Clear();
                    form.textBox15.Clear();
                    form.textBox14.Clear();

                    form.dataGridView4.DataSource = null;
                    SystemHelper.SelectButton(false, form.button6);
                    SystemHelper.SelectButton(false, form.button7);
                    form.textBox18.Focus();
                    break;
                case IndexTasks:
                    form.textBox24.Clear();
                    form.textBox25.Clear();
                    form.textBox26.Clear();
                    form.textBox27.Clear();
                    form.textBox28.Clear();
                    form.textBox29.Clear();
                    form.panel10.Visible = false;
                    form.button11.Enabled = form.button12.Enabled = false;
                    SystemHelper.SelectButton(false, form.button10);
                    SystemHelper.SelectButton(false, form.button11);
                    SystemHelper.SelectButton(false, form.button12);
                    break;
            }
        }

        /// <summary>
        /// Переходит на страницу управления сотрудниками
        /// </summary>
        /// <param name="code">Код сотрудника</param>
        public static void GetIspView(int code)
        {
            var form = SystemData.Form;

            var isp = IspService.GetByCode(code);

            form.textBox18.Text = isp.FirstName;
            form.textBox17.Text = isp.Name;
            form.textBox16.Text = isp.LastName;
            form.textBox15.Text = isp.Code.ToString();
            form.textBox14.Text = isp.CodePodr.ToString();
            form.label38.Text = isp.Code.ToString();

            SystemHelper.PullListInDataGridView(form.dataGridView4,
                 TasksService.GetListByDataGrid(TasksService.GetListTask("", code)),
                 new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));
            SystemHelper.SelectTextBox(form.textBox18);

            GetView(IndexIsp);
        }

        /// <summary>
        /// Переходит на страницу списка сотрудников
        /// </summary>
        public static void GetIspSelectView()
        {
            GetView(1);
        }

        /// <summary>
        /// Переходит на страницу управления задачами и архивом
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="IsArhiv">Данные из архива?</param>
        public static void GetTaskOrArhiv(string code , bool IsArhiv)
        {
            var form = SystemData.Form;
           
            if (!IsArhiv)
            {
                var task = TasksService.GetTaskByCode(code);

                var usr1 = IspService.GetByCode(task.IdIsp);
                var usr2 = IspService.GetByCode(task.IdCon);

                form.textBox26.Text = $"{usr1.FirstName} {usr1.Name.ToUpper()[0]}. {usr1.LastName.ToUpper()[0]}.";
                form.textBox21.Text = $"{usr2.FirstName} {usr2.Name.ToUpper()[0]}. {usr2.LastName.ToUpper()[0]}.";

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.label54.Text = task.Code.ToString();
                form.label54.Enabled = true;

                form.textBox27.Text = task.Code;
                form.dateTimePicker4.Value = task.Date;

                form.button11.Enabled = form.button12.Enabled = true;
            }
            else
            {
                var arhiv = TasksService.GetArhivByCode(code);

                var usr1 = IspService.GetByCode(arhiv.IdIsp);
                var usr2 = IspService.GetByCode(arhiv.IdCon);

                form.textBox26.Text = $"{usr1.FirstName} {usr1.Name.ToUpper()[0]}. {usr1.LastName.ToUpper()[0]}.";
                form.textBox21.Text = $"{usr2.FirstName} {usr2.Name.ToUpper()[0]}. {usr2.LastName.ToUpper()[0]}.";

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.label54.Text = arhiv.Code.ToString();
                form.label54.Enabled = false;

                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button12.Enabled = true;

                form.dateTimePicker5.Value = arhiv.DateClose;
                form.textBox28.Text = arhiv.Otm.ToString();
                form.textBox29.Text = arhiv.Num.ToString();

                form.panel10.Visible = true;
            }

            GetView(IndexTasks);
        }

        /// <summary>
        /// Переходит на страницу списка задач
        /// </summary>
        public static void GetTaskSelectView()
        {
            GetView(2);
        }

        /// <summary>
        /// Переходит на страницу списка архивных задач
        /// </summary>
        public static void GetArhivSelectView()
        {
            GetView(3);
        }
    }
}
