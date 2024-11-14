using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы редактирования исполнителя
    /// </summary>
    public class EditIsp : BasePage
    {
         /// <summary>
         /// Базовый конструктор для установки индекса страницы
         /// </summary>
         /// <param name="index">Индекс страницы</param>
        public EditIsp(int index) : base(index) { }

        /// <summary>
        /// Главная форма приложения
        /// </summary>
        private Gl_Form form = SystemData.Form;

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        protected override void Show()
        {
            
        }

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        /// <param name="code">Код исполнителя</param>
        
        private int Code;

        public void Show(int code)
        {
            Code = code;
            var form = SystemData.Form;
            var isp = IspService.GetByCode(code);

            form.textBox18.Text = isp.FirstName;
            form.textBox17.Text = isp.Name;
            form.textBox16.Text = isp.LastName;
            form.textBox15.Text = isp.Code.ToString();
            form.textBox14.Text = isp.CodePodr.ToString();

            form.label28.Visible = false;
            form.label29.Visible = false;
            form.label30.Visible = false;
            form.label31.Visible = false;
            form.label32.Visible = false;

            var list = TasksService.GetList().Where(c => c.IdCon == code || c.IdIsp == code).ToList();

            SystemHelper.PullListInDataGridView(form.dataGridView4,
                 TasksService.GetListByDataGrid(list),
                 new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));

            SystemHelper.SelectTextBox(form.textBox18);

            form.dataGridView4.ClearSelection();
        }

        /// <summary>
        /// Отчищает страницу
        /// </summary>
        protected override void Clear()
        {
            form.textBox18.Clear();
            form.textBox17.Clear();
            form.textBox16.Clear();
            form.textBox15.Clear();
            form.textBox14.Clear();

            form.dataGridView4.DataSource = null;
            SystemHelper.SelectButton(false, form.button6);
            SystemHelper.SelectButton(false, form.button7);
        }

        /// <summary>
        /// Выход с страницы
        /// </summary>
        protected override void Exit()
        {
            SystemData.Pages.SelectIsp.Init();
        }

        #region Клавиши
        public void textBox18_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox17);
            }
        }

        public void textBox17_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox18);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void textBox16_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox17);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox15);
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void textBox15_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox14);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void textBox14_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox15);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button6);
            }
        }

        public void button6_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button6);
                SystemHelper.SelectTextBox(form.textBox18);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = IspService.Update
                   (
                   Code,
                   TextBoxElement.New(form.textBox18, form.label32),
                   TextBoxElement.New(form.textBox17, form.label31),
                   TextBoxElement.New(form.textBox16, form.label30),
                   TextBoxElement.New(form.textBox15, form.label29),
                   TextBoxElement.New(form.textBox14, form.label28)
                   );

                    if (result)
                    {
                        Exit();
                        ErrorHelper.StatusComlite();
                    }
                    else
                    {
                        ErrorHelper.StatusError();
                    }
                }
                else
                {
                    ErrorHelper.StatusQuery();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button6);
                SystemHelper.SelectButton(true, form.button7);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button6);
                Exit();
            }
            e.IsInputKey = true;
        }

        public void button7_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button7);
                SystemHelper.SelectButton(true, form.button6);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button7);
                SystemHelper.SelectTextBox(form.textBox18);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    if (IspService.Disactive(Code))
                    {
                        Exit();
                        ErrorHelper.StatusComlite();
                    }
                    else
                    {
                        ErrorHelper.StatusError();
                    }
                }
                else
                {
                    ErrorHelper.StatusQuery();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectButton(false, form.button7);
                Exit();
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button7);

                SystemHelper.SelectDataGridView(true, form.dataGridView4);
            }

            e.IsInputKey = true;
        }

        public void dataGridView4_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView4.SelectedRows.Count != 0
                && form.dataGridView4.SelectedRows[0].Index == 0))
            {

                SystemHelper.SelectDataGridView(false, form.dataGridView4);
                SystemHelper.SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectDataGridView(false, form.dataGridView4);
                Exit();
                e.Handled = true;
            }
        }
        #endregion
    }
}
