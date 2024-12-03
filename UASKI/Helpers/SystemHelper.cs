using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UASKI.Models;
using UASKI.StaticModels;
using System.Linq;
using UASKI.Pages;
using UASKI.Data.Entyties;
using UASKI.Services;
using UASKI.ViewModels;
using UASKI.Data.Entityes;

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

            ResizeDataGridView(d);
        }

        /// <summary>
        /// Включает и меняет фон кнопки для отображения выделения
        /// </summary>
        /// <param name="btn">Button для выделения</param>
        /// <param name="selected">Вкл/Выкл</param>
        public static void SelectButton(Button btn , bool selected = true)
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
        /// Выбирает DataGridView
        /// </summary>
        /// <param name="d">DataGridView для выделения</param>
        /// <param name="selected">Вкл/Выкл</param>
        public static bool SelectDataGridView(DataGridView d , bool selected = true)
        {
            if(selected)
            {
                if(d.Rows.Count > 0)
                {
                    d.Rows[0].Selected = true;
                    d.Focus();
                    return true;
                }

                return false;
            }
            else
            {
                d.ClearSelection();
                return true;
            }
        }

        /// <summary>
        /// Выбирает текст бокс и переводит курсор
        /// </summary>
        /// <param name="t">TextBox для выделения</param>
        public static void SelectTextBox(TextBox t)
        {
            t.Focus();
            t.SelectionStart = t.Text.Length;
        }

        /// <summary>
        /// Выбирает и веделяет CheckBox
        /// </summary>
        /// <param name="c">CheckBox для выделения</param>
        /// <param name="selected"></param>
        public static void SelectCheckBox(CheckBox c , bool selected = true)
        {
            if (selected)
            {
                c.BackColor = Color.LightBlue;
                c.Focus();
            }
            else
            {
                c.BackColor = Color.FromArgb(224, 224, 224);
            }
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

            try
            {
                if (text.Length == 2)
                {
                    var day = Convert.ToInt32(text);
                    date = new DateTime(dateFrom.Year, dateFrom.Month, day);
                    return date;
                }

                if (text.Length == 4)
                {
                    string value = string.Empty;
                    value += text[2];
                    value += text[3];

                    var month = Convert.ToInt32(value);
                    date = new DateTime(dateFrom.Year, month, dateFrom.Day);
                    return date;
                }

                if (text.Length == 6)
                {
                    string value = string.Empty;
                    value += "20";
                    value += text[4];
                    value += text[5];

                    var year = Convert.ToInt32(value);
                    date = new DateTime(year, dateFrom.Month, dateFrom.Day);
                    return date;
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
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
            var count = 0;

            for (int i = 0; i < d.Columns.Count; i++)
            {
                if (d.Columns[i].Visible)
                    count++;
            }

            var with = (int)Math.Floor((double)d.Width / (double)count);

            for(int i = 0; i < d.Columns.Count; i++)
            {
                d.Columns[i].Width = with;
            }
        }

        /// <summary>
        /// Сортирует DataGridView по столбцу
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="key">Нажатая клавиша</param>
        public static void DataGridViewSort(DataGridView d, Keys key)
        {
            var index = GetIntKeyDown(key);

            if (d.Columns.Count >= index && index != -1)
            {
                if (d.Columns[index - 1].HeaderCell.SortGlyphDirection == SortOrder.Descending || d.Columns[index - 1].HeaderCell.SortGlyphDirection == SortOrder.None)
                    d.Sort(d.Columns[index - 1], System.ComponentModel.ListSortDirection.Ascending);
                else
                    d.Sort(d.Columns[index - 1], System.ComponentModel.ListSortDirection.Descending);
            }
        }


        /// <summary>
        /// Формирует документ для печати
        /// </summary>
        /// <param name="model">Модель для печати</param>
        public static void PrintDocument(params PrintModel[] models)
        {
            float headerY = 0f;

            foreach (var model in models)
            {
                // Вытаскиваем аргументы из модели
                var e = model.Argument;
                var font = model.Font;
                var d = model.DataGridView;

                // Инициализируем переменные для работы
                float linesPerPage = e.MarginBounds.Height / font.GetHeight(e.Graphics);
                int count = 0;
                int with = (int)Math.Ceiling((double)(e.PageBounds.Width / d.Columns.Count));

                // // Инициализируем переменные для заголовков
                var headerFont = new Font("Arial", 16, FontStyle.Bold);
                
                if(headerY == 0f)
                    headerY = e.MarginBounds.Top;

                // Выводим заголовки
                foreach (var header in model.Headers)
                {
                    SizeF headerSize = e.Graphics.MeasureString(header, headerFont);
                    float headerX = (e.PageBounds.Width - headerSize.Width) / 2;

                    e.Graphics.DrawString(header, headerFont, Brushes.Black, headerX, headerY);
                    headerY += headerSize.Height + 10;
                }

                // Расчитываем следующую строку
                float yPosition = headerY + headerFont.Size;

                // Печать заголовков таблиы
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
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        e.Graphics.DrawString(row.Cells[i].Value.ToString(), font, Brushes.Black, i * with + 15, yPosition);
                    }
                    yPosition += font.GetHeight(e.Graphics);
                    count++;
                }

                headerY = yPosition + 30;
            }

        }

        /// <summary>
        /// Расчитывает коофициент качества по списку задач
        /// </summary>
        /// <param name="tasks">Список задач</param>
        /// <param name="listPret">Список претензий и рецензий</param>
        /// <returns></returns>
        private static double GetCof(List<ArhivEntity> tasks , List<PretEntity> listPret)
        {
            if (tasks.Count == 0)
            {
                return 0;
            }

            int countTask = 0, countPret = 0, countRez = 0, countOpz = 0, countCof = 0;

            foreach (var task in tasks)
            {
                countTask += Convert.ToInt32(task.Code[0].ToString()) * task.Otm;

                var prets = listPret.Where(c => c.CodeTask.Equals(task.Code) && c.Type == 1).ToList();
                var rezs = listPret.Where(c => c.CodeTask.Equals(task.Code) && c.Type == 2).ToList();

                countPret += prets.Sum(c => Convert.ToInt32(c.Code[0].ToString()) * c.Otm);
                countRez += rezs.Sum(c => Convert.ToInt32(c.Code[0].ToString()) * c.Otm);

                if(task.Date < task.DateClose)
                {
                    countOpz += Convert.ToInt32(task.Code[0].ToString()) * (task.DateClose - task.Date).Days;
                }

                countCof += Convert.ToInt32(task.Code[0].ToString()) + prets.Sum(c => Convert.ToInt32(c.Code[0].ToString())) + rezs.Sum(c => Convert.ToInt32(c.Code[0].ToString()));
            }

            double result = (countTask + countPret + countRez - 0.2 * countOpz) / (5 * countCof);
            result = Math.Round(result, 2);

            return result;
        }


        /// <summary>
        /// Расчитывает данные для коофициента качества для исполнителя
        /// </summary>
        /// <param name="isp">Исполнитель</param>
        /// <param name="dateFrom">Дата от</param>
        /// <param name="dateTo">Дата до</param>
        /// <returns></returns>
        public static PrintPocViewModel GetKofModel(IspEntity isp , DateTime dateFrom , DateTime dateTo)
        {
            var item = new PrintPocViewModel();
            item.Isp = IspService.GetIniz(isp);

            var tasks = ArhivService.GetList().Where(c => c.IdIsp == isp.Code).ToList();
            var tasksPediod = tasks.Where(c => c.DateClose.Date >= dateFrom && c.DateClose.Date <= dateTo).ToList();
            var tasksMonth = tasks.Where(c => c.DateClose.Month == dateFrom.Month && c.DateClose.Year == dateFrom.Year).ToList();

            item.CountPeriod = tasksPediod.Count();
            item.CountMonth = tasksMonth.Count();

            item.CountOpzPeriod = tasksPediod.Count(c => c.Date < c.DateClose);
            item.CountOpzMonth = tasksMonth.Count(c => c.Date < c.DateClose);

            item.CountDayPeriod = tasksPediod.Where(c => c.Date < c.DateClose).Sum(c => (c.DateClose - c.Date).Days);
            item.CountDayMonth = tasksMonth.Where(c => c.Date < c.DateClose).Sum(c => (c.DateClose - c.Date).Days);

            var pretList = PretService.GetList();

            item.KofPeriod = SystemHelper.GetCof(tasksPediod, pretList);
            item.KofMonth = SystemHelper.GetCof(tasksMonth, pretList);

            return item;
        }
    }
}
