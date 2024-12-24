using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Enums;
using UASKI.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace UASKI.StaticModels
{
    public static class Ai
    {
        private readonly static List<Notice> Orders = new List<Notice>();
        private static TextBox Text;
        private static bool Sleep = true;
        private static Color DefultColor;

        /// <summary>
        /// Инициализация программных переменных
        /// </summary>
        /// <param name="box">TextBox чата</param>
        public static void Iniz(TextBox box)
        {
            Text = box;
            DefultColor = box.BackColor;
        }

        /// <summary>
        /// Таймер Ai
        /// </summary>
        public static bool Timer()
        {
            if (!Sleep)
            {
                Text.BackColor = DefultColor;
                Sleep = true;
            }
                

            if (Orders.Count == 0)
                return false;
            else
                Say();

            return true;
        }

        /// <summary>
        /// Добавляет сообщение в очередь
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        public static void AddMessage(TypeNotice type , string text)
        {
            string mes = "- " + text + $"{Environment.NewLine}";

            var notice = new Notice(mes, type);

            Orders.Add(notice);
        }

        /// <summary>
        /// Добавляет сообщение в очередь
        /// </summary>
        /// <param name="type">Тип сообщения</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="lines">Текст сообщения</param>
        public static void AddMessage(TypeNotice type, string text, string[] lines)
        {
            string message = string.Empty;
            message += "- " + text + $"{Environment.NewLine}";

            foreach (var line in lines)
            {
                message += line + $"{Environment.NewLine}";
            }

            var notice = new Notice(message, type);
            
            Orders.Add(notice);
        }

        /// <summary>
        /// Показывает первое сообщение из очереди
        /// </summary>
        private static void Say()
        {
            if(Orders.Count > 0)
            {
                var mes = Orders[0];
                Text.Text += mes.Message;
                Orders.Remove(mes);
                Text.BackColor = mes.Color;
                Sleep = false;
                Text.SelectionStart = Text.Text.Length;
                Text.ScrollToCaret();
            }
        }

        /// <summary>
        /// Показывает сообщение
        /// </summary>
        private static void Say(Notice mes)
        {
            Text.Text += mes.Message;
            Text.BackColor = mes.Color;
            Sleep = false;
            Text.SelectionStart = Text.Text.Length;
            Text.ScrollToCaret();
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
        /// Показывает кастомное сообщение об ошибке
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public static void Error(string text)
        {
            var notice = new Notice(text, TypeNotice.Error);
            Say(notice);
        }

        /// <summary>
        /// Показывает просьбу подтвердить операцию
        /// </summary>
        public static void Query()
        {
            var notice = new Notice("Нажмите еще раз для подтверждения" , TypeNotice.Warning);
            Say(notice);
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
    }
}
