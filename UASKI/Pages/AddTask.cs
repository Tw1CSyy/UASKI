using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class AddTask : BasePage
    {
        private Gl_Form form = SystemData.Form;
        
        public override void Show()
        {
            form.textBox1.Focus();
        }

        public override void Clear()
        {
            form.textBox1.Clear();
            form.textBox2.Clear();
            form.textBox3.Clear();
            form.textBox4.Clear();
            form.textBox5.Clear();
            form.textBox6.Clear();
            form.textBox7.Clear();

            SystemHelper.SelectButton(false, form.button1);
            form.dateTimePicker1.Value = System.DateTime.Today;
            form.textBox1.Focus();
        }

        public void textBox1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox1.Text.Length != 0 && form.textBox3.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox1.Text);

                    if (isp != null)
                    {
                        form.textBox2.Text = isp.CodePodr.ToString();
                        form.textBox3.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox4);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox4);
                }
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var form1 = new IspForm(form.textBox1, form.textBox2, form.textBox3);
                form1.Show();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public void textBox4_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox4.Text.Length != 0 && form.textBox6.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox4.Text);

                    if (isp != null)
                    {
                        form.textBox5.Text = isp.CodePodr.ToString();
                        form.textBox6.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox7);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox7);
                }
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var form1 = new IspForm(form.textBox4, form.textBox5, form.textBox6);
                form1.Show();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox1);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public void textBox7_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox4);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public void button1_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = TasksService.Add(
                    TextBoxElement.New(form.textBox7, form.label25),
                    TextBoxElement.New(form.textBox3, form.label23),
                    TextBoxElement.New(form.textBox6, form.label24),
                    DateTimeElement.New(form.dateTimePicker1, form.label26)
                    );

                    if (result)
                    {
                        NavigationHelper.ClearForm();
                        ErrorHelper.StatusComlite();
                    }
                    else
                    {
                        ErrorHelper.StatusError();
                    }
                }
                else
                {
                    ErrorHelper.StatusQuery();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox4);
                SystemHelper.SelectButton(false, form.button1);
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectButton(false, form.button1);
                form.dateTimePicker1.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button1);
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SystemHelper.SelectButton(true, form.button1);
                form.button1.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                SystemHelper.SelectTextBox(form.textBox7);
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                SystemHelper.SelectTextBox(form.textBox4);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker1);
                f.Show();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }
    }
}
