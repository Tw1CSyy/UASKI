namespace UASKI.Models
{
    public abstract class BasePagePrint : BasePage
    {
        public BasePagePrint(int index) : base(index) { }

        /// <summary>
        /// Вывести данные в DataGridView
        /// </summary>
        public abstract void Select();

        /// <summary>
        /// Напечатать данные
        /// </summary>
        protected abstract void Print();
    }
}
