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
        /// Загрузить данные на страницу
        /// </summary>
        protected abstract void Show();

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        protected abstract void Clear();

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        public void ClearPage()
        {
            SystemData.IsClear = true;
            Clear();
            SystemData.IsClear = false;
        }

        /// <summary>
        /// Переход на страницу и ее загрузка
        /// </summary>
        /// <param name="IsOpen">false - не открывать автоматически</param>
        public void Init(bool IsOpen = true)
        {
            var form = SystemData.Form;
            form.tabControl1.SelectedIndex = Index;
            form.Menu_Step2.Enabled = false;
            SystemData.Pages.Clear();
            SystemData.Index = Index;

            if(IsOpen)
            {
                SystemData.IsClear = true;
                Show();
                SystemData.IsClear = false;
            }
        }

        /// <summary>
        /// Базовый конструктор для объявления объекта страницы
        /// </summary>
        /// <param name="index"></param>
        public BasePage(int index)
        {
            Index = index;
        }

    }
}
