namespace UASKI.Models
{
    /// <summary>
    /// Абстрактный класс для объектов страницы
    /// </summary>
    public abstract class BasePageEdit : BasePage
    {
        public BasePageEdit(int index) : base(index) { }

        /// <summary>
        /// Выбраная строка на странице просмотра
        /// </summary>
        private int SelectedIndex = 0;

        /// <summary>
        /// Страница просмотра
        /// </summary>
        protected BasePageSelect Page;
        
        /// <summary>
        /// Выход со страницы
        /// </summary>
        protected override void Exit()
        {
            var d = Page.DataGridView;

            if (d.d.SelectedRows.Count > 0 && d.d.SelectedRows[0].Index != 0)
            {
                SelectedIndex = d.d.SelectedRows[0].Index;
            }

            Page.Init();

            if (d.d.Rows.Count > 0)
            {
                if (d.d.Rows.Count < SelectedIndex)
                {
                    d.d.Rows[d.d.Rows.Count - 1].Selected = true;
                    SelectedIndex = d.d.Rows.Count - 1;
                }
                else if(d.d.Rows.Count != SelectedIndex)
                {
                    d.d.Rows[SelectedIndex].Selected = true;
                }
                else 
                {
                    d.d.Rows[0].Selected = true;
                }

                if (!d.d.Rows[SelectedIndex].Displayed)
                {
                    d.d.FirstDisplayedScrollingRowIndex = SelectedIndex - d.d.DisplayedRowCount(false) + 2;
                }

                SelectedIndex = 0;
            }
        }
    }
}
