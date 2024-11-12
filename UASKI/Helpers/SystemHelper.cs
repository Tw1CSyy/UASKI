using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UASKI.Models;
using UASKI.StaticModels;
using System.Linq;
using UASKI.Pages;

namespace UASKI.Helpers
{
    /// <summary>
    /// Общий системный хелпер
    /// </summary>
    public static class SystemHelper
    {
       
        /// <summary>
        /// Заполняет DataGricView данными
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="values">Колекция строк с данными</param>
        /// <param name="columns">Набор названий колонок</param>
        public static void PullListInDataGridView(DataGridView d , List<DataGridRowModel> values , DataGridRowModel columns)
        {
            d.DataSource = null;
            
            var table = new DataTable();

            foreach (var item in columns.Values)
            {
                table.Columns.Add(item);
            }

            foreach (var line in values)
            {
                var row = table.NewRow();
                
                for(int i = 0; i < line.Values.Length; i++)
                {
                    row[i] = line.Values[i];
                }

                table.Rows.Add(row);
            }

            d.DataSource = table;

            for(int i = 0; i < columns.Values.Length; i++)
            {
                d.Columns[i].Width = (int)Math.Floor((double)(d.Width - 40) / (double)columns.Values.Length);
            }
        }

        /// <summary>
        /// Меняет фон кнопки для отображения выделения
        /// </summary>
        /// <param name="selected">Вкл/Выкл</param>
        /// <param name="btn">Кнопка</param>
        public static void SelectButton(bool selected , Button btn)
        {
            if(selected)
            {
                btn.BackColor = Color.LightBlue;
                btn.Focus();
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Включает и выбирает DataGridView
        /// </summary>
        /// <param name="selected">Вкл/Выкл</param>
        /// <param name="d">ДатаГридВью</param>
        public static void SelectDataGridView(bool selected , DataGridView d)
        {
            if(selected)
            {
                d.Focus();
                
                if(d.Rows.Count > 0)
                {
                    d.Rows[0].Selected = true;
                }
            }
            else
            {
                
                d.ClearSelection();
            }
        }

        /// <summary>
        /// Выбирает текст бокс и переводит курсор
        /// </summary>
        /// <param name="t"></param>
        public static void SelectTextBox(TextBox t)
        {
            t.Focus();
            t.SelectionStart = t.Text.Length;
        }

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
        /// Преобразует строку в дату
        /// </summary>
        /// <param name="text">Строка</param>
        /// <param name="dateFrom">Изначальная дата</param>
        /// <returns>DateTime</returns>
        public static DateTime GetDate(string text , DateTime dateFrom)
        {
            DateTime date;

            if (text.Length == 2)
            {
                var day = Convert.ToInt32(text);
                date = new DateTime(dateFrom.Year , dateFrom.Month , day);
                return date;
            }

            if (text.Length == 4)
            {
                string value = string.Empty;
                value += text[2];
                value += text[3];

                var month = Convert.ToInt32(value);
                date = new DateTime(dateFrom.Year , month , dateFrom.Day);
                return date;
            }

            if (text.Length == 8)
            {
                string value = string.Empty;
                value += text[4];
                value += text[5];
                value += text[6];
                value += text[7];

                var year = Convert.ToInt32(value);
                date = new DateTime(year, dateFrom.Month , dateFrom.Day);
                return date;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Возращает число эквивалетное нажатой клавише или -1
        /// </summary>
        /// <param name="Key">Нажатая клавиша</param>
        public static int GetIntKeyDown(Keys keys)
        {
            switch (keys)
            {
                case Keys.D0:
                    return 0;
                case Keys.D1:
                    return 1;
                case Keys.D2:
                    return 2;
                case Keys.D3:
                    return 3;
                case Keys.D4:
                    return 4;
                case Keys.D5:
                    return 5;
                case Keys.D6:
                    return 6;
                case Keys.D7:
                    return 7;
                case Keys.D8:
                    return 8;
                case Keys.D9:
                    return 9;
                case Keys.NumPad0:
                    return 0;
                case Keys.NumPad1:
                    return 1;
                case Keys.NumPad2:
                    return 2;
                case Keys.NumPad3:
                    return 3;
                case Keys.NumPad4:
                    return 4;
                case Keys.NumPad5:
                    return 5;
                case Keys.NumPad6:
                    return 6;
                case Keys.NumPad7:
                    return 7;
                case Keys.NumPad8:
                    return 8;
                case Keys.NumPad9:
                    return 9;
            }

            return -1;
        }

        /// <summary>
        /// Устанавливает размер колонок в датагрид по ширине
        /// </summary>
        /// <param name="d">DataGirdView</param>
        public static void ResizeDataGridView(DataGridView d)
        {
            var with = (int)Math.Floor((double)d.Width / (double)d.Columns.Count);

            for(int i = 0; i < d.Columns.Count; i++)
            {
                d.Columns[i].Width = with;
            }
        }
    }
}
