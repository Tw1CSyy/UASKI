using System;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Services;
using UASKI.StaticModels;
using UASKI.Models.Elements;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для отбработки нажатий клавиш на форме
    /// </summary>
    public static class KeyDownHelper
    {
        public static readonly Gl_Form form = SystemData.Form;
        public static readonly Keys ActionKey = Keys.F2;

        #region Главная
        /// <summary>
        /// При нажатии на меню 1 уровня
        /// </summary>
        /// <param name="e">Объект события</param>
        public static void Menu_Step1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.Menu_Step2.SelectedIndex = 0;
                form.Menu_Step1.Enabled = false;
            }
        }

        /// <summary>
        /// При нажатии на меню 2 уровня
        /// </summary>
        /// <param name="e">Объект события</param>
        public static void Menu_Step2_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step1.Enabled = true;
                form.Menu_Step1.Focus();
                form.Menu_Step2.ClearSelected();
                form.Menu_Step2.Enabled = false;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                NavigationHelper.Start();
            }
        }

        #endregion

        #region Доп.Форма Исполнителей

        /// <summary>
        /// При нажатии на таблицу при выборе исполнителей
        /// </summary>
        /// <param name="e">Обхект события</param>
        /// <param name="form">Форма</param>
        /// <param name="t1">Фамилия</param>
        /// <param name="t2">Подразделение</param>
        /// <param name="t3">Табельный номер</param>
        /// <param name="tt">Следующий фокус</param>
        /// <param name="d">DataGridView из формы</param>
        public static void dataGridView_KeyDown(KeyEventArgs e, IspForm form, TextBox t1, TextBox t2, TextBox t3, DataGridView d)
        {
            if (e.KeyCode == Keys.Escape)
            {
                form.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                var row = d.SelectedRows;

                if (row != null && row.Count > 0)
                {
                    t1.Text = row[0].Cells[1].Value.ToString() + " " + row[0].Cells[2].Value.ToString()[0] + ". " + row[0].Cells[3].Value.ToString()[0] + ".";
                    t2.Text = row[0].Cells[4].Value.ToString();
                    t3.Text = row[0].Cells[0].Value.ToString();

                    form.Dispose();
                }
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox1, e.KeyCode);
            }
        }

        #endregion

        #region Доп.Форма Дата
        public static bool monthCalendar1_KeyDownDate(KeyEventArgs e , DateForm form , DateTimePicker pic)
        {
            if(e.KeyCode == Keys.Escape)
            {
                form.Dispose();
                return true;
            }
            else if(e.KeyCode == Keys.Enter)
            {
                pic.Value = form.monthCalendar1.SelectionStart.Date;
                form.Dispose();

                return true;
            }

            string symbol = "";

            switch (e.KeyCode)
            {
                case Keys.D0:
                    symbol = "0"; break;
                case Keys.D1:
                    symbol = "1"; break;
                case Keys.D2:
                    symbol = "2"; break;
                case Keys.D3:
                    symbol = "3"; break;
                case Keys.D4:
                    symbol = "4"; break;
                case Keys.D5:
                    symbol = "5"; break;
                case Keys.D6:
                    symbol = "6"; break;
                case Keys.D7:
                    symbol = "7"; break;
                case Keys.D8:
                    symbol = "8"; break;
                case Keys.D9:
                    symbol = "9"; break;
                case Keys.Back:
                   if(form.textBox1.Text.Length != 0)
                        form.textBox1.Text = form.textBox1.Text.Remove(form.textBox1.Text.Length - 1, 1);
                    return true;
            }

            if(string.IsNullOrEmpty(symbol))
            {
                return false;
            }

            var textBox = form.textBox1;

            textBox.Text += symbol;

            return false;

        }

        public static void textBox1_TextChanged(EventArgs e , TextBox textBox , MonthCalendar calendar)
        {
            if (!long.TryParse(textBox.Text, out long n))
            {
                if(textBox.Text.Length != 0)
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
            }
            else
            {
                var date = SystemHelper.GetDate(textBox.Text, calendar.SelectionStart);
                
                if(date != DateTime.MinValue)
                {
                    calendar.SelectionStart = date.Date;
                }
            }
        }

        #endregion

    }
}
