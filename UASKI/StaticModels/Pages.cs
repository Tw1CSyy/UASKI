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
        public readonly SelectHoliday SelectHoliday = new SelectHoliday(4);
        public readonly SelectOpz SelectOpz = new SelectOpz(5);
        public readonly AddTask AddTask = new AddTask(6);
        public readonly AddIsp AddIsp = new AddIsp(7);
        public readonly AddHoliday AddHoliday = new AddHoliday(8);
        public readonly EditIsp EditIsp = new EditIsp(9);
        public readonly EditTask EditTask = new EditTask(10);
        public readonly PrintTaskList PrintTaskList = new PrintTaskList(11);
        
    }
}
