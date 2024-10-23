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

        public override void Show()
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
            }
            else
            {
                var arhiv = TasksService.GetArhivByCode(code);

                var usr1 = IspService.GetByCode(arhiv.IdIsp);
                var usr2 = IspService.GetByCode(arhiv.IdCon);

                form.textBox26.Text = $"{usr1.FirstName} {usr1.Name.ToUpper()[0]}. {usr1.LastName.ToUpper()[0]}.";
                form.textBox21.Text = $"{usr2.FirstName} {usr2.Name.ToUpper()[0]}. {usr2.LastName.ToUpper()[0]}.";

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox25.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox22.Text = usr2.CodePodr.ToString();
                form.label54.Text = arhiv.Code.ToString();
                form.label54.Enabled = false;

                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button12.Enabled = true;

                form.dateTimePicker5.Value = arhiv.DateClose;
                form.textBox28.Text = arhiv.Otm.ToString();
                form.textBox29.Text = arhiv.Num.ToString();

                form.panel10.Visible = true;
            }
        }

        public override void Clear()
        {
            form.textBox24.Clear();
            form.textBox25.Clear();
            form.textBox26.Clear();
            form.textBox27.Clear();
            form.textBox28.Clear();
            form.textBox29.Clear();
            form.panel10.Visible = false;
            form.button11.Enabled = form.button12.Enabled = false;
            SystemHelper.SelectButton(false, form.button10);
            SystemHelper.SelectButton(false, form.button11);
            SystemHelper.SelectButton(false, form.button12);
        }

        public void textBox26_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox25.Text.Length != 0 && form.textBox24.Text.Length == 0)
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
            else if (e.KeyCode == KeyDownHelper.ActionKey)
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
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public void textBox21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (form.textBox22.Text.Length != 0 && form.textBox23.Text.Length == 0)
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
            else if (e.KeyCode == KeyDownHelper.ActionKey)
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
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox26);
            }
        }

        public void textBox27_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                form.dateTimePicker4.Focus();
            }
            else if (e.KeyCode == Keys.Down && form.panel10.Visible)
            {
                form.dateTimePicker5.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox21);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public void dateTimePicker4_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if (e.KeyCode == Keys.Down && form.panel10.Visible)
            {
                form.dateTimePicker5.Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (form.panel10.Visible)
                    form.dateTimePicker5.Focus();
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
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
            else if (e.KeyCode == KeyDownHelper.ActionKey)
            {
                var date = new DateForm(form.dateTimePicker4);
            }
        }

        public void dateTimePicker5_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox28);
            }
            else if (e.KeyCode == KeyDownHelper.ActionKey)
            {
                var dateForm = new DateForm(form.dateTimePicker5);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox27);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public void textBox28_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                form.dateTimePicker5.Focus();
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox29);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }

        }

        public void textBox29_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox28);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button10);
            }
            else if (e.KeyCode == Keys.Up)
            {
                form.dateTimePicker4.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (form.label54.Enabled)
                    NavigationHelper.GetTaskSelectView();
                else
                    NavigationHelper.GetArhivSelectView();
            }
        }

        public void button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }

        public void button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }

        public void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }
    }
}
