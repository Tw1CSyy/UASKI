using System;

namespace UASKI.Data.Entyties
{
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
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="idIsp">Идентификатор исполнителя</param>
        /// <param name="idCon">Идентификатор котроллера</param>
        /// <param name="date">Дата срока</param>
        public ArhivEntity(string code, int idIsp, int idCon, DateTime date , DateTime dateClose , int otm)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            DateClose = dateClose;
            Otm = otm;
        }
    }
}
