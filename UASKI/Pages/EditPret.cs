using System;
using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class EditPret : BasePage
    {
        public EditPret(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {

        }

        private string CodeTask;
        private int Type;
        private int Page;
        private bool IsArhiv;

        public void Show(string codeTask , int type , int page , bool isArhiv)
        {
            CodeTask = codeTask;
            Type = type;
            Page = page;
            IsArhiv = isArhiv;

            form.dateTimePicker16.Value = DateTime.Today;

            if (type == 1)
                form.label102.Text = $"Претензия на задачу {codeTask}";
            else if(type == 2)
                form.label102.Text = $"Рецензия на задачу {codeTask}";

            form.button46.Enabled = true;
            form.button45.Enabled = false;
            form.button44.Enabled = false;

            SystemHelper.SelectTextBox(form.textBox38);
            
        }

        protected override void Clear()
        {
            form.textBox38.Clear();
            form.textBox39.Clear();

            SystemHelper.SelectButton(false, form.button44);
            SystemHelper.SelectButton(false, form.button45);
            SystemHelper.SelectButton(false, form.button46);
        }

        protected override void Exit()
        {
            SystemData.Pages.EditTask.Init(false);
            SystemData.Pages.EditTask.Show(CodeTask, IsArhiv, Page);
        }

        private void SelectButton(int idButton = 0, bool Up = false)
        {
            var list = new Button[] { form.button46, form.button45, form.button44 };
            int index = idButton - 1;

            while (true)
            {
                if (Up)
                    index--;
                else
                    index++;

                if (index == -1 || index == list.Length)
                    break;

                if (list[index].Enabled)
                {
                    foreach (var item in list)
                        SystemHelper.SelectButton(false, item);

                    SystemHelper.SelectButton(true, list[index]);
                    break;
                }

            }
        }

        #region Клавиши
        public void textBox38_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                form.dateTimePicker16.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void dateTimePicker16_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox39);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox38);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker16);
                f.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox39_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                form.dateTimePicker16.Focus();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void button46_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox38);
                SystemHelper.SelectButton(false, form.button46);
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectButton(1, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = PretService.Add(CodeTask , TextBoxElement.New(form.textBox38, form.label92),
                        DateTimeElement.New(form.dateTimePicker16, form.label93),
                        TextBoxElement.New(form.textBox39, form.label94) , Type);

                    if (result)
                    {
                        ErrorHelper.StatusComlite();
                        Exit();
                    }
                    else
                        ErrorHelper.StatusError();
                }
                else
                    ErrorHelper.StatusQuery();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button45_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox38);
                SystemHelper.SelectButton(false, form.button45);
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectButton(2, false);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectButton(2, true);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {

                }
                else
                    ErrorHelper.StatusQuery();

            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button44_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox38);
                SystemHelper.SelectButton(false, form.button44);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectButton(3, true);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {

                }
                else
                    ErrorHelper.StatusQuery();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        #endregion
    }
}
