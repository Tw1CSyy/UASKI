using System.Windows.Forms;
using System;
using UASKI.Helpers;
using UASKI.Services;
using UASKI.StaticModels;
using System.Linq;
using System.Collections.Generic;

namespace UASKI.Models.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра исполнителей
    /// </summary>
    public class SelectIsp : BasePageSelect
    {
        public SelectIsp(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            Select();
            FilterClose();
        }

        protected override void Clear()
        {
            form.textBox13.Clear();
            form.IspDataGridView.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();

        }

        public override void Select()
        {
            var model = IspService.GetList().Where(c => c.IsActive).ToList();
            var search = form.textBox13.Text;

            model = model.Where(c => c.Code.ToString().Contains(search) ||
                c.CodePodr.ToString().Contains(search) ||
                c.FirstName.ToLower().Contains(search.ToLower()) ||
                c.Name.ToLower().Contains(search.ToLower()))
                .ToList();

            var result = new List<DataGridRowModel>();

            foreach (var item in model.OrderBy(c => c.FirstName).ThenBy(c => c.Name).ThenBy(c => c.LastName))
            {
                var d = new DataGridRowModel(item.Code.ToString(), item.FirstName, item.Name, item.LastName, item.CodePodr.ToString());
                result.Add(d);
            }

            Select(form.IspDataGridView
                        , result
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
        }

        protected override void FilterOpen()
        {
            FilterOpen(form.IspDataGridView, form.panel12, form.textBox13, form.button19);
        }

        protected override void FilterClose()
        {
            FilterClose(form.IspDataGridView, form.panel12, form.textBox13, form.button19);
        }

        #region Клавиши
        public void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            // Если нажали Enter и находимся на верхней строчке или Escape
            if ((e.KeyCode == Keys.Up
                && form.IspDataGridView.SelectedRows.Count != 0
                && form.IspDataGridView.SelectedRows[0].Index == 0)
                || e.KeyCode == Keys.Escape)
            {
                Exit();
                SystemHelper.SelectDataGridView(form.IspDataGridView, false);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (form.IspDataGridView.SelectedRows.Count > 0)
                {
                    var code = Convert.ToInt32(form.IspDataGridView.SelectedRows[0].Cells[0].Value);
                    SystemData.Pages.EditIsp.Init(false);
                    SystemData.Pages.EditIsp.Show(code);
                }

                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey || e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.IspDataGridView, e.KeyCode);
                e.Handled = true;
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox13, e.KeyCode);
            }

        }

        public void textBox13_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                FilterClose();
                e.Handled = true;
            }
        }
        #endregion

    }
}
