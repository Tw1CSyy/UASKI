using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;
using UASKI.Helpers;
using UASKI.Models;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    /// <summary>
    /// Сервис для работы с таблицей Tasks
    /// </summary>
    public static class TasksService
    {
        private static readonly UAContext context = new UAContext();

        /// <summary>
        /// Возращает список задач
        /// </summary>
        public static List<TaskEntity> GetList()
        {
            return context.Tasks;
        }


        /// <summary>
        /// Возращает список задач
        /// </summary>
        /// <param name="search">Код задачи</param>
        /// <param name="isp">Код котроллера или исполнителя</param>
        /// <param name="podr">Код подразделения</param>
        /// <param name="isDate">Используется ли дата</param>
        /// <param name="dateFrom">Дата от</param>
        /// <param name="dateTo">Дата до</param>
        public static List<TaskEntity> GetList(string search, string isp, string podr , bool isDate , DateTime dateFrom , DateTime dateTo)
        {
            var list = GetList();

            if (isDate)
                list = list.Where(c => c.Date.Date >= dateFrom.Date && c.Date.Date <= dateTo.Date).ToList();

            if (!string.IsNullOrEmpty(search))
                list = list.Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(isp) && int.TryParse(isp, out int i))
                list = list.Where(c => c.IdCon == Convert.ToInt32(isp) || c.IdIsp == Convert.ToInt32(isp)).ToList();

            if(!string.IsNullOrEmpty(podr) && int.TryParse(podr , out int j))
            {
                var users = IspService.GetList().Where(c => c.CodePodr == Convert.ToInt32(podr)).ToList();
                var list2 = new List<TaskEntity>();

                foreach (var user in users)
                {
                    var items = list.Where(c => c.IdCon == user.Code || c.IdIsp == user.Code).ToList();

                    if(items.Any())
                        list2.AddRange(items);
                }

                list = list2;
                
            }

            return list;
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
            var result = Validation(Code, IdIsp, IdCon, date);

            if(!result)
                return false;

            var model = new TaskEntity
                (
                 Code.Value,
                 Convert.ToInt32(IdIsp.Value),
                 Convert.ToInt32(IdCon.Value),
                 date.Value
                );

            return context.Add(model);

        }

        /// <summary>
        /// Валидация задачи
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="IdIsp">Номер исполнителя</param>
        /// <param name="IdCon">Номер контролера</param>
        /// <param name="date">Срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        private static bool Validation(TextBoxElement Code, TextBoxElement IdIsp, TextBoxElement IdCon, DateTimeElement date , bool isUpdate = false)
        {
            var result = true;

            Code.Dispose();
            IdIsp.Dispose();
            IdCon.Dispose();
            date.Dispose();

            if (IdIsp.IsNull)
            {
                IdIsp.Error("Поле не заполнено");
                result = false;
            }

            if (IdCon.IsNull)
            {
                IdCon.Error("Поле не заполнено");
                result = false;
            }

            if (Code.IsNull)
            {
                Code.Error("Поле не заполнено");
                result = false;
            }

            if (!isUpdate && GetTaskByCode(Code.Value) != null)
            {
                Code.Error("Код задачи должен быть уникальным");
                result = false;
            }

            if (!isUpdate && date.Value < DateTime.Today)
            {
                date.Error("Срок исполнения не может быть раньше текущей даты");
                result = false;
            }

            if (HolidaysService.CheckDay(date.Value))
            {
                date.Error("Срок исполнения не может быть празднечным днем");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Изменяет код исполнителей и котроллеров в таблицах
        /// </summary>
        /// <param name="code">Код исполнителя</param>
        /// <param name="newCode">Новый код исполнителя</param>
        /// <returns></returns>
        public static bool EditIsp(int code , int newCode)
        {
            var tasks = GetList();
            var arhiv = ArhivService.GetList();

            var list1 = tasks.Where(c => c.IdIsp == code).ToList();
            var list2 = tasks.Where(c => c.IdCon == code).ToList();
            var list3 = arhiv.Where(c => c.IdIsp == code).ToList();
            var list4 = arhiv.Where(c => c.IdCon == code).ToList();

            foreach (var item in list1)
            {
                var entity = new TaskEntity(item.Code, newCode, item.IdCon, item.Date);

                if(!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            foreach (var item in list2)
            {
                var entity = new TaskEntity(item.Code, item.IdIsp, newCode, item.Date);

                if (!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            foreach (var item in list3)
            {
                var entity = new ArhivEntity(item.Code, newCode, item.IdCon, item.Date , item.DateClose , item.Otm);

                if (!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            foreach (var item in list4)
            {
                var entity = new ArhivEntity(item.Code, item.IdIsp, newCode, item.Date, item.DateClose, item.Otm);

                if (!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Формирует модель для вывода в DataGridView
        /// </summary>
        /// <param name="list">Список объектов</param>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid(List<TaskEntity> list)
        {
            var model = new List<DataGridRowModel>();

            foreach (var item in list.OrderByDescending(c => c.Date))
            {
                var isp = IspService.GetByCode(item.IdIsp);
                var con = IspService.GetByCode(item.IdCon);

                var st = new DataGridRowModel(item.Code, $"{isp.FirstName} {isp.Name} {isp.LastName}", $"{con.FirstName} {con.Name} {con.LastName}", item.Date.ToString("dd.MM.yyyy"));
                model.Add(st);
            }

            return model;
        }

        /// <summary>
        /// Возращает задачу по коду
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns></returns>
        public static TaskEntity GetTaskByCode(string code)
        {
            var list = GetList();
            var item = list.FirstOrDefault(c => c.Code.Equals(code));

            return item;
        }

        /// <summary>
        /// Изменяет задачу, предварительно валидируя
        /// </summary>
        /// <param name="code">Код текущей задачи</param>
        /// <param name="IdIsp">Код исполнителя</param>
        /// <param name="IdCon">Код котроллера</param>
        /// <param name="Code">Новый код задачи</param>
        /// <param name="date">Дата срока</param>
        /// <returns>true - успешная операция</returns>
        public static bool UpdateTask(string code, TextBoxElement IdIsp, TextBoxElement IdCon, TextBoxElement Code, DateTimeElement date)
        {
            var result = Validation(Code, IdIsp, IdCon, date);

            if (!result)
                return false;

            var entity = new TaskEntity(Code.Value, Convert.ToInt32(IdIsp.Value), Convert.ToInt32(IdCon.Value), date.Value);
            return context.Update(entity, code);
        }

        /// <summary>
        /// Удаляет задачу из Task и добавляет в Arhiv
        /// </summary>
        /// <param name="code">Код задания</param>
        /// <param name="Otm">Отметка задания</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool Close(string code , TextBoxElement Otm)
        {
            if (Otm.IsNull || !Otm.IsNumber || Otm.Value.Length > 1)
            {
                Otm.Error("Некоректные данные");
                ErrorHelper.StatusError();
                return false;
            }    

            if (Convert.ToInt32(Otm.Value) > 5 || Convert.ToInt32(Otm.Value) < 1)
            {
                Otm.Error("Число должно быть от 1 до 5");
                ErrorHelper.StatusError();
                return false;
            }

            var task = GetTaskByCode(code);
            var arhiv = new ArhivEntity(task.Code, task.IdIsp, task.IdCon, task.Date, DateTime.Now, Convert.ToInt32(Otm.Value));

            var result = context.Add(arhiv);

            if (!result)
                return false;

            return context.Delete(task);

        }
    }
}
