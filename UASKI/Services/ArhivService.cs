using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Entyties;
using UASKI.Data;
using UASKI.Models;
using System;
using System.Windows.Forms;
using UASKI.Data.Entityes;
using UASKI.Models.Elements;

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
        /// <param name="Id">Id аривной задачи</param>
        /// <returns></returns>
        public static ArhivEntity GetById(int Id , List<ArhivEntity> list)
        {
            var item = list.FirstOrDefault(c => c.Id == Id);

            return item;
        }

        /// <summary>
        /// Возращает задачу в активное состояние
        /// </summary>
        /// <param name="Id">Id закрытой задачи</param>
        /// <returns></returns>
        public static bool Open(int Id)
        {
            var arhiv = GetById(Id, GetList());
            var task = new TaskEntity(arhiv.Code, arhiv.IdIsp, arhiv.IdCon, arhiv.Date , Id);

            var result = context.Add(task);

            if (!result)
                return false;

            result = context.Delete(arhiv);
            return result;
        }

        /// <summary>
        /// Валидация Архива
        /// </summary>
        /// <param name="idIsp">Элемент номер исполнителя</param>
        /// <param name="idCon">Элемент номер контролера</param>
        /// <param name="date">Элемент срок исполнения</param>
        /// <param name="dateClose">Элемент дата закрытия</param>
        /// <param name="Otm">Элемент оценка</param>
        /// <returns>true - успешное выполнение</returns>
        private static bool Validation(TextBoxElement code, TextBoxElement idIsp, TextBoxElement idCon, DateTimeElement date, DateTimeElement dateClose , TextBoxElement Otm)
        {
            var result = true;

            code.Dispose();
            idIsp.Dispose();
            idCon.Dispose();
            date.Dispose();
            dateClose.Dispose();
            Otm.Dispose();

            if (idIsp.IsNull)
            {
                idIsp.Error("Поле не заполнено");
                result = false;
            }

            if (idCon.IsNull)
            {
                idCon.Error("Поле не заполнено");
                result = false;
            }

            if (code.IsNull)
            {
                code.Error("Поле не заполнено");
                result = false;
            }

            if(Otm.IsNull)
            {
                Otm.Error("Поле не заполнено");
                result = false;
            }

            if (!Otm.IsNumber || (Convert.ToInt32(Otm.Value) < 1 && Convert.ToInt32(Otm.Value) > 5))
            {
                Otm.Error("По 5ти бальной системе пожайлусто");
                result = false;
            }

            var holidayList = HolidaysService.GetList();

            if (HolidaysService.CheckDay(date.Value.Date , holidayList))
            {
                date.Error("В праздник никто работать не будет");
                result = false;
            }

            if (HolidaysService.CheckDay(dateClose.Value.Date , holidayList))
            {
                dateClose.Error("В праздник никто работать не будет");
                result = false;
            }

            if (code.Value.Length < 13)
            {
                code.Error("13 символов и не на символ меньше");
                result = false;
            }

            else if (code.Value.Length > 13)
            {
                code.Error("13 символов и не на символ больше");
                result = false;
            }

            if (!code.IsNull && !TasksService.CheckCode(code.Value))
            {
                code.Error("Код имеет не верный формат");
                result = false;
            }

            if (date.Value.DayOfWeek == DayOfWeek.Sunday || date.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                date.Error("В выходной никто работать не будет");
                result = false;
            }

            if (dateClose.Value.DayOfWeek == DayOfWeek.Sunday || dateClose.Value.DayOfWeek == DayOfWeek.Saturday)
            {
                dateClose.Error("В выходной никто работать не будет");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Обновление архива с предварительной валидацией
        /// </summary>
        /// <param name="idIsp">Элемент номер исполнителя</param>
        /// <param name="idCon">Элемент номер контролера</param>
        /// <param name="date">Элемент срок исполнения</param>
        /// <param name="dateClose">Элемент дата закрытия</param>
        /// <param name="Otm">Элемент оценка</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool Update(int id , TextBoxElement code, TextBoxElement idIsp, TextBoxElement idCon, DateTimeElement date, DateTimeElement dateClose, TextBoxElement Otm)
        {
            if(!Validation(code , idIsp , idCon , date , dateClose , Otm))
            {
                return false;
            }

            var arhiv = new ArhivEntity(code.Value, Convert.ToInt32(idIsp.Value), Convert.ToInt32(idCon.Value), date.Value, dateClose.Value, Convert.ToInt32(Otm.Value), id);
            return context.Update(arhiv , id);
        }

    }
}
