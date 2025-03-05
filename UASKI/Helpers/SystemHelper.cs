using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UASKI.Models;
using System.Linq;
using UASKI.Core.Models;
using UASKI.Core.SystemModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Общий системный хелпер
    /// </summary>
    public static class SystemHelper
    {
        
        /// <summary>
        /// Возращает русский символ эквивалетный нажатой клавише или '+'
        /// </summary>
        /// <param name="Key">Нажатая клавиша</param>
        private static char GetCharKeyDown(Keys Key)
        {
            switch (Key)
            {
                case Keys.D0:
                    return '0';
                case Keys.D1:
                    return '1';
                case Keys.D2:
                    return '2';
                case Keys.D3:
                    return '3';
                case Keys.D4:
                    return '4';
                case Keys.D5:
                    return '5';
                case Keys.D6:
                    return '6';
                case Keys.D7:
                    return '7';
                case Keys.D8:
                    return '8';
                case Keys.D9:
                    return '9';
                case Keys.NumPad0:
                    return '0';
                case Keys.NumPad1:
                    return '1';
                case Keys.NumPad2:
                    return '2';
                case Keys.NumPad3:
                    return '3';
                case Keys.NumPad4:
                    return '4';
                case Keys.NumPad5:
                    return '5';
                case Keys.NumPad6:
                    return '6';
                case Keys.NumPad7:
                    return '7';
                case Keys.NumPad8:
                    return '8';
                case Keys.NumPad9:
                    return '9';
                case Keys.A:
                    return 'ф';
                case Keys.B:
                    return 'и';
                case Keys.C:
                    return 'с';
                case Keys.D:
                    return 'в';
                case Keys.E:
                    return 'у';
                case Keys.F:
                    return 'а';
                case Keys.G:
                    return 'п';
                case Keys.H:
                    return 'р';
                case Keys.I:
                    return 'ш';
                case Keys.J:
                    return 'о';
                case Keys.K:
                    return 'л';
                case Keys.L:
                    return 'д';
                case Keys.M:
                    return 'ь';
                case Keys.N:
                    return 'т';
                case Keys.O:
                    return 'щ';
                case Keys.P:
                    return 'з';
                case Keys.Q:
                    return 'й';
                case Keys.R:
                    return 'к';
                case Keys.S:
                    return 'ы';
                case Keys.T:
                    return 'е';
                case Keys.U:
                    return 'г';
                case Keys.V:
                    return 'м';
                case Keys.W:
                    return 'ц';
                case Keys.X:
                    return 'ч';
                case Keys.Y:
                    return 'н';
                case Keys.Z:
                    return 'я';
                case Keys.OemSemicolon:
                    return 'ж';
                case Keys.Oemcomma:
                    return 'б';
                case Keys.OemPeriod:
                    return 'ю';
                case Keys.OemOpenBrackets:
                    return 'х';
                case Keys.OemCloseBrackets:
                    return 'ъ';
                case Keys.OemQuotes:
                    return 'э';
                case Keys.Back:
                    return '-';
            }

            return '+';
        }

        /// <summary>
        /// Добавляет в ТекстБокс нажатый символ
        /// </summary>
        /// <param name="textBox">текстБокс</param>
        /// <param name="key">Нажатая клавиша</param>
        public static bool CharInTextBox(TextBox textBox , Keys key)
        {
            var sim = GetCharKeyDown(key);

            if(sim == '+')
            {
                return false;
            }

            if(sim == '-' && textBox.Text.Length != 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
                return true;
            }
            else if(sim == '-' && textBox.Text.Length == 0)
            {
                return true;
            }

            if (textBox.Text.Length == 0)
            {
                textBox.Text += sim.ToString().ToUpper();
            }
            else
            {
                textBox.Text += sim;
            }

            return true;
        }

        /// <summary>
        /// Возращает число эквивалетное нажатой клавише или -1
        /// </summary>
        /// <param name="Key">Нажатая клавиша</param>
        public static int GetIntKeyDown(Keys keys)
        {
            var h = GetCharKeyDown(keys).ToString();

            if(int.TryParse(h , out int i))
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Формирует документ для печати
        /// </summary>
        /// <param name="model">Модель печати</param>
        /// <returns></returns>
        public static void PrintDocument(PrintModel model , bool PrintHeader)
        {
            float headerY = 5f;

            // Вытаскиваем аргументы из модели
            var e = model.Argument;
            var font = model.Font;
            var d = model.DataGridView;

            // Инициализируем переменные для работы
            float linesPerPage = e.MarginBounds.Height / font.GetHeight(e.Graphics) + 2;
            int count = 0;
            int with = (int)Math.Ceiling((double)(e.PageBounds.Width / d.Columns.Count));

            // Инициализируем переменные для заголовков
            var headerFont = new Font("Arial", 16, FontStyle.Bold);
            float yPosition = 5f;

            if(PrintHeader)
            {
                // Выводим заголовки
                foreach (var header in model.Headers)
                {
                    SizeF headerSize = e.Graphics.MeasureString(header, headerFont);
                    float headerX = (e.PageBounds.Width - headerSize.Width) / 2;

                    e.Graphics.DrawString(header, headerFont, Brushes.Black, headerX, headerY);
                    headerY += headerSize.Height + 10;
                }

                // Расчитываем следующую строку
                yPosition = headerY + headerFont.Size;
            }
            else
            {
                linesPerPage += 5;
            }
            
            // Печать заголовков таблицы
            foreach (DataGridViewColumn column in d.Columns)
            {
                e.Graphics.DrawString(column.HeaderText, font, Brushes.Black, column.Index * with + 15, yPosition);
            }

            yPosition += font.GetHeight(e.Graphics);
            yPosition += font.GetHeight(e.Graphics);

            // Печать строк
            while (count < linesPerPage && count < d.Rows.Count)
            {
                DataGridViewRow row = d.Rows[count];

                if (row.Tag != null)
                {
                    count++;
                    linesPerPage++;
                    continue;
                }
                    
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    e.Graphics.DrawString(row.Cells[i].Value.ToString(), font, Brushes.Black, i * with + 15, yPosition);
                }

                yPosition += font.GetHeight(e.Graphics);
                d.Rows[count].Tag = false;
                count++;
                
            }

            e.HasMorePages = d.Rows[d.Rows.Count - 1].Tag == null;
        }

    }
}
