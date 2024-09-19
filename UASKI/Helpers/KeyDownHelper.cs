using System.Windows.Forms;
using UASKI.StaticModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для отбработки нажатий клавиш на форме
    /// </summary>
    public static class KeyDownHelper
    {
        private static readonly Gl_Form form = SystemData.Form;

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
                form.Menu_Step2.Focus();
                form.Menu_Step2.SelectedIndex = 0;
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
                form.Menu_Step1.Focus();
                form.Menu_Step2.ClearSelected();
            }
            else if(e.KeyCode == Keys.Enter)
            {

            }
        }


    }
}
