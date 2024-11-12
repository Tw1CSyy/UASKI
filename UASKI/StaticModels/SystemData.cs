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
        /// Индекс текущей страницы
        /// </summary>
        public static int Index { get; set; }

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
        /// Инициализация данных
        /// </summary>
        /// <param name="form">Главная форма приложения для инициализации</param>
        public static void Init(Gl_Form form)
        {
            Form = form;
            Pages = new Pages();
            MenuItems = new List<ItemMenuLevel1>
            {
               new ItemMenuLevel1
               {
                   Text = "Просмотр",
                   Items = new ItemMenuLevel2[]
                   {
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Исполнителя-Контроллера",
                           NumberTabPage = Pages.SelectIsp.Index
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Планов",
                           NumberTabPage = Pages.SelectTask.Index
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Архива",
                           NumberTabPage = Pages.SelectArhiv.Index
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Празднечных Дней",
                           NumberTabPage = 0
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Опозданий",
                           NumberTabPage = 0
                       },
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
                           NumberTabPage = Pages.AddTask.Index
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Добавление Исполнителей-Контролеров",
                           NumberTabPage = Pages.AddIsp.Index
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Добавление Празднечных Дней",
                           NumberTabPage = Pages.AddHoliday.Index
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
                           NumberTabPage = 0
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Невыполненные задания",
                           NumberTabPage = 0
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Состояние выполнения мероприятия",
                           NumberTabPage = 0
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Текущие значения показателей работы",
                           NumberTabPage = 0
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Составление коэффициента качества",
                           NumberTabPage = 0
                       }
                   }
               },
            };
            Index = 0;
            IsQuery = false;
            IsClear = false;
        }
    }
}
