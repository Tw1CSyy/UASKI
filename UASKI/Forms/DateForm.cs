using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Helpers;

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
            KeyDownHelper.monthCalendar1_KeyDownDate(e, this , pic);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            KeyDownHelper.textBox1_TextChanged(e, textBox1, monthCalendar1);
        }
    }
}
