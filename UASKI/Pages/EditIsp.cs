using System;
using System.Collections.Generic;
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
        public EditIsp(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {
            
        }

        private int Code;

        public void Select()
        {
            var list = TasksService.GetList().Where(c => c.IdCon == Code || c.IdIsp == Code).ToList();

            var model = new List<DataGridRowModel>();

            var listUser = IspService.GetList();

            foreach (var item in list.OrderByDescending(c => c.Date))
            {
                var isp = IspService.GetByCode(item.IdIsp, listUser);
                var con = IspService.GetByCode(item.IdCon, listUser);

                var st = new DataGridRowModel(item.Code, IspService.GetIniz(isp), IspService.GetIniz(con), item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контроллер"),
                new DataGridColumnModel("Срок")
            };

            SystemHelper.PullListInDataGridView(form.dataGridView4, model.ToArray(), columns);
        }

        public void Show(int code)
        {
            Code = code;
            var form = SystemData.Form;
            var isp = IspService.GetByCode(code , IspService.GetList());

            form.textBox18.Text = isp.FirstName;
            form.textBox17.Text = isp.Name;
            form.textBox16.Text = isp.LastName;
            form.textBox15.Text = isp.Code.ToString();
            form.textBox14.Text = isp.CodePodr.ToString();

            Select();

            SystemHelper.SelectTextBox(form.textBox18);

            form.dataGridView4.ClearSelection();
        }

        protected override void Clear()
        {
            form.textBox18.Clear();
            form.textBox17.Clear();
            form.textBox16.Clear();
            form.textBox15.Clear();
            form.textBox14.Clear();

            form.label28.Visible = false;
            form.label29.Visible = false;
            form.label30.Visible = false;
            form.label31.Visible = false;
            form.label32.Visible = false;

            form.dataGridView4.DataSource = null;
            SystemHelper.SelectButton(form.button6, false);
            SystemHelper.SelectButton(form.button7, false);
        }

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
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button6);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox17);
                e.Handled = true;
            }
        }

        public void textBox17_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox18);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button6);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox16_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox17);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button6);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox15_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectDataGridView(form.dataGridView4);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox14);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox14_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectDataGridView(form.dataGridView4);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(form.button6);
                e.Handled = true;
            }
        }

        public void button6_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(form.button6, false);
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
                SystemHelper.SelectButton(form.button6, false);
                SystemHelper.SelectButton(form.button7);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(form.button6, false);
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button7_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(form.button7, false);
                SystemHelper.SelectButton(form.button6);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(form.button7, false);
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
                SystemHelper.SelectButton(form.button7, false);
                Exit(); 
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (SystemHelper.SelectDataGridView(form.dataGridView4))
                SystemHelper.SelectButton(form.button7, false);
            }

            e.IsInputKey = true;
        }

        public void dataGridView4_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.dataGridView4.SelectedRows.Count != 0
                && form.dataGridView4.SelectedRows[0].Index == 0))
            {

                SystemHelper.SelectDataGridView(form.dataGridView4, false);
                SystemHelper.SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectDataGridView(form.dataGridView4, false);
                Exit();
                e.Handled = true;
            }
            else if(e.Control)
            {
                SystemHelper.DataGridViewSort(form.dataGridView4, e.KeyCode);
            }
        }
        #endregion
    }
}
