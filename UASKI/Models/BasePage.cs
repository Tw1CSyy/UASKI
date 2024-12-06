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
        /// Отчищена ли форма
        /// </summary>
        protected bool IsCleared = true;

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
            IsCleared = true;
        }

        /// <summary>
        /// Переход на страницу и ее загрузка
        /// </summary>
        /// <param name="IsOpen">false - не открывать автоматически</param>
        /// <param name="IsClear">false - не отчищать предыдущую страницу</param>
        public void Init(bool IsOpen = true , bool IsClear = true)
        {
            var form = SystemData.Form;
            form.Menu_Step2.Enabled = false;

            if (SystemData.This != null && SystemData.This.Index != this.Index && IsClear)
                SystemData.This.ClearPage();
            else if(SystemData.This == null)
                ClearPage();

            SystemData.This = this;
           
            if (IsOpen)
            {
                SystemData.IsClear = true;
                Show();
                SystemData.IsClear = false;
            }

            form.tabControl1.SelectedIndex = Index;
            IsCleared = false;
        }

        /// <summary>
        /// Базовый конструктор для объявления объекта страницы
        /// </summary>
        /// <param name="index"></param>
        public BasePage(int index)
        {
            Index = index;
        }

    }
}
