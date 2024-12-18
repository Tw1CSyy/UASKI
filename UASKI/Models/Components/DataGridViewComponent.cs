using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Helpers;

namespace UASKI.Models.Components
{
    public class DataGridViewComponent
    {
        /// <summary>
        /// DataGridView
        /// </summary>
        public DataGridView d { get; private set; }

        public DataGridViewComponent(DataGridView dataGridView)
        {
            d = dataGridView;
        }

        public void KeyDown(KeyEventArgs e , TextBox textBox = null)
        {
            if(e.KeyCode == Keys.Up)
            {
                DataGridUpSelect();
            }
            else if(e.KeyCode == Keys.Down)
            {
                DataGridDownSelect();
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(d, e.KeyCode);
            }
            else if(textBox != null)
            {
                SystemHelper.CharInTextBox(textBox, e.KeyCode);
            }
        }

        /// <summary>
        /// Переводит выбранную строку в DataGridView на 1 вверх
        /// </summary>
        /// <param name="d">DataGridView</param>
        private void DataGridUpSelect()
        {
            if (d.SelectedRows.Count > 0 && d.SelectedRows[0].Index != 0)
            {
                var index = d.SelectedRows[0].Index;
                d.Rows[index - 1].Selected = true;

                if (d.FirstDisplayedScrollingRowIndex + 1 >= d.SelectedRows[0].Index && d.FirstDisplayedScrollingRowIndex != 0)
                {
                    d.FirstDisplayedScrollingRowIndex = d.FirstDisplayedScrollingRowIndex - 1;
                }
            }
        }

        /// <summary>
        /// Переводит выбранную строку в DataGridView на 1 вниз
        /// </summary>
        /// <param name="d">DataGridView</param>
        private void DataGridDownSelect()
        {
            if (d.SelectedRows.Count > 0 && d.SelectedRows[0].Index != d.Rows.Count - 1)
            {
                var index = d.SelectedRows[0].Index;
                d.Rows[index + 1].Selected = true;

                if (d.DisplayedRowCount(true) <= index + 2)
                {
                    d.FirstDisplayedScrollingRowIndex = d.FirstDisplayedScrollingRowIndex + 1;
                }
            }
        }

    }
}
