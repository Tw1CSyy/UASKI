using System;
using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class EditIsp : BasePage
    {
        private Gl_Form form = SystemData.Form;

        public override void Show()
        {
            
        }

        public void Show(int code)
        {
            var form = SystemData.Form;

            var isp = IspService.GetByCode(code);

            form.textBox18.Text = isp.FirstName;
            form.textBox17.Text = isp.Name;
            form.textBox16.Text = isp.LastName;
            form.textBox15.Text = isp.Code.ToString();
            form.textBox14.Text = isp.CodePodr.ToString();
            form.label38.Text = isp.Code.ToString();

            SystemHelper.PullListInDataGridView(form.dataGridView4,
                 TasksService.GetListByDataGrid(TasksService.GetList(code)),
                 new DataGridRowModel("Код", "Исполнитель", "Контроллер", "Срок"));

            SystemHelper.SelectTextBox(form.textBox18);
        }

        public override void Clear()
        {
            form.textBox18.Clear();
            form.textBox17.Clear();
            form.textBox16.Clear();
            form.textBox15.Clear();
            form.textBox14.Clear();

            form.dataGridView4.DataSource = null;
            SystemHelper.SelectButton(false, form.button6);
            SystemHelper.SelectButton(false, form.button7);
            form.textBox18.Focus();
        }

        public void textBox18_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                SystemHelper.SelectTextBox(form.textBox16);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = IspService.Update
                   (
                   Convert.ToInt32(form.label38.Text),
                   TextBoxElement.New(form.textBox18, form.label32),
                   TextBoxElement.New(form.textBox17, form.label31),
                   TextBoxElement.New(form.textBox16, form.label30),
                   TextBoxElement.New(form.textBox15, form.label29),
                   TextBoxElement.New(form.textBox14, form.label28)
                   );

                    if (result)
                    {
                        NavigationHelper.ClearForm();
                        NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                    var code = Convert.ToInt32(form.label38.Text);

                    if (IspService.Disactive(code))
                    {
                        NavigationHelper.ClearForm();
                        NavigationHelper.GetIspSelectView();
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
                NavigationHelper.GetIspSelectView();
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
                SystemHelper.SelectTextBox(form.textBox18);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SystemHelper.SelectDataGridView(false, form.dataGridView4);
                SystemHelper.SelectTextBox(form.textBox18);
                e.Handled = true;
            }
        }
    }
}
