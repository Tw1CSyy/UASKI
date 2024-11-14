using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;
using UASKI.Models.Elements;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы редактирования задачи
    /// </summary>
    public class EditTask : BasePage
    {
        /// <summary>
        /// Базовый конструктор для установки индекса страницы
        /// </summary>
        /// <param name="index">Индекс страницы</param>
        public EditTask(int index) : base(index) { }

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

        private string Code;
        private int Page;
        public  bool Arhiv { get; private set; }

        /// <summary>
        /// Загружает данные на страницу
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="IsArhiv">Архивное задание</param>
        /// <param name="page">Прошлая страница</param>
        public void Show(string code , bool IsArhiv , int page)
        {
            Code = code;
            Page = page;
            Arhiv = IsArhiv;

            if (!IsArhiv)
            {
                var task = TasksService.GetTaskByCode(code);

                var usr1 = IspService.GetByCode(task.IdIsp);
                var usr2 = IspService.GetByCode(task.IdCon);

                form.textBox26.Text = $"{usr1.FirstName} {usr1.Name.ToUpper()[0]}. {usr1.LastName.ToUpper()[0]}.";
                form.textBox21.Text = $"{usr2.FirstName} {usr2.Name.ToUpper()[0]}. {usr2.LastName.ToUpper()[0]}.";

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.button12.Enabled = false;
                form.button11.Enabled = false;
                form.textBox27.Text = task.Code;
                form.dateTimePicker4.Value = task.Date;

                form.button11.Text = "Закрыть";
                form.label72.Visible = false;
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else
            {
                var arhiv = ArhivService.GetByCode(code);

                var usr1 = IspService.GetByCode(arhiv.IdIsp);
                var usr2 = IspService.GetByCode(arhiv.IdCon);

                form.textBox26.Text = usr1.FirstName;
                form.textBox21.Text = usr2.FirstName;

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.button11.Enabled = true;
                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button11.Text = "Открыть";
                form.button10.Enabled = false;

                form.textBox28.Text = arhiv.Otm.ToString();
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

            form.label51.Visible = false;
            form.label53.Visible = false;
            form.label55.Visible = false;
            form.label56.Visible = false;
            form.label57.Visible = false;

            SystemHelper.SelectButton(false, form.button10);
            SystemHelper.SelectButton(false, form.button11);
            SystemHelper.SelectButton(false, form.button12);
        }

        /// <summary>
        /// Выход с страницы
        /// </summary>
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

        #region Клавиши
        public void textBox26_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox25.Text.Length == 0 && form.textBox24.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox26.Text);

                    if (isp != null)
                    {
                        form.textBox25.Text = isp.CodePodr.ToString();
                        form.textBox24.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox21);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox21);
                }
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var ispForm = new IspForm(form.textBox26, form.textBox25, form.textBox24);
                ispForm.Show();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
                else if (form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Back)
            {
                form.textBox26.Clear();
                form.textBox25.Clear();
                form.textBox24.Clear();
            }
        }

        public void textBox21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox22.Text.Length == 0 && form.textBox23.Text.Length == 0)
                {
                    var isp = IspService.GetByFirstName(form.textBox21.Text);

                    if (isp != null)
                    {
                        form.textBox22.Text = isp.CodePodr.ToString();
                        form.textBox23.Text = isp.Code.ToString();
                    }

                    SystemHelper.SelectTextBox(form.textBox27);
                }
                else
                {
                    SystemHelper.SelectTextBox(form.textBox27);
                }
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var ispForm = new IspForm(form.textBox21, form.textBox22, form.textBox23);
                ispForm.Show();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
                else if (form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Back)
            {
                form.textBox21.Clear();
                form.textBox22.Clear();
                form.textBox23.Clear();
            }
        }

        public void textBox27_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker4.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                form.textBox28.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox21);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void dateTimePicker4_KeyDown(KeyEventArgs e)
        {
            e.Handled = true;

            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if (e.KeyCode == Keys.Down)
            {
                form.textBox28.Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
                else if (form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!Arhiv)
                    form.textBox28.Focus();
                else if (form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
                else if (form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
            else if (e.KeyCode == SystemData.ActionKey)
            {
                var date = new DateForm(form.dateTimePicker4);
            }
        }

        public void textBox28_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker4.Focus();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                if(form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
                else if(form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

        }

        public void button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button10);

                if (form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else
                    SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    if(!Arhiv)
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
        }

        public void button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button11);

                if(form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button11);
                SystemHelper.SelectButton(true, form.button12);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(SystemData.IsQuery)
                {
                    if (!Arhiv)
                    {
                        var result = TasksService.Close(Code, TextBoxElement.New(form.textBox28, form.label57));

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
                SystemHelper.SelectButton(false, form.button11);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }

        public void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button12);
               
                if(form.button11.Enabled)
                    SystemHelper.SelectButton(true, form.button11);
                else if(form.button10.Enabled)
                    SystemHelper.SelectButton(true, form.button10);
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
                    if (!Arhiv)
                    {

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
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }
        }
        #endregion
    }
}
