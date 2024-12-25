using System;
using System.Linq;
using UASKI.Core.Models;
using UASKI.Models.Elements;

namespace UASKI.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Валидация задачи
        /// </summary>
        /// <param name="idIsp">Элемент номер исполнителя</param>
        /// <param name="idCon">Элемент номер контролера</param>
        /// <param name="date">Элемент срок исполнения</param>
        /// <returns>true - успешное выполнение</returns>
        public static bool TaskValidation(TextBoxElement code, TextBoxElement idIsp, TextBoxElement idCon, DateTimeElement date, bool isUpdate = false)
        {
            var result = true;

            code.Dispose();
            idIsp.Dispose();
            idCon.Dispose();
            date.Dispose();

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

            if (!isUpdate && date.Value < DateTime.Today)
            {
                date.Error("Мы из будущего?");
                result = false;
            }

            var holidayList = HolidayModel.GetList();

            if (HolidayModel.CheckDay(date.Value, holidayList))
            {
                date.Error("В праздник никто работать не будет");
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

            if (!code.IsNull && !TaskModel.CheckCode(code.Value))
            {
                code.Error("Код имеет не верный формат");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Валидация задачи при закрытии
        /// </summary>
        /// <param name="Otm"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool CloseTaskValidation(TextBoxElement Otm, DateTimeElement Date)
        {
            if (Otm.IsNull || !Otm.IsNumber)
            {
                Otm.Error("Что то не так");
                return false;
            }

            if (Convert.ToInt32(Otm.Value) > 5 || Convert.ToInt32(Otm.Value) < 1)
            {
                Otm.Error("У нас не 12ти бальная система. От 1 до 5");
                return false;
            }

            return true;
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
        public static bool ArhivValidation(TextBoxElement code, TextBoxElement idIsp, TextBoxElement idCon, DateTimeElement date, DateTimeElement dateClose, TextBoxElement Otm)
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

            if (Otm.IsNull)
            {
                Otm.Error("Поле не заполнено");
                result = false;
            }

            if (!Otm.IsNumber || (Convert.ToInt32(Otm.Value) < 1 && Convert.ToInt32(Otm.Value) > 5))
            {
                Otm.Error("По 5ти бальной системе пожайлусто");
                result = false;
            }

            var holidayList = HolidayModel.GetList();

            if (HolidayModel.CheckDay(date.Value.Date, holidayList))
            {
                date.Error("В праздник никто работать не будет");
                result = false;
            }

            if (HolidayModel.CheckDay(dateClose.Value.Date, holidayList))
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

            if (!code.IsNull && !TaskModel.CheckCode(code.Value))
            {
                code.Error("Код имеет не верный формат");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Валидация празднечных дней
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>true - успешная операция</returns>
        public static bool HolidayValidation(MonthElement date)
        {
            var result = true;

            date.Dispose();

            var holyList = HolidayModel.GetList();

            foreach (var item in date.DateRange)
            {
                var holy = holyList.FirstOrDefault(c => c.Date.Date == item);

                if (holy != null)
                {
                    date.Error("Вы уже добавили эту дату");
                    result = false;
                    break;
                }
            }

            var taskList = TaskModel.GetList();

            foreach (var item in date.DateRange)
            {
                var task = taskList.FirstOrDefault(c => c.Date == item.Date);

                if (task != null)
                {
                    date.Error($"День является сроком задачи {task.Code}");
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Валидирует претензии и рецензии
        /// </summary>
        /// <param name="code"> Элемемент кода претензии</param>
        /// <param name="date">Элемемент даты</param>
        /// <param name="otm">Элемемент оценки</param>
        /// <returns></returns>
        public static bool PretValidation(TextBoxElement code, DateTimeElement date, TextBoxElement otm)
        {
            var result = true;

            code.Dispose();
            date.Dispose();
            otm.Dispose();

            if (code.IsNull)
            {
                code.Error("Поле не заполнено");
                result = false;
            }

            if (otm.IsNull)
            {
                otm.Error("Поле не заполнено");
                result = false;
            }

            if (!otm.IsNumber)
            {
                otm.Error("Тут должно быть число");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Валидация исполнителя
        /// </summary>
        /// <param name="firstName">Элемент фамилия сотрудника</param>
        /// <param name="name">Элемент имя сотрудника</param>
        /// <param name="lastName">Элемент отчество сотрудника</param>
        /// <param name="code">Элемент код сотрудника</param>
        /// <param name="podr">Элемент код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public static bool IspValidation(TextBoxElement firstName, TextBoxElement name, TextBoxElement lastName, TextBoxElement code, TextBoxElement podr , bool IsUpdate = false)
        {
            var result = true;

            firstName.Dispose();
            name.Dispose();
            lastName.Dispose();
            code.Dispose();
            podr.Dispose();

            if (firstName.IsNull)
            {
                firstName.Error("Поле не заполнено");
                result = false;
            }

            if (name.IsNull)
            {
                name.Error("Поле не заполнено");
                result = false;
            }

            if (lastName.IsNull)
            {
                lastName.Error("Поле не заполнено");
                result = false;
            }

            if (code.IsNull)
            {
                code.Error("Поле не заполнено");
                result = false;
            }

            if (podr.IsNull)
            {
                podr.Error("Поле не заполнено");
                result = false;
            }

            if (!code.IsNumber)
            {
                code.Error("Тут должно быть число");
                result = false;
            }

            if (!podr.IsNumber && !code.IsNumber)
            {
                podr.Error("И тут тоже");
                result = false;
            }
            else if (!podr.IsNumber)
            {
                podr.Error("Тут должно быть число");
                result = false;
            }

            if (result && !IsUpdate)
            {
                var list = IspModel.GetList();
                var isp = IspModel.GetByCode(Convert.ToInt32(code.Value));

                if (isp != null)
                {
                    code.Error("Код должен быть уникальным");
                    result = false;
                }

                isp = list.FirstOrDefault(c => c.CodePodr == Convert.ToInt32(podr.Value));

                if (isp != null)
                {
                    podr.Error("Код должен быть уникальным");
                    result = false;
                }

            }

            return result;
        }
    }
}
