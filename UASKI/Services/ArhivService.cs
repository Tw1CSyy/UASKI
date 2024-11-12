using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Entyties;
using UASKI.Data.Context;
using UASKI.Models;
using System;

namespace UASKI.Services
{
    /// <summary>
    /// Сервис для работы с таблицей Arhiv
    /// </summary>
    public static class ArhivService
    {
        private static readonly UAContext context = new UAContext();

        /// <summary>
        /// Возращает список архива
        /// </summary>
        /// <returns></returns>
        public static List<ArhivEntity> GetList()
        {
            return context.Arhiv;
        }

        /// <summary>
        /// Возвращает список архива
        /// </summary>
        /// <param name="search">Код</param>
        /// <param name="isp">Котроллер или исполнитель</param>
        /// <param name="podr">Подразделение</param>
        /// <param name="isDate">используется ли дата</param>
        /// <param name="dateFrom">Дата закрытия от</param>
        /// <param name="dateTo">Дата закрытия до</param>
        /// <returns></returns>
        public static List<ArhivEntity> GetList(string search , string isp , string podr , bool isDate , DateTime dateFrom , DateTime dateTo)
        {
            var list = GetList();

            if (isDate)
                list = list.Where(c => c.DateClose.Date >= dateFrom.Date && c.DateClose.Date <= dateTo.Date).ToList();

            if (!string.IsNullOrEmpty(search))
                list = list.Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(isp) && int.TryParse(isp, out int i))
                list = list.Where(c => c.IdCon == Convert.ToInt32(isp) || c.IdIsp == Convert.ToInt32(isp)).ToList();

            if(!string.IsNullOrEmpty(podr) && int.TryParse(podr , out int j))
            {
                var users = IspService.GetList().Where(c => c.CodePodr == Convert.ToInt32(podr)).ToList();
                var list2 = new List<ArhivEntity>();

                foreach (var user in users)
                {
                    var items = list.Where(c => c.IdCon == user.Code || c.IdIsp == user.Code).ToList();

                    if (items.Any())
                        list2.AddRange(items);
                }

                list = list2;
            }

            return list;
        }

        /// <summary>
        /// Формирует модель для вывода в DataGridView
        /// </summary>
        /// <param name="list">Список объектов</param>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid(List<ArhivEntity> list)
        {
            var model = new List<DataGridRowModel>();

            foreach (var item in list.OrderByDescending(c => c.DateClose))
            {
                var isp = IspService.GetByCode(item.IdIsp);
                var con = IspService.GetByCode(item.IdCon);

                var st = new DataGridRowModel(item.Code,
                    $"{isp.FirstName} {isp.Name.ToUpper()[0]}. {isp.LastName.ToUpper()[0]}.",
                    $"{con.FirstName} {con.Name.ToUpper()[0]}. {con.LastName.ToUpper()[0]}.",
                    item.Date.ToString("dd.MM.yyyy"), item.DateClose.ToString("dd.MM.yyyy"),
                    item.Otm.ToString());

                model.Add(st);
            }

            return model;
        }

        /// <summary>
        /// Возращает архивную задачу по коду
        /// </summary>
        /// <param name="code">Код аривной задачи</param>
        /// <returns></returns>
        public static ArhivEntity GetByCode(string code)
        {
            var list = GetList();
            var item = list.FirstOrDefault(c => c.Code.Equals(code));

            return item;
        }

    }
}
