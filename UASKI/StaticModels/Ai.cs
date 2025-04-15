using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Enums;
using UASKI.Models;

namespace UASKI.StaticModels
{
    public static class Ai
    {
        /// <summary>
        /// Максимальное хранимое число истории страниц
        /// </summary>
        private const int MAX_HISTORY_PAGE_COUNT = 50;

        /// <summary>
        /// Буфер для хранения Id скопированных данных
        /// </summary>
        private readonly static List<int> Buffer = new List<int>();

        /// <summary>
        /// В каком состоянии сейчас находится буфер
        /// </summary>
        public static TypeBuffer TypeBuffer = TypeBuffer.Null;

        /// <summary>
        /// Список очереди сообщений на отправку
        /// </summary>
        public static List<Notice> WaitMessageBufer { get; private set; } = new List<Notice>();

        /// <summary>
        /// Активен ли таймер
        /// </summary>
        public static bool IsTimerStart = false;

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
        /// История страниц
        /// </summary>
        private static List<BasePage> PageHistory { get; set; }

        /// <summary>
        /// Последняя страница в истории или null
        /// </summary>
        public static BasePage This { get => PageHistory.DefaultIfEmpty().Last(); }

        /// <summary>
        /// Настройки приложения
        /// </summary>
        public static AppSettings Settings { get; set; }

        /// <summary>
        /// Текстовое поле для вывода
        /// </summary>
        private static TextBox Text;

        /// <summary>
        /// Цвет для обычных уведомлений
        /// </summary>
        private static Color DefultColor;

        /// <summary>
        /// Таймер для отчистки цвета после уведомления
        /// </summary>
        private static Timer Timer;

        /// <summary>
        /// Таймер для очереди сообщений
        /// </summary>
        private static Timer WaitTimer;
        
        /// <summary>
        /// Инициализация программных переменных
        /// </summary>
        /// <param name="box">TextBox чата</param>
        public static void Iniz(TextBox box)
        {
            Text = box;
            PageHistory = new List<BasePage>();
            DefultColor = box.BackColor;
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += NoticeTimer;

            WaitTimer = new Timer();
            WaitTimer.Interval = 200;
            WaitTimer.Tick += WaitTimerTick;
        }

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
                           Text = "Просмотр Исполнителя-Контролёра",
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
                           Text = "Просмотр Праздничных Дней",
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
                       },
                       new ItemMenuLevel2
                       {
                           Text = "Просмотр Двусторонних",
                           Page = Pages.SelectDTasks
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
                           Text = "Добавление Исполнителей-Контролёров",
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
                           Text = "Перечень планов",
                           Page = Pages.PrintPlan
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

        /// <summary>
        /// Таймер для показа уведомлений Ai
        /// </summary>
        /// <returns></returns>
        public static void NoticeTimer(object sender , EventArgs e)
        {
            Text.BackColor = DefultColor;
            IsQuery = false;
            Timer.Stop();
        }

        /// <summary>
        /// Таймер для показа очереди сообщений
        /// </summary>
        public static void WaitTimerTick(object sender , EventArgs e)
        {
            var item = WaitMessageBufer.FirstOrDefault();
            AddMessage(item.Type, item.Message);
            WaitMessageBufer.RemoveAt(0);

            if (WaitMessageBufer.Count == 0)
            {
                IsTimerStart = false;
                WaitTimer.Stop();
            }
        }

        /// <summary>
        /// Добавляет сообщение
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        public static void AddMessage(TypeNotice type , string text)
        {
            string mes = text;
            var notice = new Notice(mes, type);

            string message = Formating(mes);
            RemoveTextExixt(message);
            Text.Text += message;
            Text.BackColor = notice.Color;
            Text.SelectionStart = Text.Text.Length;
            Text.ScrollToCaret();
            Timer.Start();
        }

        /// <summary>
        /// Добавляет сообщение в бочередь на отправку
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        public static void AddWaitMessage(TypeNotice type, string text)
        {
            var notice = new Notice(text, type);
            WaitMessageBufer.Add(notice);

            if(!IsTimerStart)
            {
                IsTimerStart = true;
                WaitTimer.Start();
            }
        }

        /// <summary>
        /// Форматирует строку с множеством данных
        /// </summary>
        /// <param name="text">Заголовок</param>
        /// <param name="lines">Список строк</param>
        /// <returns>Отформатированый текст</returns>
        public static string[] Formating(string text, string[] lines)
        {
            var result = new List<string> { text };
            result.AddRange(lines);
            return result.ToArray();
        }

        /// <summary>
        /// Форматирует строку под чат
        /// </summary>
        /// <param name="text">Строка</param>
        /// <returns>Отформатированную строку</returns>
        private static string Formating(string text)
        {
            var message = "- " + text + $"{Environment.NewLine}";
            return message;
        }

        /// <summary>
        /// Показывает сообщения об ошибке
        /// </summary>
        public static void Error()
        {
            var rand = new Random();
            var mes = AiLibrary.ErrorText[rand.Next(0, AiLibrary.ErrorText.Count - 1)];
            AddMessage(TypeNotice.Error, mes);
        }

        /// <summary>
        /// Показывает сообщение об ошибке в программе
        /// </summary>
        public static void AppError()
        {
            AddMessage(TypeNotice.Error , "Ошибка при выполнении программы (от вас не зависящая)");
        }

        /// <summary>
        /// Показывает просьбу подтвердить операцию
        /// </summary>
        public static void Query()
        {
            AddMessage(TypeNotice.Warning, "Нажмите еще раз для подтверждения");
            IsQuery = true;
        }

        /// <summary>
        /// Показывает статус успешной операции
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public static void Comlite(string text)
        {
           AddMessage(TypeNotice.Comlite, text);
        }

        /// <summary>
        /// Удаляет повторный текст
        /// </summary>
        /// <param name="text">Новый текст</param>
        private static void RemoveTextExixt(string text)
        {
            var current = Text.Text;

            if(current.EndsWith(text)) 
            {
                int lenth = current.Length;
                int removeLenth = text.Length;

                if (lenth - removeLenth - 1 >= 0)
                {
                    Text.Text = current.Remove(lenth - removeLenth - 1, removeLenth);

                    Text.SelectionStart = Text.Text.Length;
                    Text.ScrollToCaret();
                }
                else
                    Text.Clear();
            }
        }

        /// <summary>
        /// Выводит предупреждение
        /// </summary>
        /// <param name="text"></param>
        public static void Warning(string text)
        {
            var mes = Formating(text);
            AddMessage(TypeNotice.Warning , text);
        }

        /// <summary>
        /// Обработка нажатых клавиш для работы с Ai
        /// </summary>
        /// <param name="key">Клавиша</param>
        /// <returns>Обработана ли клавиша</returns>
        public static bool KeyDown(KeyEventArgs key)
        {
            if (!key.Control)
                return false;

            if (This != null && This.AiKeyDown(key))
            {
                return true;
            }

            var k = key.KeyCode;

            switch (k)
            {
                case Keys.Q:
                    Text.Clear();
                    return true;
                case Keys.I:
                    var message = Formating("Горячие клавиши:", AiLibrary.Instruction.ToArray());

                    foreach (var line in message)
                    {
                        Ai.AddMessage(TypeNotice.Default, line);
                    }
                    return true;
                case Keys.L:
                    SelectMenu(Pages.AddTask);
                    Pages.AddTask.Init();
                    return true;
                case Keys.P:
                    SelectMenu(Pages.SelectTask);
                    Pages.SelectTask.Init();
                    return true;
                case Keys.F:
                    SelectMenu(Pages.SelectArhiv);
                    Pages.SelectArhiv.Init();
                    return true;
                case Keys.J:
                    SelectMenu(Pages.SelectOpz);
                    Pages.SelectOpz.Init();
                    return true;
                case Keys.Oemcomma:
                    if(Buffer.Count == 0)
                    {
                        AddMessage(TypeNotice.Error , "Буффер пустой");
                    }
                    else
                    {
                        Pages.EditTask.Init(false, true);
                        Pages.EditTask.Show(Buffer);
                    }
                    return true;
                case Keys.OemPeriod:
                    Buffer.Clear();
                    TypeBuffer = TypeBuffer.Null;
                    AddMessage(TypeNotice.Comlite , "Буффер отчищен");
                    return true;
                case Keys.Z:
                    OpenLastPage();
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Выбирает пункты меню, соответсвтвующие странице. Страницу не открывает
        /// </summary>
        /// <param name="page">Целевая страница</param>
        /// <returns>true - страница найдена. Иначе false</returns>
        public static bool SelectMenu(BasePage page)
        {
            if (page == null)
                return false;

            int lvl1 = -1, lvl2 = -1;

            bool IsReady = false;

            for(int i = 0; i < MenuItems.Count; i++)
            {
                for(int j = 0; j < MenuItems[i].Items.Count(); j++)
                {
                    if (MenuItems[i].Items[j].Page.Index == page.Index)
                    {
                        lvl1 = i;
                        lvl2 = j;
                        IsReady = true;
                        break;
                    }
                }

                if (IsReady)
                    break;
            }

            var form = Form;

            if (lvl1 != -1)
            {
                IsClear = true;
                form.Menu_Step1.SelectedIndex = lvl1;
                form.Menu_Step2.SelectedIndex = lvl2;
                var e = new KeyEventArgs(Keys.Enter);
                form.Menu_Step2_KeyDown(new object(), e);
                IsClear = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Добавляет id в буффер
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="text">Text для отображения в консоли</param>
        public static void AddBuffer(int id , string text)
        {
            

            if(Buffer.Any(c => c == id))
            {
                AddMessage(TypeNotice.Error, "Данная запись уже есть в буфере");
            }
            else
            {
                Buffer.Add(id);
                AddMessage(TypeNotice.Default, text);
                AddMessage(TypeNotice.Default, $"Записей в буфере: {Buffer.Count}");
            }
            
        }

        /// <summary>
        /// Получает буффер
        /// </summary>
        public static List<int> GetBuffer()
        {
            return Buffer;
        }

        /// <summary>
        /// Удаляет запись из буфера
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <param name="text">Text для отображения в консоли</param>
        public static void DeleteBuffer(int id , string text)
        {
            var item = Buffer.IndexOf(id);

            if(item == -1)
            {
                AddMessage(TypeNotice.Error, "Запись не найдена в буфере");
            }
            else
            {
                Buffer.RemoveAt(item);
                AddMessage(TypeNotice.Default, text);
                AddMessage(TypeNotice.Default, $"Записей в буфере: {Buffer.Count}");
            }
        }

        /// <summary>
        /// Добавляет страницу в историю
        /// </summary>
        /// <param name="page">Страница</param>
        public static void AddHostoryPage(BasePage page)
        {
            if(page.Type != TypePage.Edit)
            {
                PageHistory.Add(page);

                if (PageHistory.Count >= MAX_HISTORY_PAGE_COUNT)
                {
                    PageHistory.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Открывает предыдущую страницу из истории
        /// </summary>
        /// <returns></returns>
        public static bool OpenLastPage()
        {
            if (PageHistory.Count < 2)
                return false;

            var page = PageHistory[PageHistory.Count - 2];
            PageHistory = PageHistory.Where(c => c.Index != page.Index).ToList();
            PageHistory.RemoveAt(PageHistory.Count - 1);

            if (page.DataGridView == null)
            {
                page.Init();
            }
            else
            {
                page.ExitToDataGrid();
            }

            SelectMenu(page);
            return true;
        }
    }
}
