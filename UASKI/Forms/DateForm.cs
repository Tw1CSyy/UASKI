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

        public DateForm(DateTimePicker p)
        {
            InitializeComponent();
            pic = p;
            monthCalendar1.SelectionStart = p.Value.Date;

            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Focus();
        }

        public DateForm(DateTimePicker p1 , DateTimePicker p2)
        {
            InitializeComponent();
            pic = p1;
            pic2 = p2;
            monthCalendar1.MaxSelectionCount = 365;

            monthCalendar1.SelectionStart = p1.Value;
            monthCalendar1.SelectionEnd = p2.Value;

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
                if(pic2 == null)
                    pic.Value = form.monthCalendar1.SelectionStart.Date;
                else
                {
                    SystemData.IsClear = true;
                    pic.Value = form.monthCalendar1.SelectionRange.Start.Date;
                    SystemData.IsClear = false;
                    pic2.Value = form.monthCalendar1.SelectionRange.End.Date;
                }
                form.Dispose();

            }

            string symbol = SystemHelper.GetIntKeyDown(e.KeyCode).ToString();

            if(e.KeyCode == Keys.Back && form.textBox1.Text.Length != 0)
            {
                form.textBox1.Text = form.textBox1.Text.Remove(form.textBox1.Text.Length - 1, 1);
            }

            if (!symbol.Equals("-1"))
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

        private void button23_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            monthCalendar1_KeyDown(sender, key);
        }
    }
}
