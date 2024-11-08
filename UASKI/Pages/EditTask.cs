using System.Windows.Forms;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class EditTask : BasePage
    {
        private Gl_Form form = SystemData.Form;
        public EditTask (int index) : base(index) { }

        protected override void Show()
        {

        }

        public void Show(string code , bool IsArhiv)
        {
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
                form.label54.Text = task.Code.ToString();
                form.label54.Enabled = true;

                form.textBox27.Text = task.Code;
                form.dateTimePicker4.Value = task.Date;

                form.button11.Enabled = form.button12.Enabled = true;
                form.button11.Text = "Закрыть";

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
                form.label54.Text = arhiv.Code.ToString();
                form.label54.Enabled = false;

                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button12.Enabled = true;
                form.button11.Text = "Открыть";

                form.textBox28.Text = arhiv.Otm.ToString();

                SystemHelper.SelectTextBox(form.textBox26);
            }
        }

        public override void Clear()
        {
            form.textBox24.Clear();
            form.textBox25.Clear();
            form.textBox26.Clear();
            form.textBox27.Clear();
            form.textBox28.Clear();
            form.button11.Enabled = form.button12.Enabled = false;
            SystemHelper.SelectButton(false, form.button10);
            SystemHelper.SelectButton(false, form.button11);
            SystemHelper.SelectButton(false, form.button12);
        }

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
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
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
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
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
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
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
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (!form.label54.Enabled)
                    form.textBox28.Focus();
                else
                    SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
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
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
            }

        }

        public void button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button10);
                SystemHelper.SelectButton(true, form.button11);
            }
            else if(e.KeyCode == Keys.Enter)
            {

            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button10);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
            }
        }

        public void button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(true, form.button10);
                SystemHelper.SelectButton(false, form.button11);
            }
            else if(e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(false, form.button11);
                SystemHelper.SelectButton(true, form.button12);
            }
            else if(e.KeyCode == Keys.Enter)
            {

            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button11);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
            }
        }

        public void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;

            if(e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectButton(false, form.button12);
                SystemHelper.SelectButton(true, form.button11);
            }
            else if(e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectButton(false, form.button12);
                SystemHelper.SelectTextBox(form.textBox26);
            }
            else if(e.KeyCode == Keys.Enter)
            {

            }
            else if(e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    SystemData.Pages.SelectTask.Init();
                else
                    SystemData.Pages.SelectArhiv.Init();
            }
        }
    }
}
