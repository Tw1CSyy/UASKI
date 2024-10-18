using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Обработка ТекстБокса
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text , TextBoxElement error)
        {
            if(error.ErrorLabel.Visible != true)
            {
                error.ErrorLabel.ForeColor = Color.Red;
                error.ErrorLabel.Visible = true;
                error.ErrorLabel.Text = text;
                StatusError();
            }
        }

        /// <summary>
        /// Обработка Нумерика
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text, NumericElement error)
        {
            if (error.ErrorLabel.Visible != true)
            {
                error.ErrorLabel.ForeColor = Color.Red;
                error.ErrorLabel.Visible = true;
                error.ErrorLabel.Text = text;
                StatusError();
            }
        }

        /// <summary>
        /// Обработка Даты
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text, DateTimeElement error)
        {
            if (error.ErrorLabel.Visible != true)
            {
                error.ErrorLabel.ForeColor = Color.Red;
                error.ErrorLabel.Visible = true;
                error.ErrorLabel.Text = text;
                StatusError();
            }
        }

        /// <summary>
        /// Обработка Даты
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text, MonthElement error)
        {
            if (error.ErrorLabel.Visible != true)
            {
                error.ErrorLabel.ForeColor = Color.Red;
                error.ErrorLabel.Visible = true;
                error.ErrorLabel.Text = text;
                StatusError();
            }
        }

        /// <summary>
        /// Изменяет статус на ошибку
        /// </summary>
        public static void StatusError()
        {
            var form = SystemData.Form;

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

            form.LabelStatus.Text = "Успешно";
            form.LabelStatus.ForeColor = Color.Green;
            form.LabelStatus.Visible = true;
            form.TimerStatus.Start();
        }
    }
}
