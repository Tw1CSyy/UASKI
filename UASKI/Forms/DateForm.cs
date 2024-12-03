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
        private static DateTimePicker pic;
        private static DateTimePicker pic2;

        private bool IsTwo = false;

        public DateForm(DateTimePicker p)
        {
            InitializeComponent();
            pic = p;
            monthCalendar1.SelectionStart = p.Value.Date;
            monthCalendar1.SelectionEnd = p.Value.Date;
            IsTwo = false;

            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Focus();
        }

        public DateForm(DateTimePicker p1, DateTimePicker p2)
        {
            InitializeComponent();
            pic = p1;
            pic2 = p2;
            monthCalendar1.MaxSelectionCount = 365;
            IsTwo = true;
            monthCalendar1.SelectionStart = p1.Value;
            monthCalendar1.SelectionEnd = p1.Value;
            monthCalendar1.Focus();
        }

        private void GetOne()
        {
            var value = monthCalendar1.SelectionStart;
            var date = SystemHelper.GetDate(textBox1.Text, value);

            if (date != DateTime.MinValue)
            {
                monthCalendar1.SelectionStart = date.Date;
            }
        }

        private void GetTwo()
        {
            var start = monthCalendar1.SelectionStart;
            var value = monthCalendar1.SelectionEnd;
            var date = SystemHelper.GetDate(textBox1.Text, value);
             
            if (date != DateTime.MinValue)
            {
                monthCalendar1.SelectionStart = start;
                monthCalendar1.SelectionEnd = date.Date;
            }
        }

        private void Data()
        {
            if (pic2.Value.Date != monthCalendar1.SelectionRange.End.Date)
            {
                SystemData.IsClear = true;
                pic.Value = monthCalendar1.SelectionRange.Start.Date;
                SystemData.IsClear = false;
                pic2.Value = monthCalendar1.SelectionRange.End.Date;
            }
            else
            {
                pic.Value = monthCalendar1.SelectionRange.Start.Date;

                SystemData.IsClear = true;
                pic2.Value = monthCalendar1.SelectionRange.End.Date;
                SystemData.IsClear = false;
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
                if(!IsTwo)
                    pic.Value = monthCalendar1.SelectionStart.Date;
                else
                {
                    Data();
                   
                }
                Dispose();

            }

            if(e.Control && (e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3))
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        monthCalendar1.SelectionStart = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 1);
                        monthCalendar1.SelectionEnd = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 10);
                        break;
                    case Keys.D2:
                        monthCalendar1.SelectionStart = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 11);
                        monthCalendar1.SelectionEnd = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 20);
                        break;
                    case Keys.D3:
                        monthCalendar1.SelectionStart = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 21);
                        monthCalendar1.SelectionEnd = new DateTime(monthCalendar1.SelectionStart.Year, monthCalendar1.SelectionStart.Month, 1).AddMonths(1).AddDays(-1);
                        break;
                }

                if (!IsTwo)
                    pic.Value = monthCalendar1.SelectionStart.Date;
                else
                {
                    Data();
                }
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
                if (IsTwo)
                    GetTwo();
                else
                    GetOne();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            monthCalendar1_KeyDown(sender, key);
        }

    }
}
