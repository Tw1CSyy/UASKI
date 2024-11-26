using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class PrintMer : BasePagePrint
    {
        public PrintMer(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            form.label79.Text = "На " + DateTime.Today.ToString("dd.MM.yyyy");
            form.textBox35.Focus();
        }

        protected override void Clear()
        {
            form.textBox35.Clear();
            SystemHelper.SelectButton(false, form.button38);
            form.dataGridView9.DataSource = null;
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
            SystemHelper.SelectDataGridView(false, form.dataGridView9);
            SystemHelper.SelectButton(false, form.button38);
        }

        public override void Select()
        {
            var taskList = TasksService.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var arhivList = ArhivService.GetList().Where(c => c.Code.ToLower().Contains(form.textBox35.Text.ToLower())).ToList();
            var ispList = IspService.GetList();
            var result = new List<DataGridRowModel>();

            foreach (var item in taskList)
            {
                var isp = IspService.GetByCode(item.IdIsp, ispList);
                var con = IspService.GetByCode(item.IdCon, ispList);
                var date = (DateTime.Today - item.Date).Days;

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.Code,
                    $"{isp.Code} {isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.",
                    $"{con.Code} {con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}.",
                    item.Date.ToString("dd.MM.yyyy"), "", "", date.ToString());

                result.Add(model);
            }

            foreach (var item in arhivList)
            {
                var isp = IspService.GetByCode(item.IdIsp, ispList);
                var con = IspService.GetByCode(item.IdCon, ispList);
                var date = (item.DateClose - item.Date).Days;

                if (date < 0)
                    date = 0;

                var model = new DataGridRowModel(item.Code,
                    $"{isp.Code} {isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.",
                    $"{con.Code} {con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}.",
                    item.Date.ToString("dd.MM.yyyy"),
                    item.DateClose.ToString("dd.MM.yyyy"),
                    item.Otm.ToString(),
                    date.ToString());

                result.Add(model);
            }

            SystemHelper.PullListInDataGridView(form.dataGridView9,
                result,
                new DataGridRowModel("Код задания", "Исполнитель", "Котролер", "Срок", "Дата закрытия", "Оценка", "Дни опоздания"));
        }

        protected override void Print()
        {

        }

        #region Клавиши

        public void textBox35_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView9);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button38);
                e.Handled = true;
            }
        }

        public void dataGridView9_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.Up
                && form.dataGridView9.SelectedRows.Count != 0
                && form.dataGridView9.SelectedRows[0].Index == 0))
            {
                SystemHelper.SelectTextBox(form.textBox35);
                e.Handled = true;
            }
                
        }

        public void button38_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                Exit();
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox35);
                SystemHelper.SelectButton(false, form.button38);
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Down)
            {
                if(SystemHelper.SelectDataGridView(true, form.dataGridView9))
                    SystemHelper.SelectButton(false, form.button38);
                e.IsInputKey = true;
            }

        }
        #endregion
    }
}
