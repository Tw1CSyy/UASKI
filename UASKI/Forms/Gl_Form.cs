using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using UASKI.Models.Components;
using UASKI.Core.Models;
using UASKI.Core;
using System.Threading;

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
        private bool Start()
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            TimeTimer.Start();

            // Инициализируем системные переменные
            ComponentIniz();
            SystemData.Init(this);
            Ai.Iniz(textBox41);
            
            // Рисуем меню
            foreach (var item in SystemData.MenuItems.Select(c => c.Text).ToArray())
                Menu_Step1.Items.Add(item);

            Menu_Step1.SelectedIndex = 0;
            Menu_Step1.Focus();

            // Отключаем отображение страниц
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            Ai.AddMessage(Enums.TypeNotice.Default, "Привет");

            // Запускаем таймер Ai
            AiTimer.Start();

            // Создаем или загружаем файл настроек
            if(ApplicationHelper.Settings())
            {
                Ai.AddMessage(Enums.TypeNotice.Default, "Файл настроек загружен");
            }
            else
            {
                Ai.AddMessage(Enums.TypeNotice.Error, "Ошибка при загрузке настроечного файла");
                return false;
            }

            // Открываем подключение
            var settings = SystemData.Settings;
            string connectionString = $"Host={settings.Host};UserName={settings.User};Password={settings.Password};Database={settings.DateBase};Port={settings.Port}";
            var connection = new DataConnection(connectionString);

            if(!connection.IsConnection)
            {
                Ai.AddMessage(Enums.TypeNotice.Error, "Ошибка при подключении к базе данных");
                return false;
            }

            Ai.AddMessage(Enums.TypeNotice.Default, "Подключение с базой получено");

            if (ApplicationHelper.Dump())
                Ai.AddMessage(Enums.TypeNotice.Default, "Резервная копия базы сделана");

            Ai.AddMessage(Enums.TypeNotice.Default, "Включаю приложение. Удачной работы!");
            Menu_Step1.Visible = Menu_Step2.Visible = true;

            var count = TaskModel.GetList().Count(c => c.Date == DateTime.Today);

            if(count != 0)
                Ai.AddMessage(Enums.TypeNotice.Default, $"Сегодня должны закрыться карточки: {count}");
            else
                Ai.AddMessage(Enums.TypeNotice.Default, $"Сегодня нет карточек на закрытие");

            if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
                var dateList = ApplicationHelper.DeleteHoliday();

                if (dateList.Length > 0)
                    Ai.AddMessage(Enums.TypeNotice.Default, "Удалены прошедшие даты: ", dateList.Select(c => c.Date.ToString("dd.MM.yyyy")).ToArray());
                else
                    Ai.AddMessage(Enums.TypeNotice.Default, $"Нет дат на удаление");

                dateList = ApplicationHelper.AddHoliday();

                if (dateList.Length > 0)
                    Ai.AddMessage(Enums.TypeNotice.Default, "Добавлены новые даты: ", dateList.Select(c => c.Date.ToString("dd.MM.yyyy")).ToArray());
                else
                    Ai.AddMessage(Enums.TypeNotice.Default, $"Нет дат на добавление");
            }

            return true;
        }

        #region Системное

        // При смене выбраного элемента меню 1го уровня меняем содержимое 2го меню
        private void Menu_Step1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Menu_Step1.SelectedIndex != -1)
            {
                var text = Menu_Step1.Items[Menu_Step1.SelectedIndex].ToString();
                var items = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(text));
                Menu_Step2.Items.Clear();

                foreach (var item in items.Items.Select(c => c.Text).ToArray())
                {
                    Menu_Step2.Items.Add(item);
                }
            }
        }

        // Таймер уведомлений Ai
        private void AiTimer_Tick(object sender, EventArgs e)
        {
            Ai.Timer();
        }

        // Таймер времени
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        // Открываем страницы по меню
        private void Open()
        {
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(Menu_Step2.SelectedItem.ToString()));

            if (el.Page != null)
                el.Page.Init();
        }

        #endregion

        #region Компоненты-Переменные
        
        private void ComponentIniz()
        {
            IspDataGridView = new DataGridViewComponent(ispDataGridView);
            DataGridView3 = new DataGridViewComponent(dataGridView3);
            DataGridView5 = new DataGridViewComponent(dataGridView5);
            DataGridView6 = new DataGridViewComponent(dataGridView6);
            DataGridView1 = new DataGridViewComponent(dataGridView1);
            DataGridView2 = new DataGridViewComponent(dataGridView2);
            DataGridView4 = new DataGridViewComponent(dataGridView4);
            DataGridView7 = new DataGridViewComponent(dataGridView7);
            DataGridView8 = new DataGridViewComponent(dataGridView8);
            DataGridView9 = new DataGridViewComponent(dataGridView9);
            DataGridView10 = new DataGridViewComponent(dataGridView10);
            DataGridView11 = new DataGridViewComponent(dataGridView11);
            DataGridView13 = new DataGridViewComponent(dataGridView13);
            DataGridView12 = new DataGridViewComponent(dataGridView12);
        }

        public DataGridViewComponent IspDataGridView { get; private set; }
        public DataGridViewComponent DataGridView3 { get; private set; }
        public DataGridViewComponent DataGridView5 { get; private set; }
        public DataGridViewComponent DataGridView6 { get; private set; }
        public DataGridViewComponent DataGridView1 { get; private set; }
        public DataGridViewComponent DataGridView2 { get; private set; }
        public DataGridViewComponent DataGridView4 { get; private set; }
        public DataGridViewComponent DataGridView7 { get; private set; }
        public DataGridViewComponent DataGridView8 { get; private set; }
        public DataGridViewComponent DataGridView9 { get; private set; }
        public DataGridViewComponent DataGridView10 { get; private set; }
        public DataGridViewComponent DataGridView11 { get; private set; }
        public DataGridViewComponent DataGridView13 { get; private set; }
        public DataGridViewComponent DataGridView12 { get; private set; }
        #endregion

        #region Нажатия клавиш

        private void Menu_Step1_KeyDown(object sender, KeyEventArgs e)
        {
            var form = this;

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
                Open();
            }
            else
            {
                int si = SystemHelper.GetIntKeyDown(e.KeyCode);

                if (si != 0 && si != -1 && Menu_Step2.Items.Count >= si)
                {
                    Menu_Step2.SelectedIndex = si - 1;
                    Open();
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
        private void textBox28_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.textBox28_KeyDown(e);
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
        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectIsp.textBox13_KeyDown(e);
        }
        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.textBox19_KeyDown(e);
        }
        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.textBox29_KeyDown(e);
        }
        private void textBox32_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.textBox32_KeyDown(e);
        }
        private void dateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.dateTimePicker3_KeyDown(e);
        }
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.dateTimePicker2_KeyDown(e);
        }
        private void textBox31_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.textBox31_KeyDown(e);
        }
        private void checkBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectArhiv.checkBox1_PreviewKeyDown(e);
        }
        private void dataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.dataGridView5_KeyDown(e);
        }
        private void checkBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectTask.checkBox2_PreviewKeyDown(e);
        }
        private void dateTimePicker5_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.dateTimePicker5_KeyDown(e);
        }
        private void dateTimePicker6_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.dateTimePicker6_KeyDown(e);
        }
        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.dataGridView1_KeyDown_1(e);
        }
        private void textBox33_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.textBox33_KeyDown(e);
        }
        private void textBox34_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.textBox34_KeyDown(e);
        }
        private void checkBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectOpz.checkBox3_PreviewKeyDown(e);
        }
        private void dateTimePicker7_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.dateTimePicker7_KeyDown(e);
        }
        private void dateTimePicker8_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.dateTimePicker8_KeyDown(e);
        }
        private void dataGridView6_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectHoliday.dataGridView6_KeyDown(e);
        }
        private void button13_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectHoliday.button13_PreviewKeyDown(e);
        }
        private void dateTimePicker9_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditTask.dateTimePicker9_KeyDown(e);
        }
        private void dateTimePicker10_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintTaskList.dateTimePicker10_KeyDown(e);
        }
        private void dateTimePicker11_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintTaskList.dateTimePicker11_KeyDown(e);
        }
        private void textBox30_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintTaskList.textBox30_KeyDown(e);
        }
        private void button34_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.PrintTaskList.button34_PreviewKeyDown(e);
        }
        private void dataGridView7_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintTaskList.dataGridView7_KeyDown(e);
        }
        private void button37_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.PrintOpz.button37_PreviewKeyDown(e);
        }
        private void dataGridView8_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintOpz.dataGridView8_KeyDown(e);
        }
        private void textBox35_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintMer.textBox35_KeyDown(e);
        }
        private void dataGridView9_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintMer.dataGridView9_KeyDown(e);
        }
        private void button38_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.PrintMer.button38_PreviewKeyDown(e);
        }
        private void dateTimePicker12_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintPoc.dateTimePicker12_KeyDown(e);
        }
        private void dateTimePicker13_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintPoc.dateTimePicker13_KeyDown(e);
        }
        private void button40_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.PrintPoc.button40_PreviewKeyDown(e);
        }
        private void dataGridView10_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintPoc.dataGridView10_KeyDown(e);
        }
        private void textBox36_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintCof.textBox36_KeyDown(e);
        }
        private void dateTimePicker14_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintCof.dateTimePicker14_KeyDown(e);
        }
        private void dateTimePicker15_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintCof.dateTimePicker15_KeyDown(e);
        }
        private void button42_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.PrintCof.button42_PreviewKeyDown(e);
        }
        private void dataGridView11_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintCof.dataGridView11_KeyDown(e);
        }
        private void button47_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditTask.button47_PreviewKeyDown(e);
        }
        private void button48_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditTask.button48_PreviewKeyDown(e);
        }
        private void textBox38_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditPret.textBox38_KeyDown(e);
        }
        private void dateTimePicker16_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditPret.dateTimePicker16_KeyDown(e);
        }
        private void textBox39_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.EditPret.textBox39_KeyDown(e);
        }
        private void button46_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditPret.button46_PreviewKeyDown(e);
        }
        private void button45_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditPret.button45_PreviewKeyDown(e);
        }
        private void button44_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.EditPret.button44_PreviewKeyDown(e);
        }
        private void dataGridView12_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectPret.dataGridView12_KeyDown(e);
        }
        private void textBox40_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectPret.textBox40_KeyDown(e);
        }
        private void checkBox5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectPret.checkBox5_PreviewKeyDown(e);
        }
        private void checkBox6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectPret.checkBox6_PreviewKeyDown(e);
        }
        private void checkBox4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            SystemData.Pages.SelectPret.checkBox4_PreviewKeyDown(e);
        }
        private void dateTimePicker17_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectPret.dateTimePicker17_KeyDown(e);
        }
        private void dateTimePicker18_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectPret.dateTimePicker18_KeyDown(e);
        }
        private void dataGridView13_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.PrintCof.dataGridView13_KeyDown(e);
        }
        #endregion

        #region Обработка
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (monthCalendar1.SelectionRange.Start.Date == monthCalendar1.SelectionRange.End.Date)
            {
                label17.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd.MM.yyyy");
            }
            else
            {
                label17.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd.MM.yyyy") + " - " +
                    monthCalendar1.SelectionRange.End.Date.ToString("dd.MM.yyyy");
            }
        }
        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            button11.Enabled = (!SystemData.Pages.EditTask.IsArhiv && textBox28.Text.Length > 0 && int.TryParse(textBox28.Text, out int i)) || SystemData.Pages.EditTask.IsArhiv;
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                SystemData.Pages.SelectIsp.Select();
            }
        }
        private void textBox19_TextChanged_1(object sender, EventArgs e)
        {
           if(!SystemData.IsClear)
           {
                SystemData.Pages.SelectTask.Select();
           }
        }
        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                SystemData.Pages.SelectArhiv.Select();
            }
        }
        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                textBox32_TextChanged(sender, e);
                panel15.Visible = checkBox1.Checked;
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                textBox19_TextChanged_1(sender, e);
                panel16.Visible = checkBox2.Checked;
            }
        }
        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                SystemData.Pages.SelectOpz.Select();
            }
        }
        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
        }
        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                textBox33_TextChanged(sender, e);
                panel18.Visible = checkBox3.Checked;
            }
        }
        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
        }
        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
        }
        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                if (textBox20.Text.Length == 0 && textBox30.Text.Length >= 4)
                {
                    int code;

                    if (int.TryParse(textBox30.Text, out code))
                    {
                        var isp = IspModel.GetByCode(code);

                        if (isp != null)
                            textBox20.Text = isp.InizNotCode;
                    }
                }

                if (textBox30.Text.Length >= 4)
                {
                    SystemData.Pages.PrintTaskList.Select();
                }
                else
                {
                    dataGridView7.DataSource = null;
                    textBox20.Clear();
                }
            }
        }
        private void dateTimePicker10_ValueChanged(object sender, EventArgs e)
        {
            textBox30_TextChanged(sender , e);
        }
        private void dateTimePicker11_ValueChanged(object sender, EventArgs e)
        {
            textBox30_TextChanged(sender, e);
        }
        private void textBox35_TextChanged_1(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                if(textBox35.Text.Length >= 8)
                {
                    SystemData.Pages.PrintMer.Select();
                }
                else
                {
                    dataGridView9.DataSource = null;
                }
            }
        }
        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
                SystemData.Pages.SelectPret.Select();
        }
        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
            panel22.Visible = checkBox4.Checked;
        }
        private void dateTimePicker17_ValueChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
        }
        private void dateTimePicker18_ValueChanged(object sender, EventArgs e)
        {
            textBox40_TextChanged(sender, e);
        }
        private void dateTimePicker12_ValueChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                SystemData.Pages.PrintPoc.Select();
            }
        }
        private void dateTimePicker13_ValueChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                SystemData.Pages.PrintPoc.Select();
            }
        }
        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                if (textBox37.Text.Length == 0 && textBox36.Text.Length >= 4)
                {
                    int code;

                    if (int.TryParse(textBox36.Text, out code))
                    {
                        var isp = IspModel.GetByCode(code);

                        if (isp != null)
                            textBox37.Text = isp.InizNotCode;
                    }
                }

                if (textBox36.Text.Length >= 4)
                {
                    SystemData.Pages.PrintCof.Select();
                }
                else
                {
                    SystemData.Pages.PrintCof.ClearTime();
                }
            }

        }
        private void dateTimePicker14_ValueChanged(object sender, EventArgs e)
        {
            textBox36_TextChanged(sender, e);
        }
        private void dateTimePicker15_ValueChanged(object sender, EventArgs e)
        {
            textBox36_TextChanged(sender, e);
        }
        #endregion

        #region Обработка курсора
        private void button2_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox1_KeyDown(sender, key);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox4_KeyDown(sender, key);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button1_PreviewKeyDown(sender, key);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker1_KeyDown(sender, key);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox26_KeyDown(sender, key);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox21_KeyDown(sender, key);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker4_KeyDown(sender, key);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button10_PreviewKeyDown(sender, key);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button11_PreviewKeyDown(sender, key);
        }
        private void button12_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button12_PreviewKeyDown(sender, key);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button6_PreviewKeyDown(sender, key);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button7_PreviewKeyDown(sender, key);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button5_PreviewKeyDown(sender, key);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button4_PreviewKeyDown(sender, key);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button13_PreviewKeyDown(sender, key);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            IspDataGridView_KeyDown(sender, key);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox13_KeyDown(sender, key);
        }
        private void button20_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            dataGridView3_KeyDown(sender, key);
        }
        private void button21_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox19_KeyDown(sender, key);
        }
        private void button16_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            dataGridView5_KeyDown(sender, key);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox32_KeyDown(sender, key);
        }
        private void button22_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            dataGridView1_KeyDown_1(sender, key);
        }
        private void button23_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox33_KeyDown(sender, key);
        }
        private void button24_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox29_KeyDown(sender, key);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox31_KeyDown(sender, key);
        }
        private void button26_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox34_KeyDown(sender, key);
        }
        private void IspDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            IspDataGridView_KeyDown(sender, key);
        }
        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            dataGridView3_KeyDown(sender, key);
        }
        private void button27_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker9_KeyDown(sender, key);
        }
        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            dataGridView5_KeyDown(sender, key);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            dataGridView1_KeyDown_1(sender, key);
        }
        private void button28_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker5_KeyDown(sender, key);
        }
        private void button29_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker6_KeyDown(sender, key);
        }
        private void button30_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker2_KeyDown(sender, key);
        }
        private void button31_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker3_KeyDown(sender, key);
        }
        private void button32_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker7_KeyDown(sender, key);
        }
        private void button33_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker8_KeyDown(sender, key);
        }
        private void button35_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker10_KeyDown(sender, key);
        }
        private void button36_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox30_KeyDown(sender, key);
        }
        private void button39_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker12_KeyDown(sender, key);
        }
        private void button43_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            textBox36_KeyDown(sender, key);
        }
        private void button41_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker14_KeyDown(sender, key);
        }
        private void button34_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button34_PreviewKeyDown(sender, key);
        }
        private void button37_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button37_PreviewKeyDown(sender, key);
        }
        private void button38_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button38_PreviewKeyDown(sender, key);
        }
        private void button40_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button40_PreviewKeyDown(sender, key);
        }
        private void button42_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button42_PreviewKeyDown(sender, key);
        }
        private void button49_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker16_KeyDown(sender, key);
        }
        private void button46_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button46_PreviewKeyDown(sender, key);
        }
        private void button45_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button45_PreviewKeyDown(sender, key);
        }
        private void button44_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button44_PreviewKeyDown(sender, key);
        }
        private void button53_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker17_KeyDown(sender, key);
        }
        private void button52_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(SystemData.ActionKey);
            dateTimePicker18_KeyDown(sender, key);
        }
        private void button54_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            dataGridView12_KeyDown(sender, key);
        }
        private void button51_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox40_KeyDown(sender, key);
        }
        #endregion

       
    }
}
