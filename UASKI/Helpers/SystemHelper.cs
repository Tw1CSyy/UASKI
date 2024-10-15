using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Общий системный хелпер
    /// </summary>
    public static class SystemHelper
    {
        /// <summary>
        /// Добавляет в listBox список элементов
        /// </summary>
        /// <param name="listBox">Объект listBox</param>
        /// <param name="items">Массив элементов</param>
        public static void WriteListBox(ListBox listBox , string[] items)
        {
            listBox.Items.Clear();

            foreach (var item in items)
            {
                listBox.Items.Add(item);
            }
        }

        /// <summary>
        /// Заполняет DataGricView данными
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="values">Колекция строк с данными</param>
        /// <param name="columns">Набор названий колонок</param>
        public static void PullListInDataGridView(DataGridView d , List<DataGridRowModel> values , DataGridRowModel columns)
        {
            d.DataSource = null;
            
            var table = new DataTable();

            foreach (var item in columns.Values)
            {
                table.Columns.Add(item);
            }

            foreach (var line in values)
            {
                var row = table.NewRow();
                
                for(int i = 0; i < line.Values.Length; i++)
                {
                    row[i] = line.Values[i];
                }

                table.Rows.Add(row);
            }

            d.DataSource = table;

            for(int i = 0; i < columns.Values.Length; i++)
            {
                d.Columns[i].Width = (int)Math.Floor((double)(d.Width - 40) / (double)columns.Values.Length);
            }
        }

        /// <summary>
        /// Меняет фон кнопки для отображения выделения
        /// </summary>
        /// <param name="selected">Вкл/Выкл</param>
        /// <param name="btn">Кнопка</param>
        public static void SelectButton(bool selected , Button btn)
        {
            if(selected)
            {
                btn.BackColor = Color.LightBlue;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }
    }
}
