using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data;
using UASKI.Data.Entityes;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class TaskModel
    {
        private static UAContext context = DataConnection.Context;

        /// <summary>
        /// Код задания
        /// </summary>
        public string Code { get;  private set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int IdIsp { get; private set; }

        /// <summary>
        /// Идентификатор контролера
        /// </summary>
        public int IdCon { get; private set; }

        /// <summary>
        /// Дата срока
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary> 
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Двухстороняя задача
        /// </summary>
        public bool IsDouble { get; private set; }

        /// <summary>
        /// Создает экземпляр задачи
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <param name="idIsp">Код исполнителя</param>
        /// <param name="idCon">Код котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="isDouble">Двухстороняя задача</param>
        /// <param name="id">Id</param>
        public TaskModel(string code, int idIsp, int idCon, DateTime date, bool isDouble , int id = 0)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date.Date;
            IsDouble = isDouble;
            Id = id;
        }

        /// <summary>
        /// Создает экземпляр задачи
        /// </summary>
        /// <param name="entity">Объект TaskEntity</param>
        internal TaskModel(TaskEntity entity)
        {
            Code = entity.Code;
            IdIsp = entity.IdIsp;
            IdCon = entity.IdCon;
            Date = entity.Date.Date;
            Id = entity.Id;
            IsDouble = entity.IsDouble;
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект TaskEntity</returns>
        private TaskEntity Get()
        {
            return new TaskEntity(Code, IdIsp, IdCon, Date.Date, Id , IsDouble);
        }

        /// <summary>
        /// Добавляет новую задачу
        /// </summary>
        /// <returns>true - успешное выполнение</returns>
        public bool Add()
        {
            var entity = Get();
            return context.Add(entity);
        }

        /// <summary>
        /// Изменяет задачу
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public bool Update(int id)
        {
            var entity = Get();
            return context.Update(entity, id);
        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var entity = context.Tasks.FirstOrDefault(c => c.Id == Id);
            var result = context.Delete(entity);
            return result;
        }

        /// <summary>
        /// Удаляет задачу из Task и добавляет в Arhiv
        /// </summary>
        /// <returns>Положительный или отрицательный результат</returns>
        public bool Close(int otm, DateTime dateClose)
        {
            var arhiv = new ArhivModel(this, dateClose.Date, otm);
            var result = context.Add(arhiv.Get());

            if (!result)
                return false;

            var newId = ArhivModel.GetList().OrderByDescending(c => c.Id).First().Id;
            var prets = PretModel.GetList().Where(c => c.IdTask == Id);
           
            result = context.Delete(Get());

            if (!result)
                return false;

            foreach (var pret in prets)
            {
                var newPret = new PretModel(pret.Code, newId, pret.Date, pret.Otm, pret.Type);
                result = newPret.Update(pret.Id);

                if (!result)
                    return false;
            }

            return result;
        }

        /// <summary>
        /// Меняет код исполнителя или котроллера в задаче
        /// </summary>
        /// <param name="oldCode">Старый код</param>
        /// <param name="newCode">Новый код</param>
        /// <returns>true - Успешная операция</returns>
        public bool EditCodeIsp(int oldCode, int newCode)
        {
            if(IdIsp == oldCode)
            {
                IdIsp = newCode;
            }

            if(IdCon == oldCode)
            {
                IdCon = newCode;
            }

            return context.Update(Get(), Id);
        }

        /// <summary>
        /// Высчитывает дни опаздания на месяц по задаче, учитывая выходные дни
        /// </summary>
        /// <param name="list">Список дней из базы</param>
        /// <returns></returns>
        public int GetDaysOpz(List<HolidayModel> list)
        {
            int result = 0;

            for (DateTime i = Date; i < DateTime.Today;)
            {
                if (list.FirstOrDefault(c => c.Date == i) == null)
                    result++;

                i = i.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// Высчитывает дни опаздания на месяц по задаче, учитывая выходные дни
        /// </summary>
        /// <param name="list">Список дней из базы</param>
        /// <param name="dateFrom">Дата с которой считаем</param>
        /// <param name="dateTo">Дата до которой считаем</param>
        /// <returns></returns>
        public int GetDaysOpz(List<HolidayModel> list, DateTime dateFrom, DateTime dateTo)
        {
            int result = 0;

            if(dateFrom == dateTo)
                return result;

            for (DateTime i = Date; i <= dateTo;)
            {
                if (i < dateFrom)
                {
                    i = i.AddDays(1);
                    continue;
                }

                if (list.FirstOrDefault(c => c.Date == i) == null)
                    result++;

                i = i.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// Возвращает исполнителя
        /// </summary>
        /// <param name="list">Список исполнителей/котроллеров</param>
        /// <returns></returns>
        public IspModel GetIsp(List<IspModel> list)
        {
            var item = list.FirstOrDefault(c => c.Code == IdIsp);

            if (item != null)
                return item;
            else
                return new IspModel(0, "0", "0", "0", 0);
        }

        /// <summary>
        /// Возвращает исполнителя
        /// </summary>
        /// <param name="list">Список исполнителей/котроллеров</param>
        /// <returns></returns>
        public IspModel GetCon(List<IspModel> list)
        {
            var item = list.FirstOrDefault(c => c.Code == IdCon);

            if (item != null)
                return item;
            else
                return new IspModel(0, "0", "0", "0", 0);
        }

        /// <summary>
        /// Возвращает строку кода задания с обозначением
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            if (IsDouble)
                return "(Д) " + Code; 
            else
                return Code;
        }
        /// <summary>
        /// Возращает задачу по Id
        /// </summary>
        /// <param name="code">Id задачи</param>
        /// <returns></returns>
        public static TaskModel GetById(int code)
        {
            return GetList().FirstOrDefault(c => c.Id == code);
        }

        /// <summary>
        /// Возращает список задач
        /// </summary>
        public static List<TaskModel> GetList()
        {
            return context.Tasks.Select(c => new TaskModel(c)).ToList();
        }

        /// <summary>
        /// Проверяет код на правильность заполнения
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns>true - положительный ответ</returns>
        public static bool CheckCode(string code)
        {
            var c = code.ToArray();

            if (int.TryParse(c[1].ToString(), out int j1) || int.TryParse(c[2].ToString(), out int j2))
            {
                return false;
            }

            for (int i = 0; i < c.Length; i++)
            {
                if (i == 1 || i == 2)
                    continue;

                if (!int.TryParse(c[i].ToString(), out int j))
                {
                    return false;
                }
            }

            return true;
        }


    }
}
