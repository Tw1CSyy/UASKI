using System;
using System.Collections.Generic;
using System.Data;
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

        public void KeyDown(KeyEventArgs e, TextBox textBox = null)
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
                DataGridViewSort(e.KeyCode);
            }
            else if(textBox != null)
            {
                SystemHelper.CharInTextBox(textBox, e.KeyCode);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Переводит выбранную строку в DataGridView на 1 вверх
        /// </summary>
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

        /// <summary>
        /// Сортирует DataGridView по столбцу
        /// </summary>
        /// <param name="key">Нажатая клавиша</param>
        private bool DataGridViewSort(Keys key)
        {
            var index = SystemHelper.GetIntKeyDown(key);
            if (d.SelectedRows.Count == 0)
                return false;

            var id = d.SelectedRows[0].Index;

            if (d.Columns.Count < index || index == -1)
            {
                return false;
            }

            for (int i = index - 1; i < d.ColumnCount;)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (d.Columns[j].Visible == false)
                        i++;
                }

                if (d.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.Descending || d.Columns[i].HeaderCell.SortGlyphDirection == SortOrder.None)
                {
                    d.Sort(d.Columns[i], System.ComponentModel.ListSortDirection.Ascending);
                    break;
                }
                else
                {
                    d.Sort(d.Columns[i], System.ComponentModel.ListSortDirection.Descending);
                    break;
                }
            }

            d.Rows[id].Selected = true;
            return true;
        }

        /// <summary>
        /// Заполняет DataGridView данными
        /// </summary>
        /// <param name="values">Колекция строк с данными</param>
        /// <param name="columns">Колекция колонок</param>
        public void PullListInDataGridView(DataGridRowModel[] values, DataGridColumnModel[] columns)
        {
            int selected = -1;
            int columnSort = -1;
            SortOrder sort = SortOrder.None;

            if (d.Rows.Count > 0 && d.SelectedRows.Count > 0)
            {
                selected = d.SelectedRows[0].Index;

                if (d.SortedColumn != null)
                {
                    columnSort = d.SortedColumn.Index;
                    sort = d.Columns[columnSort].HeaderCell.SortGlyphDirection;
                }
            }

            var listWithCol = new List<int>();

            if(d.Columns.Count > 0)
            {
                foreach (DataGridViewColumn item in d.Columns)
                {
                    listWithCol.Add(item.Width);
                }
            }

            d.DataSource = null;

            var table = new DataTable();

            foreach (var item in columns)
            {
                table.Columns.Add(item.Name, item.Type);
            }

            foreach (var line in values)
            {
                var row = table.NewRow();

                for (int i = 0; i < line.Values.Length; i++)
                {
                    if (line.Values[i].Length == 0)
                        row[i] = DBNull.Value;
                    else
                        row[i] = line.Values[i];
                }

                table.Rows.Add(row);
            }

            d.DataSource = table;

            for (int i = 0; i < d.Columns.Count; i++)
            {
                d.Columns[i].Visible = columns[i].Visible;
            }

            if(listWithCol.Count == 0)
                ResizeDataGridView();
            else
            {
                for(int i = 0; i < listWithCol.Count; i++)
                {
                    d.Columns[i].Width = listWithCol[i];
                }
            }

            if (selected != -1 && d.Rows.Count > 0)
            {
                if (d.Rows.Count > selected)
                    d.Rows[selected].Selected = true;
                else if (selected != 0)
                    d.Rows[d.Rows.Count - 1].Selected = true;
            }
            else if (d.Rows.Count > 0)
            {
                d.Rows[0].Selected = true;
            }

            if (d.Rows.Count > 0 && columnSort != -1 && sort != SortOrder.None)
            {
                if (sort == SortOrder.Ascending)
                    d.Sort(d.Columns[columnSort], System.ComponentModel.ListSortDirection.Ascending);
                else
                    d.Sort(d.Columns[columnSort], System.ComponentModel.ListSortDirection.Descending);
            }
        }

        /// <summary>
        /// Устанавливает размер колонок в DataGrid по ширине
        /// </summary>
        /// <param name="d">DataGirdView</param>
        public void ResizeDataGridView()
        {
            var count = 0;

            for (int i = 0; i < d.Columns.Count; i++)
            {
                if (d.Columns[i].Visible)
                    count++;
            }

            var with = (int)Math.Floor((double)d.Width / (double)count);

            for (int i = 0; i < d.Columns.Count; i++)
            {
                d.Columns[i].Width = with;
            }
        }

        /// <summary>
        /// Отчищает тег всех строк
        /// </summary>
        public void ClearTag()
        {
            for(int i = 0; i < d.Rows.Count; i++)
            {
                d.Rows[i].Tag = null;
            }
        }

    }
}
