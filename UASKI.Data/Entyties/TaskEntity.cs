using System;
using System.Linq;

namespace UASKI.Data.Entityes
{
    /// <summary>
    /// Класс сущности задачи
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Код задания
        /// </summary>
        public string Code { get; private set; }

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
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="idIsp">Идентификатор исполнителя</param>
        /// <param name="idCon">Идентификатор котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="isDouble">Двухстороняя задача</param>
        public TaskEntity(string code, int idIsp, int idCon, DateTime date, bool isDouble)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            IsDouble = isDouble;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="idIsp">Идентификатор исполнителя</param>
        /// <param name="idCon">Идентификатор котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="isDouble">Двухстороняя задача</param>
        public TaskEntity(string code, int idIsp, int idCon, DateTime date , int id, bool isDouble)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            Id = id;
            IsDouble = isDouble;
        }

    }
}
