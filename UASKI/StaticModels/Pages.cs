using System.Linq;
using UASKI.Models;
using UASKI.Models.Pages;
using UASKI.Pages;

namespace UASKI.StaticModels
{
    /// <summary>
    /// Страницы приложения
    /// </summary>
    public class Pages
    {
        public SelectIsp SelectIsp = new SelectIsp(1);
        public SelectTask SelectTask = new SelectTask(2);
        public SelectArhiv SelectArhiv = new SelectArhiv(3);

        public AddTask AddTask = new AddTask(6);
        public AddIsp AddIsp = new AddIsp(7);
        public AddHoliday AddHoliday = new AddHoliday(8);
        public EditIsp EditIsp = new EditIsp(9);
        public EditTask EditTask = new EditTask(10);

       
        /// <summary>
        /// Открывает страницу по индексу из меню
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public void Open()
        {
            BasePage[] List =
            {
                AddHoliday,
                AddIsp,
                AddTask,
                EditIsp,
                EditTask,
                SelectArhiv,
                SelectIsp,
                SelectTask
            };

            var form = SystemData.Form;
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(form.Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(form.Menu_Step2.SelectedItem.ToString()));

            var item = List.FirstOrDefault(page => page.Index == el.NumberTabPage);

            if (item != null)
                item.Init();
        }

        /// <summary>
        /// Отчищает сраницу по текущей странице
        /// </summary>
        public void Clear()
        {
            BasePage[] List =
            {
                AddHoliday,
                AddIsp,
                AddTask,
                EditIsp,
                EditTask,
                SelectArhiv,
                SelectIsp,
                SelectTask
            };

            var item = List.FirstOrDefault(page => page.Index == SystemData.Index);

            if (item != null)
            {
                SystemData.IsClear = true;
                item.Clear();
                SystemData.IsClear = false;
            }
        }

    }
}
