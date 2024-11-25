using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Models
{
    /// <summary>
    /// Абстрактный класс для объектов страницы
    /// </summary>
    public abstract class BasePage
    {
        /// <summary>
        /// Индекс страницы
        /// </summary>
        private int Index;

        /// <summary>
        /// Загрузить данные на страницу
        /// </summary>
        protected abstract void Show();

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        protected abstract void Clear();

        /// <summary>
        /// Выход с страницы
        /// </summary>
        protected abstract void Exit();

        /// <summary>
        /// Отчистить страницу
        /// </summary>
        protected void ClearPage()
        {
            SystemData.IsClear = true;
            Clear();
            SystemData.IsClear = false;
        }

        /// <summary>
        /// Переход на страницу и ее загрузка
        /// </summary>
        /// <param name="IsOpen">false - не открывать автоматически</param>
        public void Init(bool IsOpen = true)
        {
            var form = SystemData.Form;
            form.tabControl1.SelectedIndex = Index;
            form.Menu_Step2.Enabled = false;

            if (SystemData.This != null)
                SystemData.This.ClearPage();
            else
                ClearPage();

            SystemData.This = this;

            if (IsOpen)
            {
                SystemData.IsClear = true;
                Show();
                SystemData.IsClear = false;
            }
        }

        /// <summary>
        /// Базовый конструктор для объявления объекта страницы
        /// </summary>
        /// <param name="index"></param>
        public BasePage(int index)
        {
            Index = index;
        }

        /// <summary>
        /// Выводит данные в DataGridView
        /// </summary>
        /// <param name="d">DataGridView</param>
        /// <param name="values">Список моделей данных</param>
        /// <param name="columns">Список моделей названий</param>
        protected void Select(DataGridView d, List<DataGridRowModel> values, DataGridRowModel columns)
        {
            SystemHelper.PullListInDataGridView(d, values, columns);
        }
    }
}
