using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Services;
using UASKI.StaticModels;

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
        /// Устанавливает значение индекса выбраной строки на DataGridView на странице просмотра
        /// </summary>
        void PullDataGridViewSelectedIndex()
        {
            var d = Page.DataGridView;

            if(d.SelectedRows.Count > 0 && d.SelectedRows[0].Index != 0)
            {
                SelectedIndex = d.SelectedRows[0].Index;
            }
        }

        /// <summary>
        /// Устанавливает индекс выбранной строки в DataGridView
        /// </summary>
        void PushDataGridViewSelect()
        {
            var d = Page.DataGridView;

            if(d.Rows.Count > 0)
            {
                if(d.Rows.Count >= SelectedIndex)
                    d.Rows[d.Rows.Count - 1].Selected = true;
                else
                    d.Rows[SelectedIndex].Selected = true;

                SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Выход со страницы
        /// </summary>
        protected override void Exit()
        {
            PullDataGridViewSelectedIndex();
            Page.Init();
            PushDataGridViewSelect();
        }

    }
}
