using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;

namespace UASKI.Forms
{
    public partial class IspForm : Form
    {
        private TextBox t1;
        private TextBox t2;
        private TextBox t3;

        /// <summary>
        /// Начальная настройка
        /// </summary>
        /// <param name="t1">Фамилия</param>
        /// <param name="t2">Подразделение</param>
        /// <param name="t3">Таб Номер</param>
        public IspForm(TextBox t1 , TextBox t2 , TextBox t3 )
        {
            InitializeComponent();

            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;

            Start();
        }

        private void Start(string search = "")
        {
            var model = IspService.GetList().Where(c => c.IsActive).ToList();

            if(search.Length > 0)
            {
                model = model.Where(c => c.Code.ToString().Contains(search) ||
                    c.CodePodr.ToString().Contains(search) ||
                    c.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.Name.ToLower().Contains(search.ToLower()))
                    .ToList();
            }

            var result = new List<DataGridRowModel>();

            foreach (var item in model.OrderBy(c => c.FirstName).ThenBy(c => c.Name).ThenBy(c => c.LastName))
            {
                var d = new DataGridRowModel(item.Code.ToString(), item.FirstName, item.Name, item.LastName, item.CodePodr.ToString());
                result.Add(d);
            }

            SystemHelper.PullListInDataGridView(dataGridView1,
                result,
                new Models.DataGridRowModel("Код", "Фамилия", "Имя", "Отчество", "Подразделение"));

            dataGridView1.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            var form = this;

            if (e.KeyCode == Keys.Escape)
            {
                form.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                var row = dataGridView1.SelectedRows;

                if (row != null && row.Count > 0)
                {
                    t1.Text = row[0].Cells[1].Value.ToString() + " " + row[0].Cells[2].Value.ToString()[0] + ". " + row[0].Cells[3].Value.ToString()[0] + ".";
                    t2.Text = row[0].Cells[4].Value.ToString();
                    t3.Text = row[0].Cells[0].Value.ToString();

                    form.Dispose();
                    SystemHelper.SelectTextBox(t3);
                }
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(dataGridView1, e.KeyCode);
            }
            else
            {
                SystemHelper.CharInTextBox(form.textBox1, e.KeyCode);
            }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            Start(textBox1.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var key = new KeyEventArgs(Keys.Enter);
            dataGridView1_KeyDown(sender, key);
        }
    }
}
