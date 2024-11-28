using System;
using System.Diagnostics.Contracts;

namespace UASKI.Data.Entyties
{
    public class PretEntity
    {
        /// <summary>
        /// Код претензии/рецензии
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Код задачи
        /// </summary>
        public string CodeTask { get; set; }

        /// <summary>
        /// Дата выставления
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Otm { get; set; }

        /// <summary>
        /// Тип: 1 - претензия, 2 - рецензия
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код претензии/рецензии</param>
        /// <param name="codeTask">Код задачи</param>
        /// <param name="date">Дата создания</param>
        /// <param name="otm">Оценка</param>
        /// <param name="type">Тип: 1 - претензия, 2 - рецензия</param>
        public PretEntity(string code , string codeTask , DateTime date , int otm , int type)
        {
            Code = code;
            CodeTask = codeTask;
            Date = date;
            Otm = otm;
            Type = type;
        }
    }
}
