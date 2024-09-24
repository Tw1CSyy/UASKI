using System;

namespace UASKI.Data.Entityes
{
    /// <summary>
    /// Модель задач
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Код задания
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int IdIsp { get; set; }

        /// <summary>
        /// Идентификатор контроллера
        /// </summary>
        public int IdCon { get; set; }

        /// <summary>
        /// Дата срока
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Закрыто
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime? DateClose { get; set; }

        /// <summary>
        /// Оценка задания
        /// </summary>
        public int? Otm { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Num { get; set; }
    }
}
