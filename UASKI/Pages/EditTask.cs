using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.StaticModels;
using UASKI.Enums;

namespace UASKI.Pages
{
    /// <summary>
    /// Класс для объекта страницы редактирования задачи
    /// </summary>
    public class EditTask : BasePageEdit
    {

        public EditTask(int index, TypePage type) : base(index, type) { }

        protected override void Show()
        {

        }

        protected override void Exit()
        {
            if (Buffer != null && Buffer.Count != 0)
            {
                Buffer.RemoveAt(0);
                Show(Buffer);
            }
            else
            {
                Ai.OpenLastPage();
            }
        }

        private TaskModel Task;
        private ArhivModel Arhiv;
        private List<int> Buffer;

        private string CodeTask;
        public bool IsArhiv { get; private set; }

        public void Show(int id, bool isArhiv)
        {
            IsArhiv = isArhiv;

            SelectButton(form.button10, false);
            SelectButton(form.button11, false);
            SelectButton(form.button12, false);
            SelectButton(form.button47, false);
            SelectButton(form.button48, false);

            var listUser = IspModel.GetList();

            if (!IsArhiv)
            {
                var task = TaskModel.GetById(id);

                var usr1 = task.GetIsp(listUser);
                var usr2 = task.GetCon(listUser);
                Task = task;

                form.textBox25.Text = usr1.InizNotCode;
                form.textBox22.Text = usr2.InizNotCode;

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox26.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox21.Text = usr2.CodePodr.ToString();
                form.textBox27.Text = CodeTask = task.Code;
                form.dateTimePicker4.Value = task.Date;

                form.button11.Text = "Закрыть";
                form.button10.Enabled = true;
                form.button11.Enabled = false;
                form.button12.Enabled = true;
                form.label72.Visible = false;
                form.checkBox7.Checked = task.IsDouble;
                SelectTextBox(form.textBox26);
            }
            else
            {
                var listArhiv = ArhivModel.GetList();
                var arhiv = ArhivModel.GetById(id);
                var usr1 = arhiv.GetIsp(listUser);
                var usr2 = arhiv.GetCon(listUser);

                Arhiv = arhiv;

                form.textBox25.Text = usr1.InizNotCode;
                form.textBox22.Text = usr2.InizNotCode;

                form.textBox24.Text = usr1.Code.ToString();
                form.textBox26.Text = usr1.CodePodr.ToString();
                form.textBox23.Text = usr2.Code.ToString();
                form.textBox21.Text = usr2.CodePodr.ToString();
                form.textBox27.Text = arhiv.Code;
                form.dateTimePicker4.Value = arhiv.Date;

                form.button11.Text = "Открыть";
                form.button10.Enabled = true;
                form.button11.Enabled = true;
                form.button12.Enabled = false;
                form.checkBox7.Checked = arhiv.IsDouble;
                form.textBox28.Text = arhiv.Otm.ToString();
                form.dateTimePicker9.Value = arhiv.DateClose.Date;
                form.label72.Visible = true;
                SelectTextBox(form.textBox26);
            }
        }

        public void Show(List<int> buffer)
        {
            if (buffer.Count == 0)
                Exit();
            else
            {
                Buffer = buffer;
                form.textBox28.Clear();

                var isArhiv = TaskModel.GetList().FirstOrDefault(c => c.Id == buffer[0]) == null;

                if(isArhiv)
                    Show(buffer[0], isArhiv);
                else
                    Show(buffer[0], isArhiv);

                form.dateTimePicker9.Focus();
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

            SelectButton(form.button10, false);
            SelectButton(form.button11, false);
            SelectButton(form.button12, false);
            SelectButton(form.button47, false);
            SelectButton(form.button48, false);
        }

        private void SelectButton(int idButton = 0, bool Up = false)
        {
            var list = new Button[] { form.button10, form.button11, form.button12, form.button47, form.button48 };
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
                        SelectButton(item, false);

                    SelectButton(list[index]);
                    break;
                }

            }
        }

        public override bool AiKeyDown(KeyEventArgs key)
        {
            return false;
        }

        #region Клавиши
        public void textBox26_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox21);
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                var ispForm = new IspForm(form.textBox25, form.textBox26, form.textBox24);
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
        }

        public void textBox21_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                SelectTextBox(form.textBox27);
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
            {
                var ispForm = new IspForm(form.textBox22, form.textBox21, form.textBox23);
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
                SelectTextBox(form.textBox26);
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
                SelectTextBox(form.textBox21);
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
                SelectTextBox(form.textBox27);
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
                if (!IsArhiv)
                    form.textBox28.Focus();
                else
                    SelectButton();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox21);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if (e.KeyCode == Ai.ActionKey)
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
                SelectButton(1);
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
            else if(e.KeyCode == Keys.Down)
            {
                SelectCheckBox(form.checkBox7, true);
            }

        }

        public bool button10_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down)
            {
                SelectButton(1, false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(Ai.IsQuery)
                {

                    if (!IsArhiv)
                    {
                        var code = TextBoxElement.New(form.textBox27, form.label55);
                        var idIsp = TextBoxElement.New(form.textBox24, form.label51);
                        var idCon = TextBoxElement.New(form.textBox23, form.label53);
                        var date = DateTimeElement.New(form.dateTimePicker4, form.label56);

                        var result = ValidationHelper.TaskValidation(code, idIsp, idCon, date, true);

                        if(result == false)
                        {
                            Ai.Error();
                            return false;
                        }

                        var task = new TaskModel(code.Value, idIsp.Num, idCon.Num, date.Value, form.checkBox7.Checked);
                        result = task.Update(Task.Id);

                        if (result == false)
                        {
                            Ai.AppError();
                            return false;
                        }

                        Ai.Comlite($"Задача с кодом {Task.Code} изменена");
                        Exit();
                    }    
                    else
                    {
                        var code = TextBoxElement.New(form.textBox27, form.label55);
                        var idIsp = TextBoxElement.New(form.textBox24, form.label51);
                        var idCon = TextBoxElement.New(form.textBox23, form.label53);
                        var date = DateTimeElement.New(form.dateTimePicker4, form.label56);
                        var dateClose = DateTimeElement.New(form.dateTimePicker9, form.label38);
                        var otm = TextBoxElement.New(form.textBox28, form.label57);

                        var result = ValidationHelper.ArhivValidation(code, idIsp, idCon, date, dateClose, otm);

                        if (result == false)
                        {
                            Ai.Error();
                            return false;
                        }

                        var arhiv = new ArhivModel(code.Value, idIsp.Num, idCon.Num, date.Value, dateClose.Value, otm.Num, Arhiv.Id, form.checkBox7.Checked);
                        result = arhiv.Update();

                        if (result == false)
                        {
                            Ai.AppError();
                            return false;
                        }

                        Ai.Comlite($"Архивная задача с кодом {Arhiv.Code} изменена");
                        Exit();
                    }
                }
                else
                {
                    Ai.Query();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button10, false);
                SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
            return true;
        }

        public bool button11_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(2, true);
            }
            else if (e.KeyCode == Keys.Down)
            {
                SelectButton(2, false);
               
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(Ai.IsQuery)
                {
                    
                    if (!IsArhiv)
                    {
                        var dateClose = DateTimeElement.New(form.dateTimePicker9, form.label38);
                        var otm = TextBoxElement.New(form.textBox28, form.label57);

                        var result = ValidationHelper.CloseTaskValidation(otm, dateClose);

                        if(result == false)
                        {
                            Ai.Error();
                            return false;
                        }

                        result = Task.Close(otm.Num, dateClose.Value);

                        if (result == false)
                        {
                            Ai.AppError();
                            return false;
                        }

                        Ai.Comlite($"Задача перемещена в архив с оценкой {otm.Value}");
                        Exit();
                    }
                    else
                    {
                        var result = Arhiv.Open();

                        if(result)
                        {
                            Ai.Comlite($"Архивная задача была открыта c кодом {Arhiv.Code}");
                            Exit();
                        }
                        else
                        {
                            Ai.AppError();
                        }
                    }

                }
                else
                {
                    Ai.Query();
                }

               
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button11, false);
                SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
            return true;
        }

        public void button12_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SelectButton(3, true);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button12, false);
                SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (Ai.IsQuery)
                {
                    var result = Task.Delete();

                    if(result)
                    {
                        Ai.Comlite($"Удалена задача с кодом {Task.Code}");
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
                SelectTextBox(form.textBox28);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox27);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
                e.Handled = true;
            }
            else if(e.KeyCode == Ai.ActionKey)
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
                Ai.Pages.EditPret.Init(false);
                int id = 0;

                if (Task != null)
                    id = Task.Id;
                else
                    id = Arhiv.Id;

                Ai.Pages.EditPret.Show(id, 1, IsArhiv, CodeTask);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button47, false);
                SelectTextBox(form.textBox26);
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
                int id = 0;

                if (Task != null)
                    id = Task.Id;
                else
                    id = Arhiv.Id;

                Ai.Pages.EditPret.Init(false);
                Ai.Pages.EditPret.Show(id, 2, IsArhiv, CodeTask);
            }
            else if (e.KeyCode == Keys.Left)
            {
                SelectButton(form.button48, false);
                SelectTextBox(form.textBox26);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
        }

        public void checkBox7_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                form.checkBox7.Checked = !form.checkBox7.Checked;
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox28);
                SelectCheckBox(form.checkBox7, false);
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                SelectCheckBox(form.checkBox7, false);
                Exit();
                e.IsInputKey = true;
            }
            else if(e.KeyCode == Keys.Right)
            {
                SelectButton(form.button10);
                SelectCheckBox(form.checkBox7, false);
            }
        }
    }
        #endregion
}


