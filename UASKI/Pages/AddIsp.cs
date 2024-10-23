using System.Windows.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.Services;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class AddIsp : BasePage
    {
        private Gl_Form form = SystemData.Form;

        public override void Show()
        {
            SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid(IspService.GetList())
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));
            form.textBox8.Focus();
            SystemHelper.SelectDataGridView(false, form.dataGridView1);
        }

        public override void Clear()
        {
            SystemHelper.PullListInDataGridView(form.dataGridView1
                        , IspService.GetListByDataGrid(IspService.GetList())
                        , new DataGridRowModel("Табельный номер", "Фамилия", "Имя", "Отчество", "Код подразделения"));

            form.textBox8.Clear();
            form.textBox9.Clear();
            form.textBox10.Clear();
            form.textBox11.Clear();
            form.textBox12.Clear();

            form.textBox8.Focus();
            SystemHelper.SelectButton(false, form.button4);
        }

        public void textBox8_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox9);
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public void textBox9_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox10);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox8);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public void textBox10_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox11);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox9);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public void textBox11_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectTextBox(form.textBox12);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox10);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                SystemHelper.SelectButton(true, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }
        }

        public void textBox12_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SystemHelper.SelectButton(true, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectTextBox(form.textBox11);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
            }
        }

        public void button4_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var result = IspService.Add(
                    new TextBoxElement(form.textBox8, form.label18),
                    new TextBoxElement(form.textBox9, form.label19),
                    new TextBoxElement(form.textBox10, form.label20),
                    new TextBoxElement(form.textBox11, form.label21),
                    new TextBoxElement(form.textBox12, form.label22)
                    );

                    if (result)
                    {
                        NavigationHelper.ClearForm();
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
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectButton(false, form.button4);
            }
            else if (e.KeyCode == Keys.Up)
            {
                SystemHelper.SelectTextBox(form.textBox12);
                SystemHelper.SelectButton(false, form.button4);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SystemHelper.SelectDataGridView(true, form.dataGridView1);
            }

            e.IsInputKey = true;
        }

        public void dataGridView1_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                SystemHelper.SelectTextBox(form.textBox8);
                SystemHelper.SelectDataGridView(false, form.dataGridView1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                form.Menu_Step2.Enabled = true;
                form.Menu_Step2.Focus();
                SystemHelper.SelectDataGridView(false, form.dataGridView1);
                e.Handled = true;
            }
        }
    }
}
