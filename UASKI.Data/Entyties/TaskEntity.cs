using System;

namespace UASKI.Data.Entityes
{
    /// <summary>
    /// Модель задач
    /// </summary>
    public class TaskEntity
    {
        private static readonly DateTime DateDefult = new DateTime(2000, 01, 01);
        private static readonly int OtmDefult = 0;
        private static readonly string NumDefult = " ";


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
        /// Закрыто
        /// </summary>
        public bool IsClose { get; private set; }

        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime DateClose { get; private set; }

        /// <summary>
        /// Оценка задания
        /// </summary>
        public int Otm { get; private set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Num { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="idIsp">Идентификатор исполнителя</param>
        /// <param name="idCon">Идентификатор котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="isClose">Закрыто</param>
        /// <param name="dateClose">Дата закрытия</param>
        /// <param name="otm">Оценка</param>
        /// <param name="num">Номер при закрытии</param>
        public TaskEntity(string code , int idIsp , int idCon , DateTime date , bool isClose , DateTime dateClose , int otm , string num)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            IsClose = isClose;
            DateClose = dateClose;
            Otm = otm;
            Num = num;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код</param>
        /// <param name="idIsp">Идентификатор исполнителя</param>
        /// <param name="idCon">Идентификатор котроллера</param>
        /// <param name="date">Дата срока</param>
        /// <param name="isClose">Закрыто</param>
        public TaskEntity(string code, int idIsp, int idCon, DateTime date, bool isClose)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
            IsClose = isClose;

            DateClose = DateDefult;
            Otm = OtmDefult;
            Num = NumDefult;
        }
    }
}
