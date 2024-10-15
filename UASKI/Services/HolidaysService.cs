using System;
using System.Collections.Generic;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Models;

namespace UASKI.Services
{
    public static class HolidaysService
    {
        private static UAContext context = new UAContext();


        /// <summary>
        /// Возращает список празднечных дней
        /// </summary>
        /// <returns></returns>
        private static List<HolidayEntity> GetList()
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

            foreach (var item in model)
            {
                var d = new DataGridRowModel(item.Date.ToString("dd.MM.yyyy"));
                result.Add(d);
            }

            return result;
        }

        /// <summary>
        /// Доавбляет празднечный день
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>true - успешная операция</returns>
        public static bool Add(DateTime date)
        {
            var dat = new HolidayEntity(date);
            return context.Add(dat);
        }

        /// <summary>
        /// Добавляет праздечные дни
        /// </summary>
        /// <param name="dateFrom">Дата начала</param>
        /// <param name="dateTo">Дата окончания</param>
        /// <returns>true - успешная операция</returns>
        public static bool Add(DateTime dateFrom , DateTime dateTo)
        {
            for (DateTime date = dateFrom; date.Date < dateTo.Date; date = date.AddDays(1))
            {
                if(!Add(date))
                {
                    return false;
                };
            }

            return true;
        }
    }
}
