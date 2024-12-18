using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы добавления исполнителя
    /// </summary>
    public class AddIsp : BasePage
    {
        public AddIsp(int index) : base(index) { }

        protected override void Show()
        {
            form.textBox8.Focus();
        }

        protected override void Clear()
        {
            form.textBox8.Clear();
            form.textBox9.Clear();
            form.textBox10.Clear();
            form.textBox11.Clear();
            form.textBox12.Clear();

            form.label18.Visible = false;
            form.label19.Visible = false;
            form.label20.Visible = false;
            form.label21.Visible = false;
            form.label22.Visible = false;

            SelectButton(form.button4, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        #region Клавиши
        public void textBox8_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox9);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox9_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox8);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox10_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox11);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox9);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox11_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox12);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectButton(form.button4);
                e.Handled = true;
            }
        }

        public void textBox12_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button4);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox11);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox10);
                e.Handled = true;
            }
        }

        public void button4_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    ErrorHelper.StatusWait();

                    var result = IspService.Add(
                        TextBoxElement.New(form.textBox8, form.label18),
                        TextBoxElement.New(form.textBox9, form.label19),
                        TextBoxElement.New(form.textBox10, form.label20),
                        TextBoxElement.New(form.textBox11, form.label21),
                        TextBoxElement.New(form.textBox12, form.label22)
                    );

                    if (result)
                    {
                        ClearPage();
                        ErrorHelper.StatusComlite();
                        Show();
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
                Exit();
                SelectButton(form.button4, false);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox12);
                SelectButton(form.button4, false);
            }

            e.IsInputKey = true;
        }

        #endregion
    }
}
