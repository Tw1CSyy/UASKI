using System;
using System.Linq;
using System.Windows.Forms;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Helpers;
using UASKI.StaticModels;

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
        public static bool Add(string code , string IdIsp , string IdCon , DateTime date)
        {
            if (string.IsNullOrEmpty(IdIsp))
            {
                return false;
            }

            if (string.IsNullOrEmpty(IdCon))
            {
                return false;
            }

            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            var task = context.Tasks.FirstOrDefault(c => c.Code.Equals(code));

            if (task != null)
            {
                return false;
            }

            if (date < DateTime.Today)
            {
                return false;
            }

            var dat = context.Holidays.FirstOrDefault(c => c.Date == date);

            if (dat != null)
            {
                return false;
            }

            var model = new TaskEntity
               (
                  code,
                  Convert.ToInt32(IdIsp),
                  Convert.ToInt32(IdCon),
                  date,
                  false
               );

            return context.Add(model);

        }
    }
}
