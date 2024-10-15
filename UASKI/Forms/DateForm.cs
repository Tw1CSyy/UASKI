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
            numericUpDown1.Value = DateTime.Today.Year;
            numericUpDown2.Focus();        
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox1_KeyDown(e, this);
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.listBox1_KeyDown(e, this);
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.numericUpDown1_KeyDown(e, this , pic);
        }
    }
}
