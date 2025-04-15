using UASKI.Models.Pages;
using UASKI.Pages;
using UASKI.Enums;

namespace UASKI.StaticModels
{
    /// <summary>
    /// Страницы приложения
    /// </summary>
    public class Pages
    {
        public readonly SelectIsp SelectIsp = new SelectIsp(1, TypePage.Select);
        public readonly SelectTask SelectTask = new SelectTask(2, TypePage.Select);
        public readonly SelectArhiv SelectArhiv = new SelectArhiv(3, TypePage.Select);
        public readonly SelectHoliday SelectHoliday = new SelectHoliday(4, TypePage.Select);
        public readonly SelectOpz SelectOpz = new SelectOpz(5, TypePage.Select);
        public readonly AddTask AddTask = new AddTask(6, TypePage.Defult);
        public readonly AddIsp AddIsp = new AddIsp(7, TypePage.Defult);
        public readonly AddHoliday AddHoliday = new AddHoliday(8, TypePage.Defult);
        public readonly EditIsp EditIsp = new EditIsp(9, TypePage.Edit);
        public readonly EditTask EditTask = new EditTask(10, TypePage.Edit);
        public readonly PrintTaskList PrintTaskList = new PrintTaskList(11, TypePage.Print);
        public readonly PrintOpz PrintOpz = new PrintOpz(12, TypePage.Print);
        public readonly PrintMer PrintMer = new PrintMer(13, TypePage.Print);
        public readonly PrintPoc PrintPoc = new PrintPoc(14, TypePage.Print);
        public readonly PrintCof PrintCof = new PrintCof(15, TypePage.Print);
        public readonly EditPret EditPret = new EditPret(16, TypePage.Edit);
        public readonly SelectPret SelectPret = new SelectPret(17, TypePage.Select);
        public readonly PrintPlan PrintPlan = new PrintPlan(18, TypePage.Print);
        public readonly SelectDTasks SelectDTasks = new SelectDTasks(19, TypePage.Select);
    }
}
