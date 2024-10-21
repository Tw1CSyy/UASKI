using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    public static class TasksService
    {
        private static UAContext context = new UAContext();

        /// <summary>
        /// Возращает список задач
        /// </summary>
        /// <returns></returns>
        private static List<TaskEntity> GetListTask()
        {
            return context.Tasks;
        }

        /// <summary>
        /// Вохращает список архива
        /// </summary>
        /// <returns></returns>
        private static List<ArhivEntity> GetListArhiv()
        {
            return context.Arhiv;
        }

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

            if (task != null)
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
                 date.Value
                );
                return context.Add(model);
            }

            return false;
           
        }

        /// <summary>
        /// Изменяет код исполнителей и котроллеров в таблицах
        /// </summary>
        /// <param name="code">Код исполнителя</param>
        /// <param name="newCode">Новый код исполнителя</param>
        /// <returns></returns>
        public static bool EditIsp(int code , int newCode)
        {
            var tasks = GetListTask();
            var arhiv = GetListArhiv();

            var list1 = tasks.Where(c => c.IdIsp == code).ToList();
            var list2 = tasks.Where(c => c.IdCon == code).ToList();
            var list3 = arhiv.Where(c => c.IdIsp == code).ToList();
            var list4 = arhiv.Where(c => c.IdCon == code).ToList();

            foreach (var item in list1)
            {
                var entity = new TaskEntity(item.Code, newCode, item.IdCon, item.Date);

                if(!context.Update(entity))
                {
                    return false;
                }
            }

            foreach (var item in list2)
            {
                var entity = new TaskEntity(item.Code, item.IdIsp, newCode, item.Date);

                if (!context.Update(entity))
                {
                    return false;
                }
            }

            foreach (var item in list3)
            {
                var entity = new ArhivEntity(item.Code, newCode, item.IdCon, item.Date , item.DateClose , item.Otm , item.Num);

                if (!context.Update(entity))
                {
                    return false;
                }
            }

            foreach (var item in list4)
            {
                var entity = new ArhivEntity(item.Code, item.IdIsp, newCode, item.Date, item.DateClose, item.Otm, item.Num);

                if (!context.Update(entity))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Формирует модель для вывода в DataGridView
        /// </summary>
        /// <param name="search">Строка поиска</param>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid(string search)
        {
            var model = new List<DataGridRowModel>();

            var list = GetListTask();

            if(!string.IsNullOrEmpty(search))
            {
                list = list.Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();
            }

            foreach (var item in list.OrderByDescending(c => c.Date))
            {
                var isp = IspService.GetByCode(item.IdIsp);
                var con = IspService.GetByCode(item.IdCon);

                var st = new DataGridRowModel(item.Code, $"{isp.FirstName} {isp.Name} {isp.LastName}", $"{con.FirstName} {con.Name} {con.LastName}", item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            return model;
        }
    }
}
