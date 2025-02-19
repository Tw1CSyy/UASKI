using System.Collections.Generic;
using System.Windows.Forms;
using UASKI.Models;


namespace UASKI.StaticModels
{
    /// <summary>
    /// Класс для хранения ститических общих данных
    /// </summary>
    public static class SystemData
    {
        /// <summary>
        /// Список элементов меню на главной странице
        /// </summary>
        public static List<ItemMenuLevel1> MenuItems { get; private set; }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        public static Gl_Form Form { get; private set; }

        /// <summary>
        /// Требуется ли подтверждение операции
        /// </summary>
        public static bool IsQuery { get; set; }

        /// <summary>
        /// Производится ли отчистка элементов
        /// </summary>
        public static bool IsClear { get; set; }

        /// <summary>
        /// Жаркая главиша
        /// </summary>
        public static readonly Keys ActionKey = Keys.F2;

        /// <summary>
        /// Страницы приложения
        /// </summary>
        public static Pages Pages { get; private set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public static BasePage This { get; set; }

        /// <summary>
        /// Настройки приложения
        /// </summary>
        public static AppSettings Settings { get; set; }

        /// <summary>
        /// Инициализация данных
        /// </summary>
        /// <param name="form">Главная форма приложения для инициализации</param>
        public static void Init(Gl_Form form)
        {
            Form = form;
            Pages = new Pages();
            IsQuery = false;
            IsClear = false;

            MenuItems = new List<ItemMenuLevel1>
            {
               new ItemMenuLevel1
               {
                   Text = "Просмотр",
                   Items = new ItemMenuLevel2[]
                   {
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Исполнителя-контролера",
                           Page = Pages.SelectIsp
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Планов",
                           Page = Pages.SelectTask
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Архива",
                           Page = Pages.SelectArhiv
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Празднечных Дней",
                           Page = Pages.SelectHoliday
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Опозданий",
                           Page = Pages.SelectOpz
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Претензий/Рецензий",
                           Page = Pages.SelectPret
                       }
                   }
               },
               new ItemMenuLevel1
               {
                   Text = "Добавление" ,
                   Items = new ItemMenuLevel2[]
                   {
                       new ItemMenuLevel2
                       {
                           Text = "Добавление Новых Карточек",
                           Page = Pages.AddTask
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Добавление Исполнителей-Контролеров",
                           Page = Pages.AddIsp
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Добавление Празднечных Дней",
                           Page = Pages.AddHoliday
                       }
                   }
               },
               new ItemMenuLevel1
               {
                   Text = "Печатные формы",
                   Items = new ItemMenuLevel2[]
                   {
                       new ItemMenuLevel2
                       {
                           Text = "Перечень заданий на месяц",
                           Page = Pages.PrintTaskList
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Невыполненные задания",
                           Page = Pages.PrintOpz
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Состояние выполнения мероприятия",
                           Page = Pages.PrintMer
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Текущие значения показателей работы",
                           Page = Pages.PrintPoc
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Составление коэффициента качества",
                           Page = Pages.PrintCof
                       }
                   }
               },
            };
        }
    }
}
