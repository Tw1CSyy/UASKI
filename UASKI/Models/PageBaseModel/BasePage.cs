using System.Drawing;
using System.Windows.Forms;
using UASKI.Enums;
using UASKI.Models.Components;
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
        /// Тип страницы
        /// </summary>
        public TypePage Type { get; private set; }

        /// <summary>
        /// Главная форма
        /// </summary>
        protected Gl_Form form { get => Ai.Form; }

        /// <summary>
        /// Отчищена ли форма
        /// </summary>
        public bool IsCleared = true;

        /// <summary>
        /// Загрузить данные на страницу
        /// </summary>
        protected abstract void Show();

        /// <summary>
        /// Очистить страницу
        /// </summary>
        protected abstract void Clear();

        /// <summary>
        /// DataGridView на странице или null
        /// </summary>
        public DataGridViewComponent DataGridView { get; protected set; }

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
        /// Очистить страницу
        /// </summary>
        protected void ClearPage()
        {
            Ai.IsClear = true;
            Clear();
            Ai.IsClear = false;
            IsCleared = true;
        }

        /// <summary>
        /// Переход на страницу и ее загрузка
        /// </summary>
        /// <param name="IsOpen">false - не открывать автоматически</param>
        /// <param name="IsClear">false - не отчищать предыдущую страницу</param>
        public void Init(bool IsOpen = true, bool IsClear = true)
        {
            var form = Ai.Form;
            form.Menu_Step2.Enabled = false;

            if (Ai.This != null && Ai.This.Index != this.Index && IsClear)
                Ai.This.ClearPage();
            
            Ai.AddHostoryPage(this);
            
            form.tabControl1.SelectedIndex = Index;

            if (IsOpen)
            {
                Ai.IsClear = true;
                Show();
                Ai.IsClear = false;
            }

            IsCleared = false;
        }

        /// <summary>
        /// Базовый конструктор для объявления объекта страницы
        /// </summary>
        /// <param name="index">Индекс страницы в tabControl</param>
        /// <param name="d">Компонент DataGridView страницы, если есть</param>
        public BasePage(int index, TypePage type, DataGridViewComponent d = null)
        {
            Index = index;
            DataGridView = d;
            Type = type;
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

        /// <summary>
        /// Открывает страницу с DataGridView, и подстраивает DataGridView под прошлый результат
        /// </summary>
        /// <returns>false - Страница не имеет DataGridView. Иначе true</returns>
        public bool ExitToDataGrid()
        {
            var d = DataGridView;

            if (d == null)
                return false;

            var SelectedIndex = 0;

            if (d.d.SelectedRows.Count > 0 && d.d.SelectedRows[0].Index != 0)
            {
                SelectedIndex = d.d.SelectedRows[0].Index;
            }

            Init();

            if (d.d.Rows.Count > 0)
            {
                try
                {
                    if (d.d.Rows.Count < SelectedIndex)
                    {
                        d.d.Rows[d.d.Rows.Count - 1].Selected = true;
                        SelectedIndex = d.d.Rows.Count - 1;
                    }
                    else if (d.d.Rows.Count != SelectedIndex)
                    {
                        d.d.Rows[SelectedIndex].Selected = true;
                    }
                    else
                    {
                        d.d.Rows[0].Selected = true;
                        SelectedIndex = 0;
                    }

                    if (!d.d.Rows[SelectedIndex].Displayed)
                    {
                        d.d.FirstDisplayedScrollingRowIndex = SelectedIndex - d.d.DisplayedRowCount(false) + 2;
                    }

                    SelectedIndex = 0;
                }
                catch (System.Exception)
                {
                    d.d.Rows[0].Selected = true;
                }
            }

            return true;
        }
    }
}
