using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;
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
        public static List<TaskEntity> GetList(string search, string isp, bool isDate , DateTime dateFrom, DateTime dateTo)
        {
            var list = GetList();

            if (isDate)
                list = list.Where(c => c.Date.Date >= dateFrom.Date && c.Date.Date <= dateTo.Date).ToList();

            if (!string.IsNullOrEmpty(search))
                list = list.Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(isp) && int.TryParse(isp, out int i))
                list = list.Where(c => c.IdCon == Convert.ToInt32(isp) || c.IdIsp == Convert.ToInt32(isp)).ToList();

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

            if (!isUpdate && GetTaskByCode(Code.Value , GetList()) != null)
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
           
            var list1 = GetList().Where(c => c.IdIsp == code || c.IdCon == code).ToList();
            var list2 = ArhivService.GetList().Where(c => c.IdIsp == code || c.IdCon == code).ToList();

            foreach (var item in list1)
            {
                int isp = item.IdIsp, con = item.IdCon;

                if (item.IdIsp == code)
                {
                    isp = newCode;
                }

                if (item.IdCon == code)
                {
                    con = newCode;
                }
               
                var entity = new TaskEntity(item.Code, isp, con, item.Date);

                if(!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            foreach (var item in list2)
            {
                int isp = item.IdIsp, con = item.IdCon;

                if (item.IdIsp == code)
                {
                    isp = newCode;
                }

                if (item.IdCon == code)
                {
                    con = newCode;
                }

                var entity = new ArhivEntity(item.Code, isp, con, item.Date, item.DateClose, item.Otm);

                if (!context.Update(entity , entity.Code))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Возращает задачу по коду
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns></returns>
        public static TaskEntity GetTaskByCode(string code , List<TaskEntity> list)
        {
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
            var result = Validation(Code, IdIsp, IdCon, date , true);

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
        /// <param name="Date">Дата закрытия</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool Close(string code , TextBoxElement Otm , DateTimeElement Date)
        {
            if (Otm.IsNull || !Otm.IsNumber)
            {
                Otm.Error("Некоректные данные");
                return false;
            }    

            if (Convert.ToInt32(Otm.Value) > 5 || Convert.ToInt32(Otm.Value) < 1)
            {
                Otm.Error("Число должно быть от 1 до 5");
                return false;
            }

            var task = GetTaskByCode(code , GetList());
           
            var arhiv = new ArhivEntity(task.Code, task.IdIsp, task.IdCon, task.Date, Date.Value, Convert.ToInt32(Otm.Value));

            var result = context.Add(arhiv);

            if (!result)
                return false;

            return context.Delete(task);

        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns></returns>
        public static bool Delete(string code)
        {
            var task = GetTaskByCode(code, GetList());
            var result = context.Delete(task);
            return result;
        }

    }
}
