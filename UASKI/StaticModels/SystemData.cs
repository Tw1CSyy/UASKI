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
        public readonly static List<ItemMenuLevel1> MenuItems = new List<ItemMenuLevel1>
        {
           new ItemMenuLevel1
           {
               Text = "Просмотр",
               Items = new ItemMenuLevel2[]
               {
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр Исполнителя-Контроллера",
                       NumberTabPage = 1
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр Планов",
                       NumberTabPage = 2
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр Архива",
                       NumberTabPage = 3
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр Празднечных Дней",
                       NumberTabPage = 4
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр Опозданий",
                       NumberTabPage = 5
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
                       NumberTabPage = 6
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Добавление Исполнителей-Контролеров",
                       NumberTabPage = 7
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Добавление Празднечных Дней",
                       NumberTabPage = 8
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
                       NumberTabPage = 12
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Невыполненные задания",
                       NumberTabPage = 13
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Состояние выполнения мероприятия",
                       NumberTabPage = 14
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Текущие значения показателей работы",
                       NumberTabPage = 15
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Состовление коэффициента качества",
                       NumberTabPage = 16
                   }
               }
           },
        };

        public static Pages Pages = new Pages();

        /// <summary>
        /// Текущая страница
        /// </summary>
        public static int Index { get; set; }

        /// <summary>
        /// Требуется ли подтверждение операции
        /// </summary>
        public static bool IsQuery { get; set; }

        public static Gl_Form Form { get; set; }

        public static readonly Keys ActionKey = Keys.F2;

    }
}
