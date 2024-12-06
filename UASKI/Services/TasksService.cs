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
        /// <param name="idIsp">Элемент номер исполнителя</param>
        /// <param name="idCon">Элемент номер контролера</param>
        /// <param name="date">Элемент срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool Add(TextBoxElement code , TextBoxElement idIsp , TextBoxElement idCon , DateTimeElement date)
        {
            var result = Validation(code, idIsp, idCon, date);

            if(!result)
                return false;

            var model = new TaskEntity
                (
                 code.Value,
                 Convert.ToInt32(idIsp.Value),
                 Convert.ToInt32(idCon.Value),
                 date.Value
                );

            return context.Add(model);

        }

        /// <summary>
        /// Валидация задачи
        /// </summary>
        /// <param name="idIsp">Элемент номер исполнителя</param>
        /// <param name="idCon">Элемент номер контролера</param>
        /// <param name="date">Элемент срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        private static bool Validation(TextBoxElement code, TextBoxElement idIsp, TextBoxElement idCon, DateTimeElement date , bool isUpdate = false)
        {
            var result = true;

            code.Dispose();
            idIsp.Dispose();
            idCon.Dispose();
            date.Dispose();

            if (idIsp.IsNull)
            {
                idIsp.Error("Поле не заполнено");
                result = false;
            }

            if (idCon.IsNull)
            {
                idCon.Error("Поле не заполнено");
                result = false;
            }

            if (code.IsNull)
            {
                code.Error("Поле не заполнено");
                result = false;
            }

            if (!isUpdate && date.Value < DateTime.Today)
            {
                date.Error("Мы из будущего?");
                result = false;
            }

            if (HolidaysService.CheckDay(date.Value))
            {
                date.Error("В праздник никто работать не будет");
                result = false;
            }

            if(code.Value.Length < 13)
            {
                code.Error("13 символов и не на символ меньше");
                result = false;
            }
            else if (code.Value.Length > 13)
            {
                code.Error("13 символов и не на символ больше");
                result = false;
            }

            if (!code.IsNull && !CheckCode(code.Value))
            {
                code.Error("Код имеет не верный формат");
                result = false;
            }

            if(date.Value.DayOfWeek == DayOfWeek.Sunday || date.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                date.Error("В выходной никто работать не будет");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Проверяет код на правильность заполнения
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns>true - положительный ответ</returns>
        private static bool CheckCode(string code)
        {
            var c = code.ToArray();

            if (int.TryParse(c[1].ToString() , out int j1) || !int.TryParse(c[2].ToString(), out int j2))
            {
                return false;
            }

            for(int i = 0; i < c.Length; i++)
            {
                if (i == 1 || i == 2)
                    continue;

                if(!int.TryParse(c[i].ToString(), out int j))
                {
                    return false;
                }
            }

            return true;
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
               
                var entity = new TaskEntity(item.Code, isp, con, item.Date , item.Id);

                if(!context.Update(entity , entity.Id))
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

                var entity = new ArhivEntity(item.Code, isp, con, item.Date, item.DateClose, item.Otm , item.Id);

                if (!context.Update(entity , entity.Id))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Возращает задачу по Id
        /// </summary>
        /// <param name="code">Id задачи</param>
        /// <returns></returns>
        public static TaskEntity GetTaskById(int code , List<TaskEntity> list)
        {
            var item = list.FirstOrDefault(c => c.Id == code);

            return item;
        }

        /// <summary>
        /// Изменяет задачу, предварительно валидируя
        /// </summary>
        /// <param name="IdTask">Id текущей задачи</param>
        /// <param name="idIsp">Код исполнителя</param>
        /// <param name="idCon">Код котроллера</param>
        /// <param name="code">Новый код задачи</param>
        /// <param name="date">Дата срока</param>
        /// <returns>true - успешная операция</returns>
        public static bool UpdateTask(int IdTask, TextBoxElement idIsp, TextBoxElement idCon, TextBoxElement code, DateTimeElement date)
        {
            var result = Validation(code, idIsp, idCon, date , true);

            if (!result)
                return false;

            var entity = new TaskEntity(code.Value, Convert.ToInt32(idIsp.Value), Convert.ToInt32(idCon.Value), date.Value , IdTask);
            return context.Update(entity, IdTask);
        }

        /// <summary>
        /// Удаляет задачу из Task и добавляет в Arhiv
        /// </summary>
        /// <param name="IdTask">Id задания</param>
        /// <param name="Otm">Отметка задания</param>
        /// <param name="Date">Дата закрытия</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool Close(int IdTask , TextBoxElement Otm , DateTimeElement Date)
        {
            if (Otm.IsNull || !Otm.IsNumber)
            {
                Otm.Error("Что то не так");
                return false;
            }    

            if (Convert.ToInt32(Otm.Value) > 5 || Convert.ToInt32(Otm.Value) < 1)
            {
                Otm.Error("У нас не 12ти бальная система. От 1 до 5");
                return false;
            }

            var task = GetTaskById(IdTask , GetList());
           
            var arhiv = new ArhivEntity(task.Code, task.IdIsp, task.IdCon, task.Date, Date.Value, Convert.ToInt32(Otm.Value) , IdTask);

            var result = context.Add(arhiv);

            if (!result)
                return false;

            return context.Delete(task);

        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <param name="Id">Id задачи</param>
        /// <returns></returns>
        public static bool Delete(int Id)
        {
            var task = GetTaskById(Id, GetList());
            var result = context.Delete(task);
            return result;
        }

    }
}
