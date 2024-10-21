using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using UASKI.Data.Context;
using UASKI.Models;
using UASKI.Services;

namespace UASKI
{
    public partial class Gl_Form : Form
    {
        public int timerTick = 0;
        public Gl_Form()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Старотовый метод
        /// </summary>
        private void Start()
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            TimeTimer.Start();

            // Рисуем меню
            SystemHelper.WriteListBox(Menu_Step1, SystemData.MenuItems.Select(c => c.Text).ToArray());
            Menu_Step1.SelectedIndex = 0;
            Menu_Step1.Focus();

            // Отключаем отображение страниц
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            SystemData.Form = this;
            SystemData.Index = 0;
            SystemData.IsQuery = false;
            DataModel.Open();
        }

        // При смене выбраного элемента меню 1го уровня меняем содержимое 2го меню
        private void Menu_Step1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Menu_Step1.SelectedIndex != -1)
            {
                var text = Menu_Step1.Items[Menu_Step1.SelectedIndex].ToString();
                var item = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(text));
                SystemHelper.WriteListBox(Menu_Step2, item.Items.Select(c => c.Text).ToArray());
            }
        }

        // Таймер статуса
        private void TimerStatus_Tick(object sender, EventArgs e)
        {
            if (timerTick == 2)
            {
                TimerStatus.Stop();
                LabelStatus.Visible = false;
                timerTick = 0;
                SystemData.IsQuery = false;
            }
            else
            {
                timerTick++;
            }
        }

        // Таймер времени
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        #region Нажатия клавиш

        private void Menu_Step1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.Menu_Step1_KeyDown(e);
        }
        private void Menu_Step2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.Menu_Step2_KeyDown(e);
        }
        private void Menu_Step2_Click(object sender, EventArgs e)
        {
            if(Menu_Step2.SelectedIndex != -1)
            {
                var r = new KeyEventArgs(Keys.Enter);
                KeyDownHelper.Menu_Step2_KeyDown(r);
            }
        }
        private void panel3_Click(object sender, EventArgs e)
        {
            Menu_Step2.Enabled = true;
            Menu_Step1.Enabled = false;
        }
        private void panel4_Click(object sender, EventArgs e)
        {
            Menu_Step1.Enabled = true;
            Menu_Step2.Enabled = false;
        }
        private void IspDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.IspDataGridView_KeyDown(e);
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox1_KeyDown(e);
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox4_KeyDown(e);
        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox7_KeyDown(e);
        }
        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyDownHelper.button1_KeyDown(e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var r = new PreviewKeyDownEventArgs(Keys.Enter);
            KeyDownHelper.button1_KeyDown(r);
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.dateTimePicker1_KeyDown(e);
        }
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox8_KeyDown(e);
        }
        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox9_KeyDown(e);
        }
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox10_KeyDown(e);
        }
        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox11_KeyDown(e);
        }
        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox12_KeyDown(e);
        }
        private void button4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyDownHelper.button4_PreviewKeyDown(e);
        }
        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.monthCalendar1_KeyDown(e);
        }
        private void button5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyDownHelper.button5_PreviewKeyDown(e);
        }
        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox18_KeyDown(e);
        }
        private void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox17_KeyDown(e);
        }
        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox16_KeyDown(e);
        }
        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox15_KeyDown(e);
        }
        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.textBox14_KeyDown(e);
        }
        private void button6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyDownHelper.button6_KeyDown(e);
        }
        private void button7_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            KeyDownHelper.button7_KeyDown(e);
        }

        #endregion

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if(monthCalendar1.SelectionRange.Start.Date == monthCalendar1.SelectionRange.End.Date)
            {
                label17.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd.MM.yyyy");
            }
            else
            {
                label17.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd.MM.yyyy") + " - " +
                    monthCalendar1.SelectionRange.End.Date.ToString("dd.MM.yyyy");
            }
        }
        
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            SystemHelper.PullListInDataGridView(IspDataGridView
                       , IspService.GetListByDataGrid(textBox13.Text)
                       , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
        }

    }
}
