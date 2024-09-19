using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.StaticModels;
using UASKI.Data.Context;


namespace UASKI
{
    public partial class Gl_Form : Form
    {
        public Gl_Form()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            // Рисуем меню
            SystemHelper.WriteListBox(Menu_Step1, SystemData.MenuItems.Select(c => c.Text).ToArray());
            Menu_Step1.SelectedIndex = 0;
            Menu_Step1.Focus();

            // Отключаем отображение страниц
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            SystemData.Form = this;
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

        #region Нажатия клавиш
        private void Menu_Step1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.Menu_Step1_KeyDown(e);
        }

        private void Menu_Step2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.Menu_Step2_KeyDown(e);
        }
        #endregion

    }
}
