using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Models.Elements;
using UASKI.StaticModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для обработки ошибок
    /// </summary>
    public static class ErrorHelper
    {
        
        /// <summary>
        /// Изменяет статус на ошибку
        /// </summary>
        public static void StatusError()
        {
            var form = SystemData.Form;
            Close();

            form.LabelStatus.Text = "Ошибка";
            form.LabelStatus.ForeColor = Color.Red;
            form.LabelStatus.Visible = true;
            form.TimerStatus.Start();
        }

        /// <summary>
        /// Изменяет статус на успех
        /// </summary>
        public static void StatusComlite()
        {
            var form = SystemData.Form;
            Close();

            form.LabelStatus.Text = "Успешно";
            form.LabelStatus.ForeColor = Color.Green;
            form.LabelStatus.Visible = true;
            form.TimerStatus.Start();
        }

        /// <summary>
        /// Изменяет статус на подтверждение
        /// </summary>
        public static void StatusQuery()
        {
            var form = SystemData.Form;
            Close();

            form.LabelStatus.Text = "Нажмите Enter еще раз для подтверждения";
            form.LabelStatus.ForeColor = Color.Blue;
            form.LabelStatus.Visible = true;
            SystemData.IsQuery = true;
            form.TimerStatus.Start();
        }

        /// <summary>
        /// Закрывает и скрывает статус
        /// </summary>
        private static void Close()
        {
            var form = SystemData.Form;

            form.TimerStatus.Stop();
            form.LabelStatus.Visible = false;
            form.timerTick = 0;
        }
    }
}
