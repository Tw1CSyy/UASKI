using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
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
                       Text = "Просмотр исполнителя-контролера",
                       NumberTabPage = 1
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр опозданий",
                       NumberTabPage = 2
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Просмотр планов",
                       NumberTabPage = 3
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
                       Text = "Добавление новых карточек",
                       NumberTabPage = 4
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Добавление исполнителей-контролеров",
                       NumberTabPage = 5
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Добавление празднечных дней",
                       NumberTabPage = 6
                   }
               }
           },
           new ItemMenuLevel1
           {
               Text = "Корректировка",
               Items = new ItemMenuLevel2[]
               {
                   new ItemMenuLevel2
                   {
                       Text = "Корректировка исполнителей-контролеров",
                       NumberTabPage = 7
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Корректировка карточек",
                       NumberTabPage = 8
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Корректировка архива",
                       NumberTabPage = 9
                   }
               }
           },
           new ItemMenuLevel1
           {
               Text = "Оперативная обработка информации",
               Items = new ItemMenuLevel2[]
               {
                   new ItemMenuLevel2
                   {
                       Text = "Закрытие карточек",
                       NumberTabPage = 10
                   },
                   new ItemMenuLevel2
                   {
                       Text = "Расчет коэффицента качества",
                       NumberTabPage = 11
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

        /// <summary>
        /// Объект главной формы
        /// </summary>
        public static Gl_Form Form { get; set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public static int Index { get; set; }

        /// <summary>
        /// Требуется ли подтверждение операции
        /// </summary>
        public static bool IsQuery { get; set; }

    }
}
