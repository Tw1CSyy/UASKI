using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы добавления задачи
    /// </summary>
    public class AddTask : BasePage
    {
        public AddTask(int index) : base(index) { }

        protected override void Show()
        {
            SelectTextBox(form.textBox1);
            // form.dateTimePicker1.Value = System.DateTime.Today;
        }

        protected override void Clear()
        {
            //form.textBox1.Clear();
            //form.textBox2.Clear();
            //form.textBox3.Clear();
            //form.textBox4.Clear();
            //form.textBox5.Clear();
            //form.textBox6.Clear();
            //form.textBox7.Clear();

            form.label23.Visible = false;
            form.label24.Visible = false;
            form.label25.Visible = false;
            form.label26.Visible = false;

            SelectButton(form.button1, false);
        }

        protected override void Exit()
        {
            form.Menu_Step2.Enabled = true;
            form.Menu_Step2.Focus();
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }

        #region Клавиши
        public void textBox1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (form.textBox1.Text.Length != 0 && form.textBox3.Text.Length == 0)
                {
                    var isp = IspModel.GetList().FirstOrDefault(c => c.FirstName.ToLower().Equals(form.textBox1.Text.ToLower()));

                    if (isp != null)
                    {
                        form.textBox2.Text = isp.CodePodr.ToString();
                        form.textBox3.Text = isp.Code.ToString();
                        form.textBox1.Text = isp.InizByCode;
                    }

                    SelectTextBox(form.textBox4);
                }
                else
                {
                    SelectTextBox(form.textBox4);
                }

                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button1, true);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var form1 = new IspForm(form.textBox1, form.textBox2, form.textBox3);
                form1.Show();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Back)
            {
                form.textBox1.Clear();
                form.textBox2.Clear();
                form.textBox3.Clear();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox4_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (form.textBox4.Text.Length != 0 && form.textBox6.Text.Length == 0)
                {
                    var isp = IspModel.GetList().FirstOrDefault(c => c.FirstName.ToLower().Equals(form.textBox4.Text.ToLower()));

                    if (isp != null)
                    {
                        form.textBox5.Text = isp.CodePodr.ToString();
                        form.textBox6.Text = isp.Code.ToString();
                        form.textBox4.Text = isp.InizByCode;
                    }

                    SelectTextBox(form.textBox7);
                }
                else
                {
                    SelectTextBox(form.textBox7);
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SelectButton(form.button1, true);
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var form1 = new IspForm(form.textBox4, form.textBox5, form.textBox6);
                form1.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                form.textBox4.Clear();
                form.textBox5.Clear();
                form.textBox6.Clear();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void textBox7_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker1.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox4);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public bool button1_KeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var code = TextBoxElement.New(form.textBox7, form.label25);
                    var idIsp = TextBoxElement.New(form.textBox3, form.label23);
                    var idCon = TextBoxElement.New(form.textBox6, form.label24);
                    var date = DateTimeElement.New(form.dateTimePicker1, form.label26);

                    var result = ValidationHelper.TaskValidation(code, idIsp, idCon, date);
                      
                    if(result == false)
                    {
                        Ai.Error();
                        return false;
                    }
                        
                    var model = new TaskModel(code.Value, idIsp.Num, idCon.Num, date.Value);
                    result = model.Add();

                    if (result == false)
                    {
                        Ai.AppError();
                        return false;
                    }

                    ClearPage();
                    Ai.Comlite($"Новая задача добавлена c кодом {code.Value}");
                    Show();
                }
                else
                {
                    Ai.Query();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox4);
                SelectButton(form.button1, false);
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Escape)
            {
                SelectButton(form.button1, false);
                form.dateTimePicker1.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                SelectButton(form.button1, false);
            }

            e.IsInputKey = true;
            return true;
        }

        public void dateTimePicker1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SelectButton(form.button1);
                form.button1.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                e.Handled = true;

                SelectTextBox(form.textBox7);
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                SelectTextBox(form.textBox4);
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker1);
                f.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Exit();
            }
        }
        #endregion
    }
}
