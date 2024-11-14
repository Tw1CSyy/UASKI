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
        public readonly SelectIsp SelectIsp = new SelectIsp(1);
        public readonly SelectTask SelectTask = new SelectTask(2);
        public readonly SelectArhiv SelectArhiv = new SelectArhiv(3);

        public readonly SelectOpz SelectOpz = new SelectOpz(5);
        public readonly AddTask AddTask = new AddTask(6);
        public readonly AddIsp AddIsp = new AddIsp(7);
        public readonly AddHoliday AddHoliday = new AddHoliday(8);
        public readonly EditIsp EditIsp = new EditIsp(9);
        public readonly EditTask EditTask = new EditTask(10);
        public BasePage This { get; set; }
       
        /// <summary>
        /// Открывает страницу по индексу из меню
        /// </summary>
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
                SelectTask,
                SelectOpz
            };

            BasePage d = new EditTask(10);

            var form = SystemData.Form;
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(form.Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(form.Menu_Step2.SelectedItem.ToString()));

            var item = List.FirstOrDefault(page => page.Index == el.NumberTabPage);

            if (item != null)
                item.Init();
        }

        /// <summary>
        /// Отчищает по текущей странице
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
                SelectTask,
                SelectOpz
            };

            var item = List.FirstOrDefault(page => page.Index == SystemData.Index);

            if (item != null)
                item.ClearPage();
        }

    }
}
