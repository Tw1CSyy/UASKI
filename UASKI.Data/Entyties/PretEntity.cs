using System;
using System.Diagnostics.Contracts;

namespace UASKI.Data.Entyties
{
    /// <summary>
    /// Класс сущности претензий/рецензий
    /// </summary>
    public class PretEntity
    {
        /// <summary>
        /// Идентификатор претензии/рецензии
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Код претензии/рецензии
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public int IdTask { get; private set; }

        /// <summary>
        /// Дата выставления претензии/рецензии
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Оценка претензии/рецензии
        /// </summary>
        public int Otm { get; private set; }

        /// <summary>
        /// Тип  1 - претензия  2 - рецензия
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="id">Идентиификатор претензии/рецензии</param>
        /// <param name="code">Код претензии/рецензии</param>
        /// <param name="idTask">Идентификатор задачи</param>
        /// <param name="date">Дата претензии/рецензии</param>
        /// <param name="otm">Оценка претензии/рецензии</param>
        /// <param name="type">Тип  1 - претензия  2 - рецензия</param>
        public PretEntity(int id, string code, int idTask, DateTime date, int otm, int type)
        {
            Id = id;
            Code = code;
            IdTask = idTask;
            Date = date;
            Otm = otm;
            Type = type;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код претензии/рецензии</param>
        /// <param name="idTask">Идентификатор задачи</param>
        /// <param name="date">Дата претензии/рецензии</param>
        /// <param name="otm">Оценка претензии/рецензии</param>
        /// <param name="type">Тип  1 - претензия  2 - рецензия</param>
        public PretEntity(string code, int idTask, DateTime date, int otm, int type)
        {
            Code = code;
            IdTask = idTask;
            Date = date;
            Otm = otm;
            Type = type;
        }
    }
}
