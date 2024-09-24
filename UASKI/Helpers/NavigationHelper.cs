using System.Collections.Generic;
using System.Linq;
using UASKI.Models;
using UASKI.StaticModels;
using UASKI.Services;

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
            
            NavigationHelper.GetView();
        }

        /// <summary>
        /// Загружает контент на выбраной форме
        /// </summary>
        public static void GetView()
        {
            var form = SystemData.Form;
           
            switch (form.tabControl1.SelectedIndex)
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
            }
            form.Menu_Step2.Enabled = false;
        }

        /// <summary>
        /// Отчищает форму по индексу
        /// </summary>
        /// <param name="index">Индекс формы</param>
        private static void ClearForm(int index)
        {
            var form = SystemData.Form;

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
                    break;
            }
        }
    }
}
