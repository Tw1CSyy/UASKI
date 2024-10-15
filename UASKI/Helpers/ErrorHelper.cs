using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASKI.Models.Elements;

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
            error.TextBox.BackColor = error.ErrorColor;
            error.ErrorLabel.ForeColor = Color.Red;
            error.ErrorLabel.Visible = true;
            error.ErrorLabel.Text = text;

            if(error.IsSelected)
            {
                error.TextBox.Focus();
            }
        }

        /// <summary>
        /// Обработка Нумерика
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text, NumericElement error)
        {
            error.ErrorLabel.ForeColor = Color.Red;
            error.ErrorLabel.Visible = true;
            error.ErrorLabel.Text = text;

            if (error.IsSelected)
            {
                error.NumericUpDown.Focus();
            }
        }

        /// <summary>
        /// Обработка Даты
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        /// <param name="error">Модель ошибки</param>
        public static void Error(string text, DateTimeElement error)
        {
            error.ErrorLabel.ForeColor = Color.Red;
            error.ErrorLabel.Visible = true;
            error.ErrorLabel.Text = text;

            if (error.IsSelected)
            {
                error.DateTimePicker.Focus();
            }
        }
    }
}
