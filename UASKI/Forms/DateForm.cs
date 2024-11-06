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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UASKI.Forms
{
    public partial class DateForm : Form
    {
        private static DateTimePicker pic;
        public DateForm(DateTimePicker p)
        {
            InitializeComponent();
            pic = p;
            monthCalendar1.SelectionStart = p.Value.Date;

            monthCalendar1.Focus();
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            var form = this;

            if (e.KeyCode == Keys.Escape)
            {
                form.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                pic.Value = form.monthCalendar1.SelectionStart.Date;
                form.Dispose();

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
                    if (form.textBox1.Text.Length != 0)
                        form.textBox1.Text = form.textBox1.Text.Remove(form.textBox1.Text.Length - 1, 1);
                    break;
            }

            if (!string.IsNullOrEmpty(symbol))
            {
                var textBox = form.textBox1;
                textBox.Text += symbol;
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
                var date = SystemHelper.GetDate(textBox1.Text, monthCalendar1.SelectionStart);

                if (date != DateTime.MinValue)
                {
                    monthCalendar1.SelectionStart = date.Date;
                }
            }
        }
    }
}
