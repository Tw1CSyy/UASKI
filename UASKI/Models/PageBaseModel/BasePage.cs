using System.Drawing;
using System.Windows.Forms;
using UASKI.StaticModels;

namespace UASKI.Models
{
    /// <summary>
    /// Абстрактный класс для объектов страницы
    /// </summary>
    public abstract class BasePage
    {
        /// <summary>
        /// Индекс страницы
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Главная форма
        /// </summary>
        protected Gl_Form form { get => SystemData.Form; }

        /// <summary>
        /// Отчищена ли форма
        /// </summary>
        protected bool IsCleared = true;

        /// <summary>
        /// Загрузить данные на страницу
        /// </summary>
        protected abstract void Show();

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        protected abstract void Clear();

        /// <summary>
        /// Выход с страницы
        /// </summary>
        protected abstract void Exit();

        /// <summary>
        /// Обработка клавиш AI
        /// </summary>
        /// <returns></returns>
        public abstract bool AiKeyDown(KeyEventArgs key);

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        protected void ClearPage()
        {
            SystemData.IsClear = true;
            Clear();
            SystemData.IsClear = false;
            IsCleared = true;
        }

        /// <summary>
        /// Переход на страницу и ее загрузка
        /// </summary>
        /// <param name="IsOpen">false - не открывать автоматически</param>
        /// <param name="IsClear">false - не отчищать предыдущую страницу</param>
        public void Init(bool IsOpen = true , bool IsClear = true)
        {
            var form = SystemData.Form;
            form.Menu_Step2.Enabled = false;

            if (SystemData.This != null && SystemData.This.Index != this.Index && IsClear)
                SystemData.This.ClearPage();
            else if(SystemData.This == null)
                ClearPage();

            SystemData.This = this;
            
            form.tabControl1.SelectedIndex = Index;

            if (IsOpen)
            {
                SystemData.IsClear = true;
                Show();
                SystemData.IsClear = false;
            }

            IsCleared = false;
        }

        /// <summary>
        /// Базовый конструктор для объявления объекта страницы
        /// </summary>
        /// <param name="index"></param>
        public BasePage(int index)
        {
            Index = index;
        }

        /// <summary>
        /// Включает и меняет фон кнопки для отображения выделения
        /// </summary>
        /// <param name="btn">Button для выделения</param>
        /// <param name="selected">Вкл/Выкл</param>
        protected static void SelectButton(Button btn, bool selected = true)
        {
            if (selected)
            {
                btn.BackColor = Color.LightBlue;
                btn.Focus();
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Выбирает DataGridView
        /// </summary>
        /// <param name="d">DataGridView для выделения</param>
        /// <param name="selected">Вкл/Выкл</param>
        protected bool SelectDataGridView(DataGridView d, bool selected = true)
        {
            if (selected)
            {
                if (d.Rows.Count > 0)
                {
                    d.Rows[0].Selected = true;
                    d.Focus();
                    return true;
                }

                return false;
            }
            else
            {
                d.ClearSelection();
                return true;
            }
        }

        /// <summary>
        /// Выбирает текст бокс и переводит курсор
        /// </summary>
        /// <param name="t">TextBox для выделения</param>
        protected static void SelectTextBox(TextBox t)
        {
            t.Focus();
            t.SelectionStart = t.Text.Length;
        }

        /// <summary>
        /// Выбирает и веделяет CheckBox
        /// </summary>
        /// <param name="c">CheckBox для выделения</param>
        /// <param name="selected"></param>
        protected static void SelectCheckBox(CheckBox c, bool selected = true)
        {
            if (selected)
            {
                c.BackColor = Color.LightBlue;
                c.Focus();
            }
            else
            {
                c.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

    }
}
