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
            var context = new UAContext();
            return context.Holidays;
        }

        /// <summary>
        /// Возращает данные для заполнения в DataGridView
        /// </summary>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid()
        {
            var result = new List<DataGridRowModel>();

            var model = GetList();

            foreach (var item in model.OrderByDescending(c => c.Date))
            {
                var d = new DataGridRowModel(item.Date.ToString("dd.MM.yyyy"));
                result.Add(d);
            }

            return result;
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

            foreach (var item in date.DateRange)
            {
                var holy = context.Holidays.FirstOrDefault(c => c.Date.Date == item);

                if(holy != null)
                {
                    date.Error("Дата из диапазона уже существует");
                    result = false;
                    continue;
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
    }
}
                                                                                                    