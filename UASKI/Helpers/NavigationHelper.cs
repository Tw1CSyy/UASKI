using System.Collections.Generic;
using System.Linq;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;
using System.Windows.Forms.VisualStyles;
using System.Reflection;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для навигации между табами (далее формы)
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Расчитывает и переносит на соотвествующий таб
        /// </summary>
        public static void Start()
        {
            var form = SystemData.Form;
            var elem = SystemData.MenuItems.FirstOrDefault(c => c.Text.Equals(form.Menu_Step1.SelectedItem.ToString()));
            var el = elem.Items.FirstOrDefault(c => c.Text.Equals(form.Menu_Step2.SelectedItem.ToString()));

            int index = form.tabControl1.SelectedIndex;
            form.tabControl1.SelectedIndex = el.NumberTabPage;
            
            if(index != el.NumberTabPage)
            {
                ClearForm(index);
            }

            SystemData.Index = form.tabControl1.SelectedIndex;
            NavigationHelper.GetView();
        }

        /// <summary>
        /// Загружает контент на выбраной форме
        /// </summary>
        public static void GetView()
        {
            var form = SystemData.Form;

            switch (SystemData.Index)
            {
                case 1:
                    SystemHelper.PullListInDataGridView(form.IspDataGridView
                        , IspService.GetListByDataGrid()
                        , new DataGridRowModel("Табельный номер" , "Фамилия" , "Имя" , "Отчество" , "Код подразделения"));
                    form.IspDataGridView.Focus();
                    break;

                case 4:
                    form.textBox1.Focus();
                    break;

                case 5:
                    SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid()
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
                    form.IspDataGridView.Focus();
                    form.textBox8.Focus();
                    form.dataGridView1.ClearSelection();
                    break;
            }

            form.Menu_Step2.Enabled = false;
        }

        /// <summary>
        /// Отчищает форму по индексу или по текущей странице
        /// </summary>
        /// <param name="index">Индекс формы</param>
        public static void ClearForm(int index = -1)
        {
            var form = SystemData.Form;

            if(index == -1)
            {
                index = SystemData.Index;
            }

            switch (index)
            {
                case 1:
                    form.IspDataGridView.DataSource = null;
                    break;

                case 4:
                    form.textBox1.Clear();
                    form.textBox2.Clear();
                    form.textBox3.Clear();
                    form.textBox4.Clear();
                    form.textBox5.Clear();
                    form.textBox6.Clear();
                    form.textBox7.Clear();
                    SystemHelper.SelectButton(false, form.button1);
                    form.dateTimePicker1.Value = System.DateTime.Today;
                    form.textBox1.Focus();
                    break;
            }
        }
    }
}
