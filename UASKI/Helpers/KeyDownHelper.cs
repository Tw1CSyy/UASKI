using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Services;
using UASKI.StaticModels;

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
            else if(e.KeyCode == Keys.Enter)
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
        public static void dataGridView_KeyDown(KeyEventArgs e , IspForm form , TextBox t1 , TextBox t2 , TextBox t3 , DataGridView d)
        {
            if(e.KeyCode == Keys.Escape)
            {
                form.Dispose();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                var row = d.SelectedRows;
                
                if(row != null && row.Count > 0)
                {
                    t1.Text = row[0].Cells[1].Value.ToString() + " " + row[0].Cells[2].Value.ToString()[0] + ". " + row[0].Cells[3].Value.ToString()[0] + ".";
                    t2.Text = row[0].Cells[4].Value.ToString();
                    t3.Text = row[0].Cells[0].Value.ToString();

                    form.Dispose();
                }
            }
        }

        #endregion

        #region Доп.Форма Дата
        /// <summary>
        /// День
        /// </summary>
        /// <param name="e">Объект события</param>
        /// <param name="f">Форма</param>
        public static void textBox1_KeyDown(KeyEventArgs e , DateForm f)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                f.listBox1.Focus();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                f.Dispose();
            }
        }

        /// <summary>
        /// Месяц
        /// </summary>
        /// <param name="e">Объект события</param>
        /// <param name="f">Форма</param>
        public static void listBox1_KeyDown(KeyEventArgs e , DateForm f)
        {
            if(e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                f.textBox1.Focus();
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                f.numericUpDown1.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                f.Dispose();
            }
        }

        /// <summary>
        /// Год
        /// </summary>
        /// <param name="e">Объект события</param>
        /// <param name="f">Форма</param>
        public static void numericUpDown1_KeyDown(KeyEventArgs e , DateForm f , DateTimePicker d)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    var dat = new DateTime((int)f.numericUpDown1.Value, f.listBox1.SelectedIndex + 1, Convert.ToInt32(f.textBox1.Text));

                    d.Value = dat;
                    f.Dispose();
                }
                catch (Exception)
                {
                    MessageHelper.Error("Ошибка при заполнении данных");
                }
            }
            else if(e.KeyCode == Keys.Escape)
            {
                f.Dispose();
            }   
            else if(e.KeyCode == Keys.Left)
            {
                f.listBox1.Focus();
            }
        }

        #endregion

        #region 1 страница
        /// <summary>
        /// При нажатии на таблице в просмотре
        /// </summary>
        /// <param name="e">Объект события</param>
        public static void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if((e.KeyCode == Keys.Up 
                && form.IspDataGridView.SelectedRows.Count != 0 
                && form.IspDataGridView.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.IspDataGridView.ClearSelection();
            }
        }
        #endregion

        #region 5 страница

        /// <summary>
        /// При нажитии клавиши для выбора из списка исполнителей
        /// </summary>
        /// <param name="e">Объект события</param>
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

                    form.textBox4.Focus();
                }
                else
                {
                    form.textBox4.Focus();
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

        /// <summary>
        /// При нажитии клавиши для выбора из списка исполнителей
        /// </summa\ry>
        /// <param name="e">Объект события</param>
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

                    form.textBox7.Focus();
                }
                else
                {
                    form.textBox7.Focus();
                }
            }
            else if (e.KeyCode == ActionKey)
            {
                var form1 = new IspForm(form.textBox4 , form.textBox5 , form.textBox6);
                form1.Show();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.textBox1.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// 5 страница
        /// </summary>
        /// <param name="e">Объект запроса</param>
        public static void textBox7_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker1.Focus();
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.textBox4.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(true, form.button1);
                form.button1.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// 5 страница кнопка
        /// </summary>
        /// <param name="e">Объект запроса</param>
        public static void button1_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(TasksService.AddCheck(form.textBox7, form.textBox3, form.textBox6, form.dateTimePicker1)
                    && TasksService.Add(form.textBox7.Text , form.textBox3.Text , form.textBox6.Text , form.dateTimePicker1.Value))
                {
                    MessageHelper.Info("Новое задание добавлено");
                    NavigationHelper.ClearForm();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.textBox4.Focus();
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

                form.textBox7.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                form.textBox4.Focus();
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

        #region 6 страница

        /// <summary>
        /// При нажатии на ТекстБох с фамилией
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void textBox8_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.textBox9.Focus();
            }
            else if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// При нажатии на ТекстБох с именем
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void textBox9_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.textBox10.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.textBox8.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// При нажатии на ТекстБох с отчеством
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void textBox10_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.textBox11.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.textBox9.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// При нажатии на ТекстБох с кодом
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void textBox11_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.textBox12.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.textBox10.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if(e.KeyCode == Keys.Down)
            {
                form.button4.Focus();
                SystemHelper.SelectButton(true, form.button4);
            }
        }

        /// <summary>
        /// При нажатии на ТекстБох с кодом подразделения
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void textBox12_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                form.button4.Focus();
                SystemHelper.SelectButton(true, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                form.textBox11.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        /// <summary>
        /// При нажатии на кнопку действия
        /// </summary>
        /// <param name="e">Объект с событием</param>
        public static void button4_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {

            }
            else if(e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button4);
            }
            else if(e.KeyCode == Keys.Up)
            {
                form.textBox12.Focus();
                SystemHelper.SelectButton(false, form.button4);
            }
        }
        #endregion
    }
}
