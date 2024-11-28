using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using UASKI.Models.Elements;
using System;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы редактирования задачи
    /// </summary>
    public class EditTask : BasePage
    {
        public EditTask(int index) : base(index) { }

        private Gl_Form form = SystemData.Form;

        protected override void Show()
        {

        }

        private string Code;
        private int Page;
        public bool Arhiv { get; private set; }

        public void Show(string code , bool IsArhiv , int page)
        {
            Code = code;
            Page = page;
            Arhiv = IsArhiv;

            var listUser = IspService.GetList();
            
            if (!IsArhiv)
            {
                var taskList = TasksService.GetList();
                var task = TasksService.GetTaskByCode(code , taskList);

                var usr1 = IspService.GetByCode(task.IdIsp , listUser);
                var usr2 = IspService.GetByCode(task.IdCon , listUser);

                form.textBox26.Text = $"{usr1.FirstName} {usr1.Name.ToUpper()[0]}. {usr1.LastName.ToUpper()[0]}.";
                form.textBox21.Text = $"{usr2.FirstName} {usr2.Name.ToUpper()[0]}. {usr2.LastName.ToUpper()[0]}.";

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.textBox27.Text = task.Code;
                form.dateTimePicker4.Value = task.Date;

                form.button11.Text = "Закрыть";
                form.button10.Enabled = true;
                form.button11.Enabled = false;
                form.button12.Enabled = true;
                form.label72.Visible = false;
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else
            {
                var listArhiv = ArhivService.GetList();
                var arhiv = ArhivService.GetByCode(code , listArhiv);

                var usr1 = IspService.GetByCode(arhiv.IdIsp, listUser);
                var usr2 = IspService.GetByCode(arhiv.IdCon, listUser);

                form.textBox26.Text = usr1.FirstName;
                form.textBox21.Text = usr2.FirstName;

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button11.Text = "Открыть";
                form.button10.Enabled = false;
                form.button11.Enabled = true;
                form.button12.Enabled = false;

                form.textBox28.Text = arhiv.Otm.ToString();
                form.dateTimePicker9.Value = arhiv.DateClose.Date;
                form.label72.Visible = true;
                SystemHelper.SelectTextBox(form.textBox26);
            }
        }

        protected override void Clear()
        {
            form.textBox24.Clear();
            form.textBox25.Clear();
            form.textBox26.Clear();
            form.textBox27.Clear();
            form.textBox28.Clear();

            form.dateTimePicker9.Value = DateTime.Today.Date;

            form.label51.Visible = false;
            form.label53.Visible = false;
            form.label55.Visible = false;
            form.label56.Visible = false;
            form.label57.Visible = false;

            SystemHelper.SelectButton(false, form.button10);
            SystemHelper.SelectButton(false, form.button11);
            SystemHelper.SelectButton(false, form.button12);
            SystemHelper.SelectButton(false, form.button47);
            SystemHelper.SelectButton(false, form.button48);
        }

        protected override void Exit()
        {
            switch (Page)
            {
                case 1:
                    SystemData.Pages.SelectTask.Init();
                    break;
                case 2:
                    SystemData.Pages.SelectArhiv.Init();
                    break;
                case 3:
                    SystemData.Pages.SelectOpz.Init();
                    break;
            }
        }

        private void SelectButton(int idButton = 0 , bool Up = false)
        {
            var list = new Button[] { form.button10, form.button11, form.button12 , form.button47 , form.button48 };
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
        public void textBox26_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox25.Text.Length == 0 && form.textBox24.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox26.Text , IspService.GetList());

                    if (isp != null)
                    {
                        form.textBox25.Text = isp.CodePodr.ToString();
                        form.textBox24.Text = isp.Code.ToString();
                        form.textBox26.Text = $"{isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.";
                    }

                    SystemHelper.SelectTextBox(form.textBox21);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox21);
                }

                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var ispForm = new IspForm(form.textBox26, form.textBox25, form.textBox24);
                ispForm.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                form.textBox26.Clear();
                form.textBox25.Clear();
                form.textBox24.Clear();
                e.Handled = true;
            }
        }

        public void textBox21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox22.Text.Length == 0 && form.textBox23.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox21.Text , IspService.GetList());

                    if (isp != null)
                    {
                        form.textBox22.Text = isp.CodePodr.ToString();
                        form.textBox23.Text = isp.Code.ToString();
                        form.textBox21.Text = $"{isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.";
                    }

                    SystemHelper.SelectTextBox(form.textBox27);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox27);
                }

                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var ispForm = new IspForm(form.textBox21, form.textBox22, form.textBox23);
                ispForm.Show();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox26);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                form.textBox21.Clear();
                form.textBox22.Clear();
                form.textBox23.Clear();
                e.Handled = true;
            }
        }

        public void textBox27_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker4.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                form.dateTimePicker9.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox21);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
        }

        public void dateTimePicker4_KeyDown(KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox27);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                form.textBox28.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!Arhiv)
                    form.textBox28.Focus();
                else
                    SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox21);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var date = new DateForm(form.dateTimePicker4);
                date.Show();
                e.Handled = true;
            }
        }

        public void textBox28_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker4.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Left)
            {
                form.dateTimePicker9.Focus();
                e.Handled = true;
            }

        }

        public void button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down)
            {
                SelectButton(1 , false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    ErrorHelper.StatusWait();

                    if (!Arhiv)
                    {
                        var result = TasksService.UpdateTask(Code,
                            TextBoxElement.New(form.textBox24, form.label51),
                            TextBoxElement.New(form.textBox23, form.label53),
                            TextBoxElement.New(form.textBox27, form.label55),
                            DateTimeElement.New(form.dateTimePicker4, form.label56));

                        if(result)
                        {
                            ErrorHelper.StatusComlite();
                            Exit();
                        }
                        else
                        {
                            ErrorHelper.StatusError();
                        }
                    }    
                    else
                    {

                    }
                }
                else
                {
                    ErrorHelper.StatusQuery();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button10);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(2 , true);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectButton(2 , false);
               
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    ErrorHelper.StatusWait();

                    if (!Arhiv)
                    {
                        var result = TasksService.Close(Code, TextBoxElement.New(form.textBox28, form.label57) , DateTimeElement.New(form.dateTimePicker9 , form.label38));

                        if(result)
                        {
                            ErrorHelper.StatusComlite();
                            Exit();
                        }
                        else
                        {
                            ErrorHelper.StatusError();
                        }
                    }
                    else
                    {
                        var result = ArhivService.Open(Code);

                        if(result)
                        {
                            ErrorHelper.StatusComlite();
                            Exit();
                        }
                        else
                        {
                            ErrorHelper.StatusError();
                        }
                    }

                }
                else
                {
                    ErrorHelper.StatusQuery();
                }

               
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button11);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(3 , true);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button12);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    ErrorHelper.StatusWait();

                    var result = TasksService.Delete(Code);

                    if(result)
                    {
                        ErrorHelper.StatusComlite();
                        Exit();
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
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectButton(3, false);
            }

            e.IsInputKey = true;
        }

        public void dateTimePicker9_KeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox28);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == SystemData.ActionKey)
            {
                var f = new DateForm(form.dateTimePicker9);
                f.Show();
                e.Handled = true;
            }

        }

        public void button47_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(4, true);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectButton(4, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SystemData.Pages.EditPret.Init(false);
                SystemData.Pages.EditPret.Show(Code, 1, Page, Arhiv);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button47);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void button48_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(5, true);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SystemData.Pages.EditPret.Init(false);
                SystemData.Pages.EditPret.Show(Code, 2, Page, Arhiv);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button48);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }
        #endregion
    }
}
