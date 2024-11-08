using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class SelectTask : BasePage
    {
        private Gl_Form form = SystemData.Form;
        public SelectTask (int index) : base(index) { }

        protected override void Show()
        {
            SystemHelper.PullListInDataGridView(form.dataGridView3,
                        TasksService.GetListByDataGrid(TasksService.GetList(form.textBox19.Text)),
                        new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));

            form.dataGridView3.Focus();
        }

        public override void Clear()
        {
            form.textBox18.Clear();
            form.dataGridView3.DataSource = null;
        }

        public void dataGridView3_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.dataGridView3.SelectedRows.Count != 0
                && form.dataGridView3.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.dataGridView3.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter && form.dataGridView3.SelectedRows.Count > 0)
            {
                var code = form.dataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(code, false);
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox19, e.KeyCode);
            }
        }

    }
}
