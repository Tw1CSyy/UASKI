using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Исполнитель
        /// </summary>
        public IspModel Isp { get; private set; }

        /// <summary>
        /// Контроллер
        /// </summary>
        public IspModel Con { get; private set; }

        /// <summary>
        /// Дней опазданий с учетом выходных и праздников
        /// </summary>
        public int DaysOpz { get; private set; }

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
        internal ArhivModel(ArhivEntity a, IspModel isp, IspModel con, int dayOpz)
        {
            Code = a.Code;
            IdIsp = a.IdIsp;
            IdCon = a.IdCon;
            Date = a.Date;
            DateClose = a.DateClose;
            Otm = a.Otm;
            Id = a.Id;

            Isp = isp;
            Con = con;
            DaysOpz = dayOpz;
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

            result = context.Delete(Get());
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
        /// Возращает список архива
        /// </summary> 
        /// <returns>Список архивных задач</returns>
        public static List<ArhivModel> GetList()
        {
            var ispList = context.Isps;
            var taskList = context.Arhiv;
            var holidayList = context.Holidays;
            var result = new List<ArhivModel>();

            foreach (var task in taskList)
            {
                var isp = ispList.FirstOrDefault(c => c.Code == task.IdIsp);
                var con = ispList.FirstOrDefault(c => c.Code == task.IdCon);
                var days = 0;

                for (DateTime date = task.Date; date < task.DateClose;)
                {
                    if (holidayList.FirstOrDefault(c => c.Date == date) != null)
                    {
                        date = date.AddDays(1);
                        continue;
                    }

                    days++;
                    date = date.AddDays(1);
                }

                var item = new ArhivModel(task, new IspModel(isp), new IspModel(con), days);
                result.Add(item);
            }

            return result;
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
