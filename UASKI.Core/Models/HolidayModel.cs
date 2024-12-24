using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data;
using UASKI.Data.Entityes;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель для празднечного дня
    /// </summary>
    public class HolidayModel
    {
        private readonly static UAContext context = new UAContext();

        /// <summary>
        /// Идентификатор праздника
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Дата праздника
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="entity">Сущность дня</param>
        private HolidayModel(HolidayEntity entity)
        {
            Id = entity.Id;
            Date = entity.Date;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="date">Дата</param>
        public HolidayModel(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект HolidayEntity</returns>
        public HolidayEntity Get()
        {
            return new HolidayEntity(Id, Date);
        }

        /// <summary>
        /// Добавляет празднечный день
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            return context.Add(Get());
        }

        /// <summary>
        /// Удаляет празднечные дни
        /// </summary>
        /// <returns>true - Успешная операция</returns>
        public bool Delete()
        {
            var entity = context.Holidays.FirstOrDefault(c => c.Id == Id);

            return context.Delete(entity);
        }

        /// <summary>
        /// Возращает список празднечных дней
        /// </summary>
        /// <returns></returns>
        public static List<HolidayModel> GetList()
        {
            return context.Holidays.Select(c => new HolidayModel(c)).OrderByDescending(c => c.Date).ToList();
        }

        /// <summary>
        /// Проверяет дату на праздничный день
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <param name="list">Список празднечных дней</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool CheckDay(DateTime date, List<HolidayModel> list)
        {
            var day = list.FirstOrDefault(c => c.Date.Date == date.Date);
            return day != null;
        }
    }
}
