using System;
using System.Collections.Generic;
using System.Linq;

using UASKI.Core.SystemModels;
using UASKI.Data;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель архивной задачи
    /// </summary>
    public class ArhivModel
    {
        private readonly static UAContext context = new UAContext();

        /// <summary>
        /// Код задания
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int IdIsp { get; private set; }

        /// <summary>
        /// Идентификатор контроллера
        /// </summary>
        public int IdCon { get; private set; }

        /// <summary>
        /// Дата срока
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime DateClose { get; private set; }

        /// <summary>
        /// Оценка задания
        /// </summary>
        public int Otm { get; private set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="task">Модель задачи</param>
        /// <param name="dateClose">Дата закрытия</param>
        /// <param name="otm">Оценка</param>
        public ArhivModel(TaskModel task , DateTime dateClose , int otm)
        {
            Code = task.Code;
            IdIsp = task.IdIsp;
            IdCon = task.IdCon;
            Date = task.Date;
            DateClose = dateClose;
            Otm = otm;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <param name="idIsp">Код исполнителя</param>
        /// <param name="idCon">Код котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="dateClose">Дата закрытия</param>
        /// <param name="otm">Оценка</param>
        public ArhivModel(string code , int idIsp , int idCon , DateTime date , DateTime dateClose , int otm , int id)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            DateClose = dateClose;
            Otm = otm;
            Id = id;
        }

        /// <summary>
        /// Создает объект класса
        /// <param name="a">Сущность архивной задачи</param>
        /// </summary>
        internal ArhivModel(ArhivEntity a)
        {
            Code = a.Code;
            IdIsp = a.IdIsp;
            IdCon = a.IdCon;
            Date = a.Date;
            DateClose = a.DateClose;
            Otm = a.Otm;
            Id = a.Id;
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект ArhivEntity</returns>
        public ArhivEntity Get()
        {
            return new ArhivEntity(Code, IdIsp, IdCon, Date, DateClose, Otm, Id);
        }

        /// <summary>
        /// Возращает задачу в активное состояние
        /// </summary>
        /// <returns>true - Успешное выполнение</returns>
        public bool Open()
        {
            var task = new TaskEntity(Code, IdIsp, IdCon, Date, Id);

            var result = context.Add(task);

            if (!result)
                return false;

            var entity = Get();
            result = context.Delete(entity);

            if (!result)
                return false;

            var prets = PretModel.GetList().Where(c => c.IdTask == entity.Id);
            var newId = TaskModel.GetList().OrderByDescending(c => c.Id).First().Id;

            foreach (var pret in prets)
            {
                var newPret = new PretModel(pret.Code, newId, pret.Date, pret.Otm, pret.Type);
                result =  newPret.Update(pret.Id);

                if (!result)
                    return false;
            }

            return result;
        }

        /// <summary>
        /// Возвращает разницу в днях учитывая празднечные дни
        /// </summary>
        /// <returns></returns>
        public int GetDaysOpz(List<HolidayModel> list)
        {
            int result = 0;

            for (DateTime i = Date; i < DateClose;)
            {
                if (list.FirstOrDefault(c => c.Date == i) == null)
                    result++;

                i = i.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// Обновление архива с предварительной валидацией
        /// </summary>
        /// <returns>true - успешное выполнение</returns>
        public bool Update()
        {
            var arhiv = Get();
            return context.Update(arhiv, Id);
        }

        /// <summary>
        /// Меняет код исполнителя или котроллера в задаче
        /// </summary>
        /// <param name="oldCode">Старый код</param>
        /// <param name="newCode">Новый код</param>
        /// <returns>true - Успешная операция</returns>
        public bool EditCodeIsp(int oldCode, int newCode)
        {
            if (IdIsp == oldCode)
            {
                IdIsp = newCode;
            }

            if (IdCon == oldCode)
            {
                IdCon = newCode;
            }

            return context.Update(Get(), Id);
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
                return new IspModel(0 , "0", "0", "0", 0);
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
        /// Возращает список архива
        /// </summary> 
        /// <returns>Список архивных задач</returns>
        public static List<ArhivModel> GetList()
        {
            return context.Arhiv.Select(c => new ArhivModel(c)).ToList();
        }

        /// <summary>
        /// Возращает архивную задачу по коду
        /// </summary>
        /// <param name="Id">Id аривной задачи</param>
        /// <returns>Объект модели архивной задачи</returns>
        public static ArhivModel GetById(int Id)
        {
            return GetList().FirstOrDefault(c => c.Id == Id);
        }

    }
}
