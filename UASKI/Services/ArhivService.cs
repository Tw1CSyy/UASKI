using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Entyties;
using UASKI.Data.Context;
using UASKI.Models;
using System;
using System.Windows.Forms;
using UASKI.Data.Entityes;

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
        /// <param name="isDate">Используется ли дата</param>
        /// <param name="dateFrom">Дата закрытия от</param>
        /// <param name="dateTo">Дата закрытия до</param>
        /// <returns></returns>
        public static List<ArhivEntity> GetList(string search , string isp , bool isDate , DateTime dateFrom , DateTime dateTo)
        {
            var list = GetList();

            if (isDate)
                list = list.Where(c => c.DateClose.Date >= dateFrom.Date && c.DateClose.Date <= dateTo.Date).ToList();

            if (!string.IsNullOrEmpty(search))
                list = list.Where(c => c.Code.ToLower().Contains(search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(isp) && int.TryParse(isp, out int i))
                list = list.Where(c => c.IdCon == Convert.ToInt32(isp) || c.IdIsp == Convert.ToInt32(isp)).ToList();

            return list;
        }

        /// <summary>
        /// Возращает архивную задачу по коду
        /// </summary>
        /// <param name="code">Код аривной задачи</param>
        /// <returns></returns>
        public static ArhivEntity GetByCode(string code , List<ArhivEntity> list)
        {
            var item = list.FirstOrDefault(c => c.Code.Equals(code));

            return item;
        }

        /// <summary>
        /// Возращает задачу в активное состояние
        /// </summary>
        /// <param name="code">Код закрытой задачи</param>
        /// <returns></returns>
        public static bool Open(string code)
        {
            var arhiv = GetByCode(code, GetList());
            var task = new TaskEntity(arhiv.Code, arhiv.IdIsp, arhiv.IdCon, arhiv.Date);

            var result = context.Add(task);

            if (!result)
                return false;

            result = context.Delete(arhiv);
            return result;
        }

    }
}
