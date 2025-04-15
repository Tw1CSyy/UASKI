using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Models.Components;
using UASKI.StaticModels;

namespace UASKI.Models.Pages
{
    /// <summary>
    /// Класс для объекта страницы просмотра исполнителей
    /// </summary>
    public class SelectIsp : BasePageSelect
    {
        public SelectIsp(int index) : base(index) { }
        public override DataGridViewComponent DataGridView { get => form.IspDataGridView; protected set => throw new NotImplementedException(); }

        protected override void Show()
        {
            if(this.IsCleared)
            {
                FilterClose();
            }

            Select();
            form.IspDataGridView.d.Focus();
        }

        protected override void Clear()
        {
            form.textBox13.Clear();
            form.IspDataGridView.d.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();

        }

        public override void Select()
        {
            var model = IspModel.GetList();
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

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Таб номер" , typeof(int)),
                new DataGridColumnModel("Фамилия"),
                new DataGridColumnModel("Имя"),
                new DataGridColumnModel("Отчество"),
                new DataGridColumnModel("Код подразделения" , typeof(int))
            };

            form.IspDataGridView.PullListInDataGridView(result.ToArray(), columns);
        }
              
        protected override void FilterOpen()
        {
            FilterOpen(form.IspDataGridView, form.panel12, form.textBox13, form.button19);
        }

        protected override void FilterClose()
        {
            FilterClose(form.IspDataGridView, form.panel12, form.textBox13, form.button19);
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }
        #region Клавиши
        public void IspDataGridView_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
                SelectDataGridView(form.IspDataGridView.d, false);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (form.IspDataGridView.d.SelectedRows.Count > 0)
                {
                    var code = Convert.ToInt32(form.IspDataGridView.d.SelectedRows[0].Cells[0].Value);
                    Ai.Pages.EditIsp.Init(false , false);
                    Ai.Pages.EditIsp.Show(code , this);
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                FilterOpen();
                e.Handled = true;
            }
            else
            {
                form.IspDataGridView.KeyDown(e , form.textBox13);
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
