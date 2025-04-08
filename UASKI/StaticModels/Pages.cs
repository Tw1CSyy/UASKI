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
        public readonly PrintOpz PrintOpz = new PrintOpz(12);
        public readonly PrintMer PrintMer = new PrintMer(13);
        public readonly PrintPoc PrintPoc = new PrintPoc(14);
        public readonly PrintCof PrintCof = new PrintCof(15);
        public readonly EditPret EditPret = new EditPret(16);
        public readonly SelectPret SelectPret = new SelectPret(17);
        public readonly PrintPlan PrintPlan = new PrintPlan(18);
    }
}
