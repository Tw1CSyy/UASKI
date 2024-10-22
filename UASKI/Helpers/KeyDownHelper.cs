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
        private static readonly Gl_Form form = SystemData.Form;
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

        #region Просмотр исполнителей
        /// <summary>
        /// При нажатии на таблице в просмотре
        /// </summary>
        /// <param name="e">Объект события</param>
        public static void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.IspDataGridView.SelectedRows.Count != 0
                && form.IspDataGridView.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.IspDataGridView.ClearSelection();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if(form.IspDataGridView.SelectedRows.Count > 0)
                {
                    var code = Convert.ToInt32(form.IspDataGridView.SelectedRows[0].Cells[0].Value);
                    NavigationHelper.GetIspView(code);
                }
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox13, e.KeyCode);
            }
        }

        #endregion

        #region Просмотр задач
        public static void dataGridView3_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.dataGridView3.SelectedRows.Count != 0
                && form.dataGridView3.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.dataGridView3.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox19, e.KeyCode);
            }
        }

        #endregion

        #region Добавление задачи

        public static void textBox1_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if(form.textBox1.Text.Length != 0 && form.textBox3.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox1.Text);

                    if(isp != null)
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
            else if(e.KeyCode == ActionKey)
            {
                var form1 = new IspForm(form.textBox1 , form.textBox2 , form.textBox3);
                form1.Show();
            }
            else if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public static void textBox4_KeyDown(KeyEventArgs e)
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
            else if (e.KeyCode == ActionKey)
            {
                var form1 = new IspForm(form.textBox4 , form.textBox5 , form.textBox6);
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

        public static void textBox7_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker1.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox4);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public static void button1_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    var result = TasksService.Add(
                    new TextBoxElement(form.textBox7, form.label25),
                    new TextBoxElement(form.textBox3, form.label23),
                    new TextBoxElement(form.textBox6, form.label24),
                    new DateTimeElement(form.dateTimePicker1, form.label26)
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

        public static void dateTimePicker1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SystemHelper.SelectButton(true, form.button1);
                form.button1.Focus();
            }
            else if(e.KeyCode == Keys.Left)
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
            else if(e.KeyCode == ActionKey)
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

        #endregion

        #region Добавления исполнителей

        public static void textBox8_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox9);
            }
            else if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public static void textBox9_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox10);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox8);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public static void textBox10_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox11);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox9);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public static void textBox11_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox12);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(true, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public static void textBox12_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox11);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public static void button4_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    var result = IspService.Add(
                    new TextBoxElement(form.textBox8, form.label18),
                    new TextBoxElement(form.textBox9, form.label19),
                    new TextBoxElement(form.textBox10, form.label20),
                    new TextBoxElement(form.textBox11, form.label21),
                    new TextBoxElement(form.textBox12, form.label22)
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
            else if(e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button4);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox12);
                SystemHelper.SelectButton(false, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }

            e.IsInputKey = true;
        }

        public static void dataGridView1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectTextBox(form.textBox8);
                SystemHelper.SelectDataGridView(false, form.dataGridView1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectDataGridView(false, form.dataGridView1);
                e.Handled = true;
            }
        }

        #endregion

        #region Добавления праздников
        public static void monthCalendar1_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button5);
                form.button5.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public static void button5_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.monthCalendar1.Focus();
                SystemHelper.SelectButton(false, form.button5);
               
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = HolidaysService.Add
                    (
                        new MonthElement(form.monthCalendar1, form.label27)
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
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button5);
            }

            e.IsInputKey = true;
        }
        #endregion

        #region Управление Исполнителями

        public static void textBox18_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox17);
            }
        }

        public static void textBox17_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox18);
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
            }
        }

        public static void textBox16_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox17);
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox15);
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
            }
        }

        public static void textBox15_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox14);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
            }
        }

        public static void textBox14_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox15);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
        }

        public static void button6_KeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button6);
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = IspService.Update
                   (
                   Convert.ToInt32(form.label38.Text),
                   new TextBoxElement(form.textBox18, form.label32),
                   new TextBoxElement(form.textBox17, form.label31),
                   new TextBoxElement(form.textBox16, form.label30),
                   new TextBoxElement(form.textBox15, form.label29),
                   new TextBoxElement(form.textBox14, form.label28)
                   );

                    if (result)
                    {
                        NavigationHelper.ClearForm();
                        NavigationHelper.GetIspSelectView();
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
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button6);
                SystemHelper.SelectButton(true, form.button7);
            }
            else if(e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button6);
                NavigationHelper.GetIspSelectView();
            }
            e.IsInputKey = true;
        }

        public static void button7_KeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button7);
                SystemHelper.SelectButton(true, form.button6);
            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button7);
                SystemHelper.SelectTextBox(form.textBox18);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    var code = Convert.ToInt32(form.label38.Text);

                    if (IspService.Disactive(code))
                    {
                        NavigationHelper.ClearForm();
                        NavigationHelper.GetIspSelectView();
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
            else if(e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectButton(false, form.button7);
                NavigationHelper.GetIspSelectView();
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button7);

                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }

            e.IsInputKey = true;
        }

        public static void dataGridView4_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView4.SelectedRows.Count != 0
                && form.dataGridView4.SelectedRows[0].Index == 0))
            {

                SystemHelper.SelectDataGridView(false, form.dataGridView4);
                SystemHelper.SelectTextBox(form.textBox18);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectDataGridView(false, form.dataGridView4);
                SystemHelper.SelectTextBox(form.textBox18);
                e.Handled = true;
            }
        }

        #endregion

        #region Управление задачами-архивом
        public static void textBox26_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox25.Text.Length != 0 && form.textBox24.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox26.Text);

                    if (isp != null)
                    {
                        form.textBox25.Text = isp.CodePodr.ToString();
                        form.textBox24.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox21);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox21);
                }
            }
            else if(e.KeyCode == ActionKey)
            {
                var ispForm = new IspForm(form.textBox26, form.textBox25, form.textBox24);
                ispForm.Show();
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true , form.button10);
            }  
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public static void textBox21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox22.Text.Length != 0 && form.textBox23.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox21.Text);

                    if (isp != null)
                    {
                        form.textBox22.Text = isp.CodePodr.ToString();
                        form.textBox23.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox27);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox27);
                }
            }
            else if (e.KeyCode == ActionKey)
            {
                var ispForm = new IspForm(form.textBox21, form.textBox22, form.textBox23);
                ispForm.Show();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox26);
            }
        }

        public static void textBox27_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker4.Focus();
            }
            else if(e.KeyCode == Keys.Down && form.panel10.Visible)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox21);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public static void dateTimePicker4_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if(e.KeyCode == Keys.Down && form.panel10.Visible)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true , form.button10);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if(form.panel10.Visible)
                    form.dateTimePicker5.Focus();
                else
                    SystemHelper.SelectButton(true, form.button10);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
            else if(e.KeyCode == ActionKey)
            {
                var date = new DateForm(form.dateTimePicker4);
            }
        }

        public static void dateTimePicker5_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox28);
            }
            else if(e.KeyCode == ActionKey)
            {
                var dateForm = new DateForm(form.dateTimePicker5);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public static void textBox28_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker5.Focus();
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox29);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
            
        }

        public static void textBox29_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox28);
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button10);
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker4.Focus();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public static void button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }

        public static void button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }

        public static void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }
        #endregion
    }
}
