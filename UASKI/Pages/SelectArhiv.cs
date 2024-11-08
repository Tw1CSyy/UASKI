using System;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра архива заданий
    /// </summary>
    public class SelectArhiv : BasePage
    {
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public SelectArhiv(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker3.Value = DateTime.Today;

            SystemHelper.PullListInDataGridView(form.dataGridView5,
                ArhivService.GetListByDataGrid(ArhivService.GetList(form.dateTimePicker2.Value, form.dateTimePicker3.Value)),
                new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка", "Номер"));
            form.dataGridView5.Focus();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        public override void Clear()
        {
            form.dataGridView5.DataSource = null;
        }

        #region Клавиши
        public void dataGridView5_KeyDown(KeyEventArgs e)
        {

        }
        #endregion

    }
}
