using System.Collections.Generic;
using System.Windows.Forms;
using UASKI.Data.Entityes;
using UASKI.Helpers;
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
            var model = IspService.GetListByDataGrid(IspService.GetList(search));

            SystemHelper.PullListInDataGridView(dataGridView1,
                model,
                new Models.DataGridRowModel("Код", "Фамилия", "Имя", "Отчество", "Подразделение"));

            dataGridView1.Focus();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownHelper.dataGridView_KeyDown(e , this , t1 , t2 , t3 , dataGridView1);
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            Start(textBox1.Text);
        }
    }
}
