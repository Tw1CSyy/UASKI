using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
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
        /// Стартовый метод
        /// </summary>
        private void Start()
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            TimeTimer.Start();

            // Инициализируем системные переменные
            SystemData.Init(this);
            

            // Рисуем меню

            foreach (var item in SystemData.MenuItems.Select(c => c.Text).ToArray())
            {
                Menu_Step1.Items.Add(item);
            }

            Menu_Step1.SelectedIndex = 0;
            Menu_Step1.Focus();

            // Отключаем отображение страниц
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

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
        }

        // При смене выбраного элемента меню 1го уровня меняем содержимое 2го меню
        private void Menu_Step1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Menu_Step1.SelectedIndex != -1)
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

        // Открываем страницы по меню
        private void Open()
        {
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(Menu_Step2.SelectedItem.ToString()));
            el.Page.Init(); 
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
        private void textBox30_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectTask.textBox30_KeyDown(e);
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
        private void textBox20_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectArhiv.textBox20_KeyDown(e);
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
        private void textBox35_KeyDown(object sender, KeyEventArgs e)
        {
            SystemData.Pages.SelectOpz.textBox35_KeyDown(e);
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
            button11.Enabled = (!SystemData.Pages.EditTask.Arhiv && textBox28.Text.Length > 0 && int.TryParse(textBox28.Text, out int i)) || SystemData.Pages.EditTask.Arhiv;
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if(!SystemData.IsClear)
            {
                var model = IspService.GetList();
                var search = textBox13.Text;

                model = model.Where(c => c.Code.ToString().Contains(search) ||
                    c.CodePodr.ToString().Contains(search) ||
                    c.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.Name.ToLower().Contains(search.ToLower()))
                    .ToList();

                SystemHelper.PullListInDataGridView(IspDataGridView
                            , IspService.GetListByDataGrid(model)
                            , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
            }
        }
        private void textBox19_TextChanged_1(object sender, EventArgs e)
        {
           if(!SystemData.IsClear)
            {
                var list = TasksService.GetList(textBox19.Text, textBox29.Text, textBox30.Text, checkBox2.Checked, dateTimePicker5.Value, dateTimePicker6.Value);

                SystemHelper.PullListInDataGridView(dataGridView3,
                            TasksService.GetListByDataGrid(list),
                            new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));
            }
        }
        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            textBox19_TextChanged_1(sender, e);
        }
        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            if (!SystemData.IsClear)
            {
                var list = ArhivService.GetList(textBox32.Text, textBox31.Text, textBox20.Text, checkBox1.Checked, dateTimePicker2.Value, dateTimePicker3.Value);

                SystemHelper.PullListInDataGridView(dataGridView5,
                    ArhivService.GetListByDataGrid(list),
                    new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка"));
            }
        }
        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void textBox20_TextChanged_1(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox32_TextChanged(sender, e);
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
            textBox19_TextChanged_1(sender, e);
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
                var form = this;

                SystemHelper.PullListInDataGridView(form.dataGridView1,
                    ArhivService.GetOpzListDataGrid(form.textBox33.Text, form.textBox34.Text, form.textBox35.Text, form.checkBox3.Checked, form.dateTimePicker7.Value, form.dateTimePicker8.Value),
                    new DataGridRowModel("Код", "Исполнитель", "Котроллер", "Срок", "Дата закрытия", "Оценка"));
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
            textBox33_TextChanged(sender, e);
        }
        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
        }
        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            textBox33_TextChanged(sender, e);
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
        #endregion

    }
}
