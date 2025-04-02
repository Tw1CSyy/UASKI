using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UASKI.Enums;
using UASKI.Helpers;
using UASKI.Models;

namespace UASKI.StaticModels
{
    public static class Ai
    {
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

        private static TextBox Text;
        private static Color DefultColor;
        private static Timer Timer;
        private static Timer WaitTimer;
        
        /// <summary>
        /// Инициализация программных переменных
        /// </summary>
        /// <param name="box">TextBox чата</param>
        public static void Iniz(TextBox box)
        {
            Text = box;
            DefultColor = box.BackColor;
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += NoticeTimer;

            WaitTimer = new Timer();
            WaitTimer.Interval = 200;
            WaitTimer.Tick += WaitTimerTick;
        }

        /// <summary>
        /// Таймер для показа уведомлений Ai
        /// </summary>
        /// <returns></returns>
        public static void NoticeTimer(object sender , EventArgs e)
        {
            Text.BackColor = DefultColor;
            SystemData.IsQuery = false;
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
            SystemData.IsQuery = true;
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

            if (SystemData.This != null && SystemData.This.AiKeyDown(key))
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
                    SelectMenu(SystemData.Pages.AddTask);
                    return true;
                case Keys.P:
                    SelectMenu(SystemData.Pages.SelectTask);
                    return true;
                case Keys.F:
                    SelectMenu(SystemData.Pages.SelectArhiv);
                    return true;
                case Keys.J:
                    SelectMenu(SystemData.Pages.SelectOpz);
                    return true;
                case Keys.Oemcomma:
                    if(Buffer.Count == 0)
                    {
                        AddMessage(TypeNotice.Error , "Буффер пустой");
                    }
                    else
                    {
                        SystemData.Pages.EditTask.Init(false, true);
                        SystemData.Pages.EditTask.Show(Buffer);
                    }
                    return true;
                case Keys.OemPeriod:
                    Buffer.Clear();
                    TypeBuffer = TypeBuffer.Null;
                    AddMessage(TypeNotice.Comlite , "Буффер отчищен");
                    return true;
                case Keys.S:
                    if (ApplicationHelper.Dump())
                        Ai.AddMessage(Enums.TypeNotice.Comlite, "Создана копия данных");
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Выбирает пункты меню, соответсвтвующие странице
        /// </summary>
        /// <param name="page">Целевая страница</param>
        /// <returns>true - страница найдена</returns>
        public static bool SelectMenu(BasePage page)
        {
            if (page == null)
                return false;

            int lvl1 = -1, lvl2 = -1;

            bool IsReady = false;

            for(int i = 0; i < SystemData.MenuItems.Count; i++)
            {
                for(int j = 0; j < SystemData.MenuItems[i].Items.Count(); j++)
                {
                    if (SystemData.MenuItems[i].Items[j].Page.Index == page.Index)
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

            var form = SystemData.Form;

            if (lvl1 != -1)
            {
                form.Menu_Step1.SelectedIndex = lvl1;
                form.Menu_Step2.SelectedIndex = lvl2;
                var e = new KeyEventArgs(Keys.Enter);
                form.Menu_Step2_KeyDown(new object(), e);
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

    }
}
