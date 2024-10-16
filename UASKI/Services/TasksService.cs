using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Helpers;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    public static class TasksService
    {
        private static UAContext context = new UAContext();

        /// <summary>
        /// Добавляет новую задачу, предварительно валидируя
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="IdIsp">Номер исполнителя</param>
        /// <param name="IdCon">Номер контролера</param>
        /// <param name="date">Срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool Add(TextBoxElement Code , TextBoxElement IdIsp , TextBoxElement IdCon , DateTimeElement date)
        {
            var result = true;

            Code.Dispose();
            IdIsp.Dispose();
            IdCon.Dispose();
            date.Dispose();

            if (string.IsNullOrEmpty(IdIsp.Value))
            {
                ErrorHelper.Error("Поле не заполнено",IdIsp);
                result = false;
            }

            if (string.IsNullOrEmpty(IdCon.Value))
            {
                ErrorHelper.Error("Поле не заполнено", IdCon);
                result = false;
            }

            if (string.IsNullOrEmpty(Code.Value))
            {
                ErrorHelper.Error("Поле не заполнено", Code);
                result = false;
            }

            var task = context.Tasks.FirstOrDefault(c => c.Code.Equals(Code.Value));

            if (task != null && result)
            {
                ErrorHelper.Error("Код задачи должен быть уникальным", Code);
                result = false;
            }

            if (date.Value < DateTime.Today)
            {
                ErrorHelper.Error("Срок исполнения не может быть раньше текущей даты", date);
                result = false;
            }

            var dat = context.Holidays.FirstOrDefault(c => c.Date == date.Value);

            if (dat != null)
            {
                ErrorHelper.Error("Срок исполнения не может быть празднечным днем", date);
                result = false;
            }

            if(result)
            {
                var model = new TaskEntity
                (
                 Code.Value,
                 Convert.ToInt32(IdIsp.Value),
                 Convert.ToInt32(IdCon.Value),
                 date.Value,
                 false
                );
                return context.Add(model);
            }

            return false;
           
        }
    }
}
