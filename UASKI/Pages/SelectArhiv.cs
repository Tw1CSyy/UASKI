using System;
using System.Windows.Forms;
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
                ArhivService.GetListByDataGrid(ArhivService.GetList(form.dateTimePicker2.Value, form.dateTimePicker3.Value)),
                new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок", "Дата закрытия", "Оценка", "Номер"));
            form.dataGridView5.Focus();
        }

        public override void Clear()
        {
            form.dataGridView5.DataSource = null;
        }

        public void dataGridView5_KeyDown(KeyEventArgs e)
        {
           
        }

    }
}
