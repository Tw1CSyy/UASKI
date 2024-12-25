using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы редактирования исполнителя
    /// </summary>
    public class EditIsp : BasePageEdit
    {
        public EditIsp(int index) : base(index) { }

        protected override void Show()
        {
            
        }

        private IspModel Isp;

        public void Select()
        {
            var list = TaskModel.GetList().Where(c => c.IdCon == Isp.Code || c.IdIsp == Isp.Code).ToList();

            var model = new List<DataGridRowModel>();

            foreach (var item in list.OrderBy(c => c.Date))
            {
                var st = new DataGridRowModel(item.Code, item.Isp.InizByCode, item.Con.InizByCode, item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            var columns = new DataGridColumnModel[]
            {
                new DataGridColumnModel("Код"),
                new DataGridColumnModel("Исполнитель"),
                new DataGridColumnModel("Контроллер"),
                new DataGridColumnModel("Срок" , typeof(DateTime))
            };

            form.DataGridView4.PullListInDataGridView(model.ToArray(), columns);
        }

        public void Show(int code , BasePageSelect page)
        {
            Page = page;

            var form = SystemData.Form;
            var isp = IspModel.GetByCode(code);

            Isp = isp;

            form.textBox18.Text = isp.FirstName;
            form.textBox17.Text = isp.Name;
            form.textBox16.Text = isp.LastName;
            form.textBox15.Text = isp.Code.ToString();
            form.textBox14.Text = isp.CodePodr.ToString();

            Select();

            SelectTextBox(form.textBox18);

            form.DataGridView4.d.ClearSelection();
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

            form.DataGridView4.d.DataSource = null;
            SelectButton(form.button6, false);
            SelectButton(form.button7, false);
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
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
                SelectButton(form.button6);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox17);
                e.Handled = true;
            }
        }

        public void textBox17_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox18);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button6);
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
                SelectTextBox(form.textBox17);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button6);
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
                SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectDataGridView(form.DataGridView4.d);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox14);
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
                SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox16);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectDataGridView(form.DataGridView4.d);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton(form.button6);
                e.Handled = true;
            }
        }

        public bool button6_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button6, false);
                SelectTextBox(form.textBox18);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var firstName = TextBoxElement.New(form.textBox18, form.label32);
                    var name = TextBoxElement.New(form.textBox17, form.label31);
                    var lastName = TextBoxElement.New(form.textBox16, form.label30);
                    var code = TextBoxElement.New(form.textBox15, form.label29);
                    var podr = TextBoxElement.New(form.textBox14, form.label28);

                    var result = ValidationHelper.IspValidation(firstName, name, lastName, code, podr , true);

                    if(result == false)
                    {
                        Ai.Error();
                        return false;
                    }

                    var isp = new IspModel(code.Num, firstName.Value, name.Value, lastName.Value, podr.Num);
                    result = isp.Update(Isp.Code);

                    if (result == false)
                    {
                        Ai.AppError();
                        return false;
                    }

                    Exit();
                    Ai.Comlite($"Сотрудник с кодом {code.Value} изменен");
                }
                else
                {
                    Ai.Query();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectButton(form.button6, false);
                SelectButton(form.button7);
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Up)
            {
                SelectButton(form.button6, false);
                Exit();
            }

            e.IsInputKey = true;

            return true;
        }

        public void button7_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(form.button7, false);
                SelectButton(form.button6);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button7, false);
                SelectTextBox(form.textBox18);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = Isp.Delete();

                    if(result)
                    {
                        Ai.Comlite($"Сотрудник с кодом {Isp.Code} удален");
                        Exit();
                    }
                    else
                    {
                        Ai.AppError();
                    }
                }
                else
                {
                    Ai.Query();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectButton(form.button7, false);
                Exit(); 
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (SelectDataGridView(form.DataGridView4.d))
                SelectButton(form.button7, false);
            }

            e.IsInputKey = true;
        }

        public void dataGridView4_KeyDown(KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up
                && form.DataGridView4.d.SelectedRows.Count != 0
                && form.DataGridView4.d.SelectedRows[0].Index == 0))
            {

                SelectDataGridView(form.DataGridView4.d, false);
                SelectTextBox(form.textBox15);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectDataGridView(form.DataGridView4.d, false);
                Exit();
                e.Handled = true;
            }
            else
            {
                form.DataGridView4.KeyDown(e);
            }
        }
        #endregion
    }
}
