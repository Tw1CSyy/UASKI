using System.Windows.Forms;
using System;
using UASKI.Helpers;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Models.Pages
{
    public class SelectIsp : BasePage
    {
        private Gl_Form form = SystemData.Form;
        public SelectIsp (int index) : base(index) { }

        protected override void Show()
        {
            SystemHelper.PullListInDataGridView(form.IspDataGridView
                        , IspService.GetListByDataGrid(IspService.GetList(form.textBox13.Text))
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
            form.IspDataGridView.Focus();
        }

        public override void Clear()
        {
            form.textBox13.Clear();
            form.IspDataGridView.DataSource = null;
        }

        public void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.IspDataGridView.SelectedRows.Count != 0
                && form.IspDataGridView.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                form.IspDataGridView.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (form.IspDataGridView.SelectedRows.Count > 0)
                {
                    var code = Convert.ToInt32(form.IspDataGridView.SelectedRows[0].Cells[0].Value);
                    SystemData.Pages.EditIsp.Init(false);
                    SystemData.Pages.EditIsp.Show(code);
                }
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox13, e.KeyCode);
            }
        }

    }
}
