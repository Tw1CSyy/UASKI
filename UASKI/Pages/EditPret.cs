using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using UASKI.Core.Models;
using UASKI.Forms;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;
using UASKI.StaticModels;

namespace UASKI.Pages
{
    public class EditPret : BasePageEdit
    {
        public EditPret(int index) : base(index) { }

        protected override void Show()
        {
            
        }

        private int IdTask;
        private int Type;
        private bool IsArhiv;
        private int IdPret;

        public void Show(int idTask , int type , BasePageSelect page , bool isArhiv , string codeTask)
        {
            IdTask = idTask;
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

            SelectTextBox(form.textBox38);
        }

        public void Show(int idPret, int type, BasePageSelect page)
        {
            IdPret = idPret;
            Type = type;
            Page = page;
            
            var pret = PretModel.GetById(idPret);
            var task = TaskModel.GetById(pret.IdTask);

            if(task != null)
            {
                if (type == 1)
                    form.label102.Text = $"Претензия на задачу {task.Code}";
                else if (type == 2)
                    form.label102.Text = $"Рецензия на задачу {task.Code}";
            }
            else
            {
                var arhiv = ArhivModel.GetById(pret.IdTask);

                if (type == 1)
                    form.label102.Text = $"Претензия на архивную задачу {arhiv.Code}";
                else if (type == 2)
                    form.label102.Text = $"Рецензия на архивную задачу {arhiv.Code}";
            }
           

            form.button46.Enabled = false;
            form.button45.Enabled = true;
            form.button44.Enabled = true;

            form.textBox38.Text = pret.Code;
            form.dateTimePicker16.Value = pret.Date;
            form.textBox39.Text = pret.Otm.ToString();

            SelectTextBox(form.textBox38);
        }

        protected override void Clear()
        {
            form.textBox38.Clear();
            form.textBox39.Clear();

            SelectButton(form.button44, false);
            SelectButton(form.button45, false);
            SelectButton(form.button46, false);
        }

        protected new void Exit()
        {
            if(IdPret == 0)
            {
                SystemData.Pages.EditTask.Init(false);
                SystemData.Pages.EditTask.Show(IdTask, IsArhiv, Page);
            }
            else
            {
                base.Exit();
            }
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
                SelectTextBox(form.textBox39);
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectTextBox(form.textBox38);
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

        public bool button46_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox38);
                SelectButton(form.button46, false);
            }
            else if(e.KeyCode == Keys.Down)
            {
                SelectButton(1, false);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var code = TextBoxElement.New(form.textBox38, form.label92);
                    var date = DateTimeElement.New(form.dateTimePicker16, form.label93);
                    var otm = TextBoxElement.New(form.textBox39, form.label94);

                    var result = ValidationHelper.PretValidation(code, date, otm);

                    if (!result)
                    {
                        Ai.Error();
                        return false;
                    }

                    var item = new PretModel(code.Value, IdTask, date.Value, otm.Num, Type);
                    result = item.Add();

                    if (!result)
                    {
                        Ai.AppError();
                        return false;
                    }

                    Ai.Comlite($"Успешно добавлена претензия/рецензия с номером {code.Value}");
                    Exit();
                }
                else
                    Ai.Query();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
            return true;
        }

        public bool button45_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox38);
                SelectButton(form.button45, false);
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
                    var code = TextBoxElement.New(form.textBox38, form.label92);
                    var date = DateTimeElement.New(form.dateTimePicker16, form.label93);
                    var otm = TextBoxElement.New(form.textBox39, form.label94);

                    var result = ValidationHelper.PretValidation(code, date, otm);

                    if (!result)
                    {
                        Ai.Error();
                        return false;
                    }

                    var item = new PretModel(code.Value, IdTask, date.Value, otm.Num, Type);
                    result = item.Update(IdPret);

                    if (!result)
                    {
                        Ai.AppError();
                        return false;
                    }

                    Ai.Comlite($"Успешно изменена претензия/рецензия с номером {code.Value}");
                    Exit();
                }
                else
                    Ai.Query();

            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
            return true;
        }

        public bool button44_PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SelectTextBox(form.textBox38);
                SelectButton(form.button44, false);
            }
            else if(e.KeyCode == Keys.Up)
            {
                SelectButton(3, true);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                if (SystemData.IsQuery)
                {
                    var entity = PretModel.GetById(IdPret);
                    var result = entity.Delete();


                    if (!result)
                    {
                        Ai.AppError();
                        return false;
                    }

                    Ai.Comlite($"Успешно удалена претензия/рецензия с номером {entity.Code}");
                    Exit();
                }
                else
                    Ai.Query();
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Exit();
            }

            e.IsInputKey = true;
            return true;
        }

        #endregion
    }
}
