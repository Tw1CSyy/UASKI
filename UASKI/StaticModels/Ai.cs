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
        private readonly static List<Notice> Orders = new List<Notice>();
        private static TextBox Text;
        private static bool Sleep = true;
        private static Color DefultColor;
        private static Timer Timer;

        /// <summary>
        /// Инициализация программных переменных
        /// </summary>
        /// <param name="box">TextBox чата</param>
        public static void Iniz(TextBox box)
        {
            Text = box;
            DefultColor = box.BackColor;
            Timer = new Timer();
            Timer.Interval = 500;
            Timer.Tick += NoticeTimer;
        }

        /// <summary>
        /// Таймер для показа уведомлений Ai
        /// </summary>
        /// <returns></returns>
        public static void NoticeTimer(object sender , EventArgs e)
        {
            if (!Sleep)
            {
                Text.BackColor = DefultColor;
                SystemData.IsQuery = false;
                Sleep = true;
            }

            if (Orders.Count > 0)
            {
                var mes = Orders.First();
                Say(mes);
                Orders.Remove(mes);
            }
            else
                Timer.Stop();
        }

        /// <summary>
        /// Добавляет сообщение в очередь
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        public static void AddMessage(TypeNotice type , string text)
        {
            string mes = text;

            var notice = new Notice(mes, type);

            Orders.Add(notice);
            Timer.Start();
        }

        /// <summary>
        /// Добавляет сообщение в очередь
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="lines">Текст сообщения</param>
        public static void AddMessage(TypeNotice type, string text, string[] lines)
        {
            var message = Formating(text, lines);
            AddMessage(type, message);
        }

        /// <summary>
        /// Форматирует строку с множеством данных
        /// </summary>
        /// <param name="text">Заголовок</param>
        /// <param name="lines">Список строк</param>
        /// <returns>Отформатированый текст</returns>
        public static string Formating(string text, string[] lines)
        {
            string message = string.Empty;
            message += text + Environment.NewLine;

            foreach (var line in lines)
            {
                message += line + Environment.NewLine;
            }

            return message;
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
        /// Показывает сообщение
        /// </summary>
        private static void Say(Notice mes)
        {
            string message = Formating(mes.Message);
            RemoveTextExixt(message);
            Text.Text += message;
            Text.BackColor = mes.Color;
            Sleep = false;
            Text.SelectionStart = Text.Text.Length;
            Text.ScrollToCaret();
            Timer.Start();
        }

        /// <summary>
        /// Показывает сообщения об ошибке
        /// </summary>
        public static void Error()
        {
            var rand = new Random();
            var mes = AiLibrary.ErrorText[rand.Next(0, AiLibrary.ErrorText.Count - 1)];

            var notice = new Notice(mes, TypeNotice.Error);
            Say(notice);
        }

        /// <summary>
        /// Показывает сообщение об ошибке в программе
        /// </summary>
        public static void AppError()
        {
            var notice = new Notice("Ошибка при выполнении программы (от вас не зависящая)", TypeNotice.Error);
            Say(notice);
        }

        /// <summary>
        /// Показывает просьбу подтвердить операцию
        /// </summary>
        public static void Query()
        {
            var notice = new Notice("Нажмите еще раз для подтверждения" , TypeNotice.Warning);
            Say(notice);
            SystemData.IsQuery = true;
        }

        /// <summary>
        /// Показывает статус успешной операции
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public static void Comlite(string text)
        {
            var notice = new Notice(text, TypeNotice.Comlite);
            Say(notice);
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

                Text.Text = current.Remove(lenth - removeLenth - 1, removeLenth);

                Text.SelectionStart = Text.Text.Length;
                Text.ScrollToCaret();
            }
        }

        /// <summary>
        /// Выводит предупреждение
        /// </summary>
        /// <param name="text"></param>
        public static void Warning(string text)
        {
            var mes = Formating(text);
            var notice = new Notice(text, TypeNotice.Warning);
            Say(notice);
        }

        /// <summary>
        /// Обработка нажатых клавиш для работы с Ai
        /// </summary>
        /// <param name="key">Клавиша</param>
        /// <returns>Обработана ли клавиша</returns>
        public static bool KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.Q:
                    Text.Clear();
                    return true;
                case Keys.I:
                    var message = Formating("Горячие клавиши:", AiLibrary.Instruction.ToArray());
                    Say(new Notice(message , TypeNotice.Default));
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
            }

            return false;
        }

        /// <summary>
        /// Выбирает пункты меню, соответсвтвующие странице
        /// </summary>
        /// <param name="page">Целевая страница</param>
        private static void SelectMenu(BasePage page)
        {
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
                form.Menu_Step1.SelectedIndex = lvl1;

            if (lvl1 != -1)
            {
                form.Menu_Step2.SelectedIndex = lvl2;
                var e = new KeyEventArgs(Keys.Enter);
                form.Menu_Step2_KeyDown(new object() , e);
            }
               

            
        }
    }
}
