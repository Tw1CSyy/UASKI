using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using UASKI.Models;
using UASKI.Services;
using UASKI.Models.Pages;

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
        /// Стартовый метод
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

            // Инициализируем системные переменные
            SystemData.Form = this;
            SystemData.Index = 0;
            SystemData.IsQuery = false;

            // Открываем подключение
            try
            {
                DataModel.Open();
            }
            catch (Exception)
            {
                Menu_Step1.Visible = Menu_Step2.Visible = false;
                ErrorHelper.StatusError();
            }
            
            InitPage();
        }

        /// <summary>
        /// Инициализируем страницы
        /// </summary>
        private void InitPage()
        {
            SystemData.Pages.AddHoliday = new Pages.AddHoliday();
            SystemData.Pages.AddIsp = new Pages.AddIsp();
            SystemData.Pages.AddTask = new Pages.AddTask();
            SystemData.Pages.EditTask = new Pages.EditTask();
            SystemData.Pages.EditIsp = new Pages.EditIsp();
            SystemData.Pages.SelectArhiv = new Pages.SelectArhiv();
            SystemData.Pages.SelectIsp = new SelectIsp();
            SystemData.Pages.SelectTask = new Pages.SelectTask();
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
            var form = this;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.Menu_Step2.SelectedIndex = 0;
                form.Menu_Step1.Enabled = false;
            }
            else
            {
                int si = SystemHelper.GetIntKeyDown(e.KeyCode);

                if(si != 0 && si != -1 && Menu_Step1.Items.Count >= si)
                {
                    Menu_Step1.SelectedIndex = si - 1;
                    var r = new KeyEventArgs(Keys.Enter);
                    Menu_Step1_KeyDown(sender, r);
                }
            }
        }
        private void Menu_Step2_KeyDown(object sender, KeyEventArgs e)
        {
            var form = this;

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
            else
            {
                int si = SystemHelper.GetIntKeyDown(e.KeyCode);

                if (si != 0 && si != -1 && Menu_Step2.Items.Count >= si)
                {
                    Menu_Step2.SelectedIndex = si - 1;
                    NavigationHelper.Start();
                }
            }
        }
        private void Menu_Step2_Click(object sender, EventArgs e)
        {
            if(Menu_Step2.SelectedIndex != -1)
            {
                var r = new KeyEventArgs(Keys.Enter);
                Menu_Step2_KeyDown(sender , r);
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
            SystemData.Pages.SelectIsp.IspDataGridView_KeyDown(e);
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddTask.textBox1_KeyDown(e);
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddTask.textBox4_KeyDown(e);
        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddTask.textBox7_KeyDown(e);
        }
        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.AddTask.button1_KeyDown(e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddTask.dateTimePicker1_KeyDown(e);
        }
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddIsp.textBox8_KeyDown(e);
        }
        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddIsp.textBox9_KeyDown(e);
        }
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddIsp.textBox10_KeyDown(e);
        }
        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddIsp.textBox11_KeyDown(e);
        }
        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddIsp.textBox12_KeyDown(e);
        }
        private void button4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.AddIsp.button4_PreviewKeyDown(e);
        }
        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddHoliday.monthCalendar1_KeyDown(e);
        }
        private void button5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.AddHoliday.button5_PreviewKeyDown(e);
        }
        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.textBox18_KeyDown(e);
        }
        private void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.textBox17_KeyDown(e);
        }
        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.textBox16_KeyDown(e);
        }
        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.textBox15_KeyDown(e);
        }
        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.textBox14_KeyDown(e);
        }
        private void button6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditIsp.button6_KeyDown(e);
        }
        private void button7_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditIsp.button7_KeyDown(e);
        }
        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.dataGridView3_KeyDown(e);
        }
        private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditIsp.dataGridView4_KeyDown(e);
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.AddTask.dateTimePicker1_KeyDown(e);
        }
        private void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox26_KeyDown(e);
        }
        private void textBox21_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox21_KeyDown(e);
        }
        private void textBox27_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox27_KeyDown(e);
        }
        private void dateTimePicker4_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.dateTimePicker4_KeyDown(e);
        }
        private void dateTimePicker5_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.dateTimePicker5_KeyDown(e);
        }
        private void textBox28_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox28_KeyDown(e);
        }
        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox29_KeyDown(e);
        }
        private void button10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditTask.button10_PreviewKeyDown(e);
        }
        private void button11_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditTask.button11_PreviewKeyDown(e);
        }
        private void button12_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditTask.button12_PreviewKeyDown(e);
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
                       , IspService.GetListByDataGrid(IspService.GetList(textBox13.Text))
                       , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
        } 

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            SystemHelper.PullListInDataGridView(dataGridView3,
                        TasksService.GetListByDataGrid(TasksService.GetListTask(textBox19.Text)),
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));
        }

    }
}
