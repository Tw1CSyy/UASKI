using System;

namespace UASKI.Data.Entyties
{
    /// <summary>
    /// Класс сущности архивной задачи
    /// </summary>
    public class ArhivEntity
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
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="isDouble">Двухстороняя задача</param>
        public ArhivEntity(string code, int idIsp, int idCon, DateTime date, DateTime dateClose, int otm, int id, bool isDouble)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            DateClose = dateClose;
            Otm = otm;
            Id = id;
            IsDouble = isDouble;
        }

    }
}
