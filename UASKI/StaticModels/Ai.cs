using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void AddMessage(TypeNotice type , string text)
        {
            string mes = "- " + text + $"{Environment.NewLine}";

            var notice = new Notice(mes, type);

            Orders.Add(notice);
        }

        public static void AddMessage(TypeNotice type, params string[] text)
        {
            string message = string.Empty;

            foreach (var line in text)
            {
                message += "- " + line + $"{Environment.NewLine}";
            }

            var notice = new Notice(message, type);
            
            Orders.Add(notice);
        }

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
    }
}
