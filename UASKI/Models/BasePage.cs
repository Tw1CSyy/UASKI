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
        public abstract void Clear();

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
                Show();
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
