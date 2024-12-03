using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Models;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    /// <summary>
    /// Сервис для работы с таблицей Holidays
    /// </summary>
    public static class HolidaysService
    {
        private static readonly UAContext context = new UAContext();


        /// <summary>
        /// Возращает список празднечных дней
        /// </summary>
        /// <returns></returns>
        public static List<HolidayEntity> GetList()
        {
            return context.Holidays.OrderByDescending(c => c.Date).ToList();
        }

        /// <summary>
        /// Добавляет празднечный день
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>true - успешная операция</returns>
        public static bool Add(MonthElement date)
        {
            var result = true;

            date.Dispose();

            if(date.Date < DateTime.Today.Date)
            {
                date.Error("Дата не может быть раньше текущей даты");
                result = false;
            }

            var holyList = GetList();

            foreach (var item in date.DateRange)
            {
                var holy = holyList.FirstOrDefault(c => c.Date.Date == item);

                if(holy != null)
                {
                    date.Error("Дата из диапазона уже существует");
                    result = false;
                    break;
                }
            }

            var taskList = TasksService.GetList();

            foreach (var item in date.DateRange)
            {
                var task = taskList.FirstOrDefault(c => c.Date == item.Date);

                if(task != null)
                {
                    date.Error($"День является сроком задачи {task.Code}");
                    result = false;
                    break;
                }
            }

            if (result)
            {
                foreach (var item in date.DateRange)
                {
                    var holy = new HolidayEntity(item);
                    result = context.Add(holy);
                }

                return result;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверяет дату на праздничный день
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool CheckDay(DateTime date)
        {
            var day = GetList().FirstOrDefault(c => c.Date.Date == date.Date);
            return day != null;
        }

        /// <summary>
        /// Удаляет празднечные дни
        /// </summary>
        /// <param name="list">Список id дней</param>
        /// <returns>true - Успешная операция</returns>
        public static bool Delete(List<int> list , List<HolidayEntity> listEn)
        {
            var result = true;

            foreach (var item in list)
            {
                var holy = listEn.FirstOrDefault(c => c.Id == item);
                result = context.Delete(holy);

                if (!result)
                    break;
            }

            return result;
        }
    }
}
                                                                                                    