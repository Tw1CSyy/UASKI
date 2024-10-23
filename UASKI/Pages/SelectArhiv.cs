using System;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class SelectArhiv : BasePage
    {
        private Gl_Form form = SystemData.Form;

        public override void Show()
        {
            form.dateTimePicker2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            form.dateTimePicker3.Value = DateTime.Today;

            SystemHelper.PullListInDataGridView(form.dataGridView5,
                TasksService.GetListByDataGrid(TasksService.GetListArhiv(form.dateTimePicker2.Value, form.dateTimePicker3.Value)),
                new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка", "Номер"));
            form.dataGridView5.Focus();
        }

        public override void Clear()
        {
            form.dataGridView5.DataSource = null;
        }

    }
}
