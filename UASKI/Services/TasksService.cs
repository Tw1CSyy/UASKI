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
        /// <summary>
        /// Валидация полей перед добавлением
        /// </summary>
        /// <param name="code">ТекстБокс с кодом задания</param>
        /// <param name="IdIsp">ТекстБокс с номером исполнителя</param>
        /// <param name="IdCon">ТекстБокс с номером контролера</param>
        /// <param name="date">ДатаПикер с сроком исполнения</param>
        /// <returns>true - успешная проверка</returns>
        private static bool AddCheck(string code, string IdIsp, string IdCon, DateTime date)
        {
            var context = new UAContext();
            
            if(string.IsNullOrEmpty(IdIsp))
            {
                MessageHelper.Error("Укажите исполнителя");
                return false;
            }

            if(string.IsNullOrEmpty(IdCon))
            {
                MessageHelper.Error("Укажите контролера");
                return false;
            }

            if(string.IsNullOrEmpty(code))
            {
                MessageHelper.Error("Укажите код задания");
                return false;
            }

            var task = context.Tasks.FirstOrDefault(c => c.Code.Equals(code));

            if (task != null)
            {
                MessageHelper.Error("Задача с данным кодом уже была добавлена");
                return false;
            }

            if(date < DateTime.Today)
            {
                MessageHelper.Error("Дата не может быть раньше текущей");
                return false;
            }    

            var dat = context.Holidays.FirstOrDefault(c => c.Date == date);

            if(dat != null)
            {
                MessageHelper.Error("Срок исполнения не может быть празднечным днем");
                return false;
            }

            return true;
        }


        /// <summary>
        /// Добавляет новую задачу
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="IdIsp">Номер исполнителя</param>
        /// <param name="IdCon">Номер контролера</param>
        /// <param name="date">Срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool Add(string code , string IdIsp , string IdCon , DateTime date)
        {
            if(AddCheck(code , IdIsp , IdCon , date))
            {
                var context = new UAContext();

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

            return false;
        }
    }
}
