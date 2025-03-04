using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UASKI.Forms
{
    public partial class DateForm : Form
    {
        private DateTimePicker pic;
        private DateTimePicker pic2;

        public DateForm(DateTimePicker p)
        {
            InitializeComponent();
            pic = p;
            monthCalendar1.SelectionStart = p.Value.Date;
            monthCalendar1.SelectionEnd = p.Value.Date;
           
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Focus();
        }

        public DateForm(DateTimePicker p1, DateTimePicker p2)
        {
            InitializeComponent();
            pic = p1;
            pic2 = p2;
            monthCalendar1.MaxSelectionCount = 365;
            monthCalendar1.SelectionStart = p1.Value;
            monthCalendar1.SelectionEnd = p1.Value;
            monthCalendar1.Focus();
        }

        private void GetOne()
        {
            var value = monthCalendar1.SelectionStart;
            var date = GetDate(textBox1.Text, value);

            if (date != DateTime.MinValue)
            {
                monthCalendar1.SelectionStart = date.Date;
            }
        }

        private void Data()
        {
            if (pic2 != null && pic2.Value.Date != monthCalendar1.SelectionRange.End.Date)
            {
                SystemData.IsClear = true;
                pic.Value = monthCalendar1.SelectionRange.Start.Date;
                SystemData.IsClear = false;
                pic2.Value = monthCalendar1.SelectionRange.End.Date;
            }
            else
            {
                pic.Value = monthCalendar1.SelectionRange.Start.Date;
            }
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Escape)
            {
                Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Data();
                Dispose();

            }
            else
            {
                string symbol = SystemHelper.GetIntKeyDown(e.KeyCode).ToString();

                if (e.KeyCode == Keys.Back && textBox1.Text.Length != 0)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }

                if (!symbol.Equals("-1"))
                {
                    var textBox = textBox1;
                    textBox.Text += symbol;
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!long.TryParse(textBox1.Text, out long n))
            {
                if (textBox1.Text.Length != 0)
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
            else
            {
                GetOne();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            monthCalendar1_KeyDown(sender, key);
        }

        private DateTime GetDate(string text, DateTime dateFrom)
        {
            DateTime date;

            try
            {
                if (text.Length == 2)
                {
                    var day = Convert.ToInt32(text);

                    if (dateFrom.Month != 12 && day > new DateTime(dateFrom.Year, dateFrom.Month + 1, 1).AddDays(-1).Day)
                        day = new DateTime(dateFrom.Year, dateFrom.Month + 1, 1).AddDays(-1).Day;
                    else if(dateFrom.Month == 12 && day > new DateTime(dateFrom.Year + 1, 1, 1).AddDays(-1).Day)
                        day = new DateTime(dateFrom.Year + 1, 1, 1).AddDays(-1).Day;

                    date = new DateTime(dateFrom.Year, dateFrom.Month, day);
                    return date;
                }

                if (text.Length == 4)
                {
                    string value = string.Empty;
                    value += text[0];
                    value += text[1];
                    var day = Convert.ToInt32(value);

                    value = string.Empty;
                    value += text[2];
                    value += text[3];

                    var month = Convert.ToInt32(value);

                    if (month != 12 && day > new DateTime(dateFrom.Year, month + 1, 1).AddDays(-1).Day)
                        day = new DateTime(dateFrom.Year, month + 1, 1).AddDays(-1).Day;
                    else if(month == 12 && day > new DateTime(dateFrom.Year + 1, 1, 1).AddDays(-1).Day)
                        day = new DateTime(dateFrom.Year + 1, 1, 1).AddDays(-1).Day;

                    date = new DateTime(dateFrom.Year, month, day);
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
                return dateFrom;
            }

            return DateTime.MinValue;
        }

    }
}
