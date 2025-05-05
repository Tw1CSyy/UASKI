using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using UASKI.Models.Components;
using UASKI.Core.Models;
using UASKI.Core;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UASKI.Core.SystemModels;

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
            TimeTimer.Start();
            
            // Инициализируем системные переменные
            ComponentIniz();
            Ai.Init(this);
            Ai.Iniz(textBox41);
            
            // Рисуем меню
            foreach (var item in Ai.MenuItems.Select(c => c.Text).ToArray())
                Menu_Step1.Items.Add(item);

            ResizeEl();
            textBox41.Clear();
            Menu_Step1.SelectedIndex = 0;
            Menu_Step1.Focus();

            // Отключаем отображение страниц
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            Ai.AddWaitMessage(Enums.TypeNotice.Default, "Включаю приложение. Удачной работы!");
            Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}");

            // Создаем или загружаем файл настроек
            if (!ApplicationHelper.Settings())
            {
                return false;
            }

            // Открываем подключение
            var settings = Ai.Settings;
            string connectionString = $"Host={settings.Host};UserName={settings.User};Password={settings.Password};Database={settings.DateBase};Port={settings.Port}; SSL Mode=Require;";
            var connection = new DataConnection(connectionString);

            if(!connection.IsConnection)
            {
                Ai.AddWaitMessage(Enums.TypeNotice.Error, "Ошибка при подключении к базе данных");
                return false;
            }

            Ai.AddWaitMessage(Enums.TypeNotice.Default, "Подключение с базой получено");

            // Собираем статистику
            var tasks = TaskModel.GetList();
            var arhiv = ArhivModel.GetList();
            var holys = HolidayModel.GetList();
            var prets = PretModel.GetList();

            var count = tasks.Count(c => c.Date == DateTime.Today);

            if(count != 0)
                Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Сегодня должны закрыться карточки: {count}");
            else
                Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Сегодня нет карточек на закрытие");

            count = tasks.Count(c => c.Date < DateTime.Today);

            if (count != 0)
                Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Опаздывающих на текущий момент: {count}");
            else
                Ai.AddWaitMessage(Enums.TypeNotice.Default, $"На текущий момент никто не опаздывает");

            // Коэф. качества за месяц
            var kofs = new List<KofModel>();
             
            foreach (var isp in IspModel.GetList())
            {
                var item = isp.GetKofModel(tasks, arhiv, holys, prets, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);

                if (item.Kof <= 0 && item.Count == 0)
                    continue;

                if (item.Kof < 0)
                    item.Kof = 0;

                kofs.Add(item);
            }

            
            var kofsGroup = kofs
                .GroupBy(c => c.Kof)
                .OrderByDescending(c => c.Key);

            Ai.AddWaitMessage(Enums.TypeNotice.Default, "Показатели работы:");

            foreach (var item in kofsGroup)
            {
                Ai.AddWaitMessage(Enums.TypeNotice.Default, $"{item.Key.ToString()} - {item.Count()}");
            }

            // Если сегодня понедельник - создаем даты
            if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
            {
                ApplicationHelper.AddHoliday();
            }

            var minDate = holys.Min(c => c.Date).Date;
            var maxDate = holys.Max(c => c.Date).Date;
            Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Праздничные дни в диапазоне: {minDate.ToString("dd.MM.yyyy")} - {maxDate.ToString("dd.MM.yyyy")}");

            Ai.AddWaitMessage(Enums.TypeNotice.Default, $"Получить информацию о работе клавишах: CTRL + Ш");
            Menu_Step1.Visible = Menu_Step2.Visible = true;
            return true;
        }

        #region Системное

        // При смене выбраного элемента меню 1го уровня меняем содержимое 2го меню
        private void Menu_Step1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Menu_Step1.SelectedIndex != -1)
            {
                var text = Menu_Step1.Items[Menu_Step1.SelectedIndex].ToString();
                var items = Ai.MenuItems.FirstOrDefault(c => c.Text.Equals(text));
                Menu_Step2.Items.Clear();

                foreach (var item in items.Items.Select(c => c.Text).ToArray())
                {
                    Menu_Step2.Items.Add(item);
                }
            }
        }

        // Таймер времени
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            if(DateTime.Now.Minute == 0)
            {
                Ai.AddMessage(Enums.TypeNotice.Default, "Статистика за текущий день:");
                Ai.AddMessage(Enums.TypeNotice.Default, $"Добавлено карт: {Ai.Settings.CountAdd}");
                Ai.AddMessage(Enums.TypeNotice.Default, $"Закрыто карт: {Ai.Settings.CountClose}");
                Ai.AddMessage(Enums.TypeNotice.Default, $"Распечатано: {Ai.Settings.CountPrint}");
            }

            if(DateTime.Now.Hour == 11 && DateTime.Now.Minute == 30)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Скоро обед. Осталось чуть чуть");
            }
            else if (DateTime.Now.Hour == 16 && DateTime.Now.Minute == 10)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Скоро домой!!!");
            }
            else if(DateTime.Today.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Hour == 14 && DateTime.Now.Minute == 23)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Завтра выходной! Удачи отдохнуть");
            }
            else if(DateTime.Now.Hour == 9 && DateTime.Now.Minute == 43)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "И не лень тебе в такую рань работать");
            }
            else if (DateTime.Now.Hour == 11 && DateTime.Now.Minute == 13)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Вспомни про котиков и станет легче");
            }
            else if (DateTime.Now.Hour == 15 && DateTime.Now.Minute == 30)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Домой через час, пора заканчивать");
            }
            else if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 30)
            {
                Ai.AddMessage(Enums.TypeNotice.Comlite, "Пора выпить кофейка");
            }
        }

        // Открываем страницы по меню
        private void Open()
        {
            var elem = Ai.MenuItems.FirstOrDefault(c => c.Text.Equals(Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(Menu_Step2.SelectedItem.ToString()));

            if (el.Page != null && !Ai.IsClear)
                el.Page.Init();
        }

        // Подстраиваем размер компонентов под расмер формы
        private void ResizeEl()
        {
            // Основное и главная страница
            textBox41.Location = new System.Drawing.Point(this.Width - textBox41.Width - 15, textBox41.Location.Y);
            textBox41.Height = this.Height - 40;
            Menu_Step1.Width = Menu_Step2.Width = panel3.Width = panel4.Width = (this.Width - textBox41.Width - 15) / 2;
            Menu_Step2.Location = new System.Drawing.Point(Menu_Step1.Width + Menu_Step1.Location.X, Menu_Step2.Location.Y);
            panel3.Location = Menu_Step2.Location;
            tabControl1.Width = this.Width - 375;
            tabControl1.Height = this.Height - 192;
            pictureBox1.Location = new System.Drawing.Point((tabControl1.Width / 2) - (pictureBox1.Width / 2), (tabControl1.Height / 2) - (pictureBox1.Height / 2));
            // Просмотр

            ispDataGridView.Width = dataGridView3.Width = dataGridView1.Width = dataGridView5.Width = dataGridView12.Width = dataGridView15.Width = tabControl1.Width - 27;
            ispDataGridView.Height = dataGridView3.Height = dataGridView1.Height = dataGridView5.Height = dataGridView12.Height = dataGridView15.Height = tabControl1.Height - 22;

            dataGridView6.Width = tabControl1.Width - 184;
            dataGridView6.Height = tabControl1.Height - 26;
            button13.Location = new System.Drawing.Point(tabControl1.Width - 178, button13.Location.Y);
            monthCalendar2.Location = new System.Drawing.Point(tabControl1.Width - 178, monthCalendar2.Location.Y);

            // Редактирование
            dataGridView4.Width = tabControl1.Width - 11;
            dataGridView4.Height = tabControl1.Height - 365;
            
            //Печатные
            panel23.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel23.Width / 2), panel23.Location.Y);
            dataGridView7.Width = tabControl1.Width - 10;
            dataGridView7.Height = tabControl1.Height - 110;
            
            panel24.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel24.Width / 2), panel24.Location.Y);
            dataGridView8.Width = tabControl1.Width - 8;
            dataGridView8.Height = tabControl1.Height - 110;
            
            panel25.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel25.Width / 2), panel25.Location.Y);
            dataGridView9.Width = tabControl1.Width - 8;
            dataGridView9.Height = tabControl1.Height - 124;
            DataGridView9.ResizeDataGridView();

            panel26.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel26.Width / 2), panel26.Location.Y);
            dataGridView10.Width = tabControl1.Width - 8;
            dataGridView10.Height = tabControl1.Height - 110;

            panel29.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel26.Width / 2), panel26.Location.Y);
            dataGridView14.Width = tabControl1.Width - 8;
            dataGridView14.Height = tabControl1.Height - 110;

            panel27.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel27.Width / 2), panel27.Location.Y);
            dataGridView11.Width = tabControl1.Width - 8;
            dataGridView11.Height = tabControl1.Height - 322;
            panel28.Location = new System.Drawing.Point((tabControl1.Width / 2) - (panel28.Width / 2), dataGridView11.Height + dataGridView11.Location.Y + 5);
            dataGridView13.Width = tabControl1.Width - 8;
            dataGridView13.Location = new System.Drawing.Point(dataGridView13.Location.X, panel28.Location.Y + panel28.Height + 5);

            IspDataGridView.ResizeDataGridView();
            DataGridView1.ResizeDataGridView();
            DataGridView2.ResizeDataGridView();
            DataGridView3.ResizeDataGridView();
            DataGridView4.ResizeDataGridView();
            DataGridView5.ResizeDataGridView();
            DataGridView6.ResizeDataGridView();
            DataGridView7.ResizeDataGridView();
            DataGridView8.ResizeDataGridView();
            DataGridView9.ResizeDataGridView();
            DataGridView10.ResizeDataGridView();
            DataGridView11.ResizeDataGridView();
            DataGridView12.ResizeDataGridView();
            DataGridView13.ResizeDataGridView();
            DataGridView14.ResizeDataGridView();

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
            DataGridView14 = new DataGridViewComponent(dataGridView14);
            DataGridView15 = new DataGridViewComponent(dataGridView15);
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
        public DataGridViewComponent DataGridView14 { get; private set; }
        public DataGridViewComponent DataGridView15 { get; private set; }
        #endregion

        #region Нажатия клавиш

        private void Gl_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if(Menu_Step1.Visible && Menu_Step2.Visible)
            {
                var result = Ai.KeyDown(e);
                 
                if (result)
                    e.Handled = true;
            }
        }
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
        public void Menu_Step2_KeyDown(object sender, KeyEventArgs e)
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
                Menu_Step1.Enabled = false;
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
                Menu_Step2_KeyDown(sender, r);
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
        private void dateTimePicker19_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintOpz.dateTimePicker19_KeyDown(e);
        }
        private void IspDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectIsp.IspDataGridView_KeyDown(e);
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddTask.textBox1_KeyDown(e);
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddTask.textBox4_KeyDown(e);
        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddTask.textBox7_KeyDown(e);
        }
        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.AddTask.button1_KeyDown(e);
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddTask.dateTimePicker1_KeyDown(e);
        }
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddIsp.textBox8_KeyDown(e);
        }
        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddIsp.textBox9_KeyDown(e);
        }
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddIsp.textBox10_KeyDown(e);
        }
        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddIsp.textBox11_KeyDown(e);
        }
        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddIsp.textBox12_KeyDown(e);
        }
        private void checkBox8_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.AddTask.checkBox8_PreviewKeyDown(e);
        }
        private void button4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.AddIsp.button4_PreviewKeyDown(e);
        }
        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.AddHoliday.monthCalendar1_KeyDown(e);
        }
        private void button5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.AddHoliday.button5_PreviewKeyDown(e);
        }
        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.textBox18_KeyDown(e);
        }
        private void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.textBox17_KeyDown(e);
        }
        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.textBox16_KeyDown(e);
        }
        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.textBox15_KeyDown(e);
        }
        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.textBox14_KeyDown(e);
        }
        private void button6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditIsp.button6_KeyDown(e);
        }
        private void button7_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditIsp.button7_KeyDown(e);
        }
        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectTask.dataGridView3_KeyDown(e);
        }
        private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditIsp.dataGridView4_KeyDown(e);
        }
        private void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.textBox26_KeyDown(e);
        }
        private void textBox21_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.textBox21_KeyDown(e);
        }
        private void textBox27_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.textBox27_KeyDown(e);
        }
        private void dateTimePicker4_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.dateTimePicker4_KeyDown(e);
        }
        private void textBox28_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.textBox28_KeyDown(e);
        }
        private void button10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.button10_PreviewKeyDown(e);
        }
        private void button11_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.button11_PreviewKeyDown(e);
        }
        private void button12_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.button12_PreviewKeyDown(e);
        }
        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectIsp.textBox13_KeyDown(e);
        }
        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectTask.textBox19_KeyDown(e);
        }
        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectTask.textBox29_KeyDown(e);
        }
        private void textBox32_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectArhiv.textBox32_KeyDown(e);
        }
        private void dateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectArhiv.dateTimePicker3_KeyDown(e);
        }
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectArhiv.dateTimePicker2_KeyDown(e);
        }
        private void textBox31_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectArhiv.textBox31_KeyDown(e);
        }
        private void checkBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectArhiv.checkBox1_PreviewKeyDown(e);
        }
        private void dataGridView5_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectArhiv.dataGridView5_KeyDown(e);
        }
        private void checkBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectTask.checkBox2_PreviewKeyDown(e);
        }
        private void dateTimePicker5_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectTask.dateTimePicker5_KeyDown(e);
        }
        private void dateTimePicker6_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectTask.dateTimePicker6_KeyDown(e);
        }
        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectOpz.dataGridView1_KeyDown_1(e);
        }
        private void textBox33_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectOpz.textBox33_KeyDown(e);
        }
        private void textBox34_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectOpz.textBox34_KeyDown(e);
        }
        private void checkBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectOpz.checkBox3_PreviewKeyDown(e);
        }
        private void dateTimePicker7_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectOpz.dateTimePicker7_KeyDown(e);
        }
        private void dateTimePicker8_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectOpz.dateTimePicker8_KeyDown(e);
        }
        private void dataGridView6_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectHoliday.dataGridView6_KeyDown(e);
        }
        private void button13_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectHoliday.button13_PreviewKeyDown(e);
        }
        private void dateTimePicker9_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditTask.dateTimePicker9_KeyDown(e);
        }
        private void dateTimePicker10_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintTaskList.dateTimePicker10_KeyDown(e);
        }
        private void dateTimePicker11_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintTaskList.dateTimePicker11_KeyDown(e);
        }
        private void textBox30_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintTaskList.textBox30_KeyDown(e);
        }
        private void button34_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintTaskList.button34_PreviewKeyDown(e);
        }
        private void dataGridView7_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintTaskList.dataGridView7_KeyDown(e);
        }
        private void button37_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintOpz.button37_PreviewKeyDown(e);
        }
        private void dataGridView8_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintOpz.dataGridView8_KeyDown(e);
        }
        private void textBox35_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintMer.textBox35_KeyDown(e);
        }
        private void dataGridView9_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintMer.dataGridView9_KeyDown(e);
        }
        private void button38_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintMer.button38_PreviewKeyDown(e);
        }
        private void dateTimePicker12_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPoc.dateTimePicker12_KeyDown(e);
        }
        private void dateTimePicker13_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPoc.dateTimePicker13_KeyDown(e);
        }
        private void button40_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintPoc.button40_PreviewKeyDown(e);
        }
        private void dataGridView10_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPoc.dataGridView10_KeyDown(e);
        }
        private void textBox36_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintCof.textBox36_KeyDown(e);
        }
        private void dateTimePicker14_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintCof.dateTimePicker14_KeyDown(e);
        }
        private void dateTimePicker15_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintCof.dateTimePicker15_KeyDown(e);
        }
        private void button42_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintCof.button42_PreviewKeyDown(e);
        }
        private void dataGridView11_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintCof.dataGridView11_KeyDown(e);
        }
        private void button47_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.button47_PreviewKeyDown(e);
        }
        private void button48_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.button48_PreviewKeyDown(e);
        }
        private void textBox38_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditPret.textBox38_KeyDown(e);
        }
        private void dateTimePicker16_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditPret.dateTimePicker16_KeyDown(e);
        }
        private void textBox39_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.EditPret.textBox39_KeyDown(e);
        }
        private void button46_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditPret.button46_PreviewKeyDown(e);
        }
        private void button45_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditPret.button45_PreviewKeyDown(e);
        }
        private void checkBox10_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectTask.checkBox10_PreviewKeyDown(e);
        }
        private void checkBox11_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectArhiv.checkBox11_PreviewKeyDown(e);
        }
        private void checkBox12_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectOpz.checkBox12_PreviewKeyDown(e);
        }
        private void button44_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditPret.button44_PreviewKeyDown(e);
        }
        private void dataGridView12_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectPret.dataGridView12_KeyDown(e);
        }
        private void textBox40_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectPret.textBox40_KeyDown(e);
        }
        private void checkBox5_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectPret.checkBox5_PreviewKeyDown(e);
        }
        private void checkBox6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectPret.checkBox6_PreviewKeyDown(e);
        }
        private void checkBox4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectPret.checkBox4_PreviewKeyDown(e);
        }
        private void dateTimePicker17_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectPret.dateTimePicker17_KeyDown(e);
        }
        private void dateTimePicker18_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectPret.dateTimePicker18_KeyDown(e);
        }
        private void dataGridView13_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintCof.dataGridView13_KeyDown(e);
        }
        private void dateTimePicker20_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPlan.dateTimePicker20_KeyDown(e);
        }
        private void dateTimePicker21_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPlan.dateTimePicker21_KeyDown(e);
        }
        private void textBox42_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPlan.textBox42_KeyDown(e);
        }
        private void button56_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.PrintPlan.button56_PreviewKeyDown(e);
        }
        private void dataGridView14_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.PrintPlan.dataGridView14_KeyDown(e);
        }
        private void checkBox7_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.EditTask.checkBox7_PreviewKeyDown(e);
        }
        private void dataGridView15_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectDTasks.dataGridView15_KeyDown(e);
        }
        private void textBox44_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectDTasks.textBox44_KeyDown(e);
        }
        private void checkBox9_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Ai.Pages.SelectDTasks.checkBox9_PreviewKeyDown(e);
        }
        private void dateTimePicker22_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectDTasks.dateTimePicker22_KeyDown(e);
        }
        private void dateTimePicker23_KeyDown(object sender, KeyEventArgs e)
        {
            Ai.Pages.SelectDTasks.dateTimePicker23_KeyDown(e);
        }
        #endregion

        #region Обработка
        private void Gl_Form_Resize(object sender, EventArgs e)
        {
            ResizeEl();
        }
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
            if(!Ai.IsClear)
            button11.Enabled = (!Ai.Pages.EditTask.IsArhiv && textBox28.Text.Length > 0 && int.TryParse(textBox28.Text, out int i)) || Ai.Pages.EditTask.IsArhiv;
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
            {
                Ai.Pages.SelectIsp.Select();
            }
        }
        private void textBox19_TextChanged_1(object sender, EventArgs e)
        {
           if(!Ai.IsClear)
           {
                Ai.Pages.SelectTask.Select();
           }
        }
        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
            {
                Ai.Pages.SelectArhiv.Select();
            }
        }
        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
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
        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count != 0)
            {
                var date = Convert.ToDateTime(dataGridView6.SelectedRows[0].Cells[1].Value);
                monthCalendar2.SelectionStart = date;
                monthCalendar2.SelectionEnd = date;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
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
            if(!Ai.IsClear)
            {
                Ai.Pages.SelectOpz.Select();
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
            if(!Ai.IsClear)
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
            if (!Ai.IsClear)
            {
                if (textBox30.Text.Length > 0)
                {
                    int code;

                    if (int.TryParse(textBox30.Text, out code))
                    {
                        var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == code);

                        if (isp != null)
                            textBox20.Text = isp.InizNotCode;
                        else
                            textBox20.Clear();
                    }
                    else
                        textBox20.Clear();
                }

                if (textBox30.Text.Length > 0)
                {
                    Ai.Pages.PrintTaskList.Select();
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
            textBox30_TextChanged(sender, e);
        }
        private void dateTimePicker19_ValueChanged(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
                Ai.Pages.PrintOpz.Select();
        }
        private void dateTimePicker11_ValueChanged(object sender, EventArgs e)
        {
            textBox30_TextChanged(sender, e);
        }
        private void textBox35_TextChanged_1(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
            {
                if(textBox35.Text.Length >= 8)
                {
                    Ai.Pages.PrintMer.Select();
                }
                else
                {
                    dataGridView9.DataSource = null;
                }
            }
        }
        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
                Ai.Pages.SelectPret.Select();
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
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
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
            if (!Ai.IsClear)
            {
                Ai.Pages.PrintPoc.Select();
            }
        }
        private void dateTimePicker13_ValueChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
            {
                Ai.Pages.PrintPoc.Select();
            }
        }
        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
            {
                if (textBox36.Text.Length > 0)
                {
                    int code;

                    if (int.TryParse(textBox36.Text, out code))
                    {
                        var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == code);

                        if (isp != null)
                            textBox37.Text = isp.InizNotCode;
                        else
                            textBox37.Clear();
                    }
                    else
                        textBox37.Clear();
                }

                if (textBox36.Text.Length > 0)
                {
                    Ai.Pages.PrintCof.Select();
                }
                else
                {
                    Ai.Pages.PrintCof.ClearTime();
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
        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if(!Ai.IsClear)
            {
                if(dateTimePicker4.Value.DayOfWeek == DayOfWeek.Sunday || dateTimePicker4.Value.DayOfWeek == DayOfWeek.Saturday)
                {
                    Ai.Warning("Предупреждение: Выбранная дата это выходной день");
                }
            }
        }
        private void dateTimePicker9_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker9.Value.DayOfWeek == DayOfWeek.Sunday || dateTimePicker9.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                Ai.Warning("Предупреждение: Выбранная дата это выходной день");
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday || dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                Ai.Warning("Предупреждение: Выбранная дата это выходной день");
            }
        }
        private void dateTimePicker16_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker16.Value.DayOfWeek == DayOfWeek.Sunday || dateTimePicker16.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                Ai.Warning("Предупреждение: Выбранная дата это выходной день");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && int.TryParse(textBox1.Text, out int podr) && !Ai.IsClear)
            {
                var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == podr);

                if (isp != null)
                {
                    textBox3.Text = isp.Code.ToString();
                    textBox2.Text = isp.InizNotCode;

                }
                else
                {
                    textBox3.Clear();
                    textBox2.Clear();
                }
            }
            else
            {
                textBox3.Clear();
                textBox2.Clear();
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 0 && int.TryParse(textBox4.Text, out int podr) && !Ai.IsClear)
            {
                var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == podr);

                if (isp != null)
                {
                    textBox6.Text = isp.Code.ToString();
                    textBox5.Text = isp.InizNotCode;
                }
                else
                {
                    textBox5.Clear();
                    textBox6.Clear();
                }
            }
            else
            {
                textBox5.Clear();
                textBox6.Clear();
            }
        }
        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox26.Text, out int podr))
            {
                var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == podr);

                if (isp != null)
                {
                    textBox24.Text = isp.Code.ToString();
                    textBox25.Text = isp.InizNotCode;
                }
                else
                {
                    textBox25.Clear();
                    textBox24.Clear();
                }
            }
            else
            {
                textBox25.Clear();
                textBox24.Clear();
            }
        }
        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox21.Text, out int podr))
            {
                var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == podr);

                if (isp != null)
                {
                    textBox22.Text = isp.InizNotCode;
                    textBox23.Text = isp.Code.ToString();
                }
                else
                {
                    textBox22.Clear();
                    textBox23.Clear();
                }
            }
            else
            {
                textBox22.Clear();
                textBox23.Clear();
            }
        }
        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
            {
                if (textBox42.Text.Length > 0)
                {
                    int code;

                    if (int.TryParse(textBox42.Text, out code))
                    {
                        var isp = IspModel.GetList().FirstOrDefault(c => c.CodePodr == code);

                        if (isp != null)
                            textBox43.Text = isp.InizNotCode;
                        else
                            textBox43.Clear();
                    }
                    else
                        textBox43.Clear();
                }

                if (textBox43.Text.Length > 0)
                {
                    Ai.Pages.PrintPlan.Select();
                }
                else
                {
                    dataGridView14.DataSource = null;
                    textBox43.Clear();
                }
            }
        }
        private void dateTimePicker20_ValueChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear && textBox43.Text.Length > 0)
                Ai.Pages.PrintPlan.Select();
        }
        private void dateTimePicker21_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker20_ValueChanged(sender, e);
        }
        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            if (!Ai.IsClear)
                Ai.Pages.SelectDTasks.Select();
        }
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            panel31.Visible = checkBox9.Checked;
            textBox44_TextChanged(sender, e);
        }
        private void dateTimePicker22_ValueChanged(object sender, EventArgs e)
        {
            textBox44_TextChanged(sender, e);
        }
        private void dateTimePicker23_ValueChanged(object sender, EventArgs e)
        {
            textBox44_TextChanged(sender, e);
        }
        #endregion

        #region Обработка курсора
        private void button2_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox1_KeyDown(sender, key);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox4_KeyDown(sender, key);
        }
        private void button56_Click(object sender, EventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button56_PreviewKeyDown(sender, key);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker1_KeyDown(sender, key);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox26_KeyDown(sender, key);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox21_KeyDown(sender, key);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker4_KeyDown(sender, key);
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
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox29_KeyDown(sender, key);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox31_KeyDown(sender, key);
        }
        private void button26_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
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
            var key = new KeyEventArgs(Ai.ActionKey);
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
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker5_KeyDown(sender, key);
        }
        private void button29_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker6_KeyDown(sender, key);
        }
        private void button30_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker2_KeyDown(sender, key);
        }
        private void button31_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker3_KeyDown(sender, key);
        }
        private void button32_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker7_KeyDown(sender, key);
        }
        private void button33_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker8_KeyDown(sender, key);
        }
        private void button36_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox30_KeyDown(sender, key);
        }
        private void button43_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox36_KeyDown(sender, key);
        }
        private void button57_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Left);
            dataGridView15_KeyDown(sender, key);
        }
        private void button59_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Keys.Right);
            textBox44_KeyDown(sender, key);
        }
        private void button61_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker22_KeyDown(sender, key);
        }
        private void button60_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker23_KeyDown(sender, key);
        }
        private void button50_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            textBox42_KeyDown(sender, key);
        }
        private void button53_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
            dateTimePicker17_KeyDown(sender, key);
        }
        private void button52_Click(object sender, EventArgs e)
        {
            var key = new KeyEventArgs(Ai.ActionKey);
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
        private void button13_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button13_PreviewKeyDown(sender, key);
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button1_PreviewKeyDown(sender, key);
        }
        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button5_PreviewKeyDown(sender, key);
        }
        private void button6_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button6_PreviewKeyDown(sender, key);
        }
        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button7_PreviewKeyDown(sender, key);
        }
        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button4_PreviewKeyDown(sender, key);
        }
        private void button10_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button10_PreviewKeyDown(sender, key);
        }
        private void button11_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button11_PreviewKeyDown(sender, key);
        }
        private void button12_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button12_PreviewKeyDown(sender, key);
        }
        private void button47_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button47_PreviewKeyDown(sender, key);
        }
        private void button48_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button48_PreviewKeyDown(sender, key);
        }
        private void button34_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button34_PreviewKeyDown(sender, key);
        }
        private void button37_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button37_PreviewKeyDown(sender, key);
        }
        private void button38_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button38_PreviewKeyDown(sender, key);
        }
        private void button40_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button40_PreviewKeyDown(sender, key);
        }
        private void button42_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button42_PreviewKeyDown(sender, key);
        }
        private void button46_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button46_PreviewKeyDown(sender, key);
        }
        private void button45_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button45_PreviewKeyDown(sender, key);
        }
        private void button44_MouseClick(object sender, MouseEventArgs e)
        {
            var key = new PreviewKeyDownEventArgs(Keys.Enter);
            button44_PreviewKeyDown(sender, key);
        }

        #endregion

    }
}
