using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data;
using UASKI.Data.Entyties;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель претензии или рецензии
    /// </summary>
    public class PretModel
    {
        private readonly static UAContext context = new UAContext();

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
        /// <param name="code">Код претензии/рецензии</param>
        /// <param name="idTask">Идентификатор задачи</param>
        /// <param name="date">Дата претензии/рецензии</param>
        /// <param name="otm">Оценка претензии/рецензии</param>
        /// <param name="type">Тип  1 - претензия  2 - рецензия</param>
        public PretModel(string code, int idTask, DateTime date, int otm, int type)
        {
            Code = code;
            IdTask = idTask;
            Date = date;
            Otm = otm;
            Type = type;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="entity">Сущность претензии/рецензии</param>
        public PretModel(PretEntity entity)
        {
            Id = entity.Id;
            Code = entity.Code;
            IdTask = entity.IdTask;
            Date = entity.Date;
            Otm = entity.Otm;
            Type = entity.Type;
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект PretEntity</returns>
        public PretEntity Get()
        {
            return new PretEntity(Id, Code, IdTask, Date, Otm, Type);
        }

        /// <summary>
        /// Добавляет новую претензию или рецензию, предаварительно валидируя
        /// </summary>
        /// <returns>true - успешное выполнение</returns>
        public bool Add()
        {
            var entity = Get();
            return context.Add(entity);
        }

        /// <summary>
        /// Возвращает список из таблицы Pret
        /// </summary>
        /// <returns></returns>
        public static List<PretModel> GetList()
        {
            return context.Prets.Select(c => new PretModel(c)).ToList();
        }

        /// <summary>
        /// Возвращает pret по коду
        /// </summary>
        /// <param name="Id">Id</param>
        public static PretModel GetById(int Id)
        {
            return GetList().FirstOrDefault(c => c.Id == Id);
        }

        /// <summary>
        /// Обновляет претензию/рецензию
        /// </summary>
        /// <returns></returns>
        public bool Update(int id)
        {
            var entity = Get();
            return context.Update(entity, id);
        }

        /// <summary>
        /// Удаляет претензию/рецензию
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            return context.Delete(Get());
        }

    }
}
