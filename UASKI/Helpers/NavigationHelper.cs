using System.Collections.Generic;
using System.Linq;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;
using System.Windows.Forms.VisualStyles;
using System.Reflection;
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
                    SystemData.Pages.SelectIsp.Show();
                    break;
                // Просмотр планов
                case 2:
                    SystemData.Pages.SelectTask.Show();
                    break;
                // Просмотр Архива
                case 3:
                    SystemData.Pages.SelectArhiv.Show();
                    break;
                //Добавление карточек
                case 6:
                    SystemData.Pages.AddTask.Show();
                break;
                // Добавление исполнителя
                case 7:
                    SystemData.Pages.AddIsp.Show();
                    break;
                // Добавление праздника
                case 8:
                    SystemData.Pages.AddHoliday.Show();
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
                    SystemData.Pages.SelectIsp.Clear();
                    break;
                case 2:
                    SystemData.Pages.SelectTask.Clear();
                    break;
                case 3:
                    SystemData.Pages.SelectArhiv.Clear();
                    break;
                case 6:
                    SystemData.Pages.AddTask.Clear();
                    break;
                case 7:
                    SystemData.Pages.AddIsp.Clear();
                    break;
                case 8:
                    SystemData.Pages.AddHoliday.Clear();
                    break;
                case IndexIsp:
                    SystemData.Pages.EditIsp.Clear();
                    break;
                case IndexTasks:
                    SystemData.Pages.EditTask.Clear();
                    break;
            }
        }

        /// <summary>
        /// Переходит на страницу управления сотрудниками
        /// </summary>
        /// <param name="code">Код сотрудника</param>
        public static void GetIspView(int code)
        {
            GetView(IndexIsp);
            SystemData.Pages.EditIsp.Show(code);
            
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
            GetView(IndexTasks);
            SystemData.Pages.EditTask.Show(code, IsArhiv);
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
