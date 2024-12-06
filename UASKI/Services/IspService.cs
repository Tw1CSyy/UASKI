using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Models;
using UASKI.Models.Elements;

namespace UASKI.Services
{
    /// <summary>
    /// Сервис для работы с таблицей Isp
    /// </summary>
    public static class IspService
    {
        private static readonly UAContext context = new UAContext();

        /// <summary>
        /// Возращает список пользователей
        /// </summary>
        /// <returns></returns>
        public static List<IspEntity> GetList(bool isActive)
        {
            return GetList().Where(c => c.IsActive == isActive).ToList();
        }

        /// <summary>
        /// Возращает список пользователей
        /// </summary>
        /// <returns></returns>
        public static List<IspEntity> GetList()
        {
            return context.Isps.OrderBy(c => c.FirstName).ThenBy(c => c.Name).ThenBy(c => c.LastName).ToList();
        }

        /// <summary>
        /// Возвращает объект исполнителя по фамилии
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static IspEntity GetByFirstName(string firstName , List<IspEntity> list)
        {
            var result = list.FirstOrDefault(c => c.FirstName.ToLower().Equals(firstName.ToLower()));
            return result;
        }

        /// <summary>
        /// Возвращает объект исполнителя по коду
        /// </summary>
        /// <param name="code">Код</param>
        /// <returns>Объект класса</returns>
        public static IspEntity GetByCode(int code , List<IspEntity> list)
        {
            return list.FirstOrDefault(c => c.Code == code);
        }

        /// <summary>
        /// Добавляет в базу исполнителя, предварительно валидируя
        /// </summary>
        /// <param name="firstName">Элемент фамилия сотрудника</param>
        /// <param name="name">Элемент имя сотрудника</param>
        /// <param name="lastName">Элемент отчество сотрудника</param>
        /// <param name="code">Элемент код сотрудника</param>
        /// <param name="podr">Элемент код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public static bool Add(TextBoxElement firstName , TextBoxElement name , TextBoxElement lastName , TextBoxElement code , TextBoxElement podr)
        {
            var result = Validation(firstName, name, lastName, code, podr);

            if (!result)
                return false;

            var item = new IspEntity(Convert.ToInt32(code.Value), firstName.Value, name.Value, lastName.Value, Convert.ToInt32(podr.Value), true);
            var context = new UAContext();
            result = context.Add(item);

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
        private static bool Validation(TextBoxElement firstName, TextBoxElement name, TextBoxElement lastName, TextBoxElement code, TextBoxElement podr)
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
            else if(!podr.IsNumber)
            {
                podr.Error("Тут должно быть число");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Дизактивирует исполнителя
        /// </summary>
        /// <param name="code">Код исполнителя</param>
        /// <returns>true - успешная операция</returns>
        public static bool Disactive(int code)
        {
            var isp = GetByCode(code , GetList());
            var entiry = new IspEntity(isp.Code, isp.FirstName, isp.Name, isp.LastName, isp.CodePodr, false);
            return context.Update(entiry , code);
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="codeIsp">Код сотрудника (старый)</param>
        /// <param name="firstName">Фамилия сотрудника</param>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="lastName">Отчество сотрудника</param>
        /// <param name="code">Код сотрудника (новый)</param>
        /// <param name="podr">Код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public static bool Update(int codeIsp , TextBoxElement firstName, TextBoxElement name, TextBoxElement lastName, TextBoxElement code, TextBoxElement podr)
        {
            var result = Validation(firstName , name , lastName , code , podr);

            if (!result)
                return result;

            var entity = GetByCode(codeIsp , GetList());
            var item = new IspEntity(Convert.ToInt32(code.Value), firstName.Value, name.Value, lastName.Value, Convert.ToInt32(podr.Value) , entity.IsActive);
            result = context.Update(item , codeIsp);

            if(!result)
                return false;

            if (codeIsp != Convert.ToInt32(code.Value))
                result = TasksService.EditIsp(codeIsp, Convert.ToInt32(code.Value));

            return result;
        }

        /// <summary>
        /// Возвращает строку кода и инициалов исполнителя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetIniz(IspEntity entity , bool code = true)
        {
           if(code)
                return $"{entity.FirstName} {entity.Name.ToUpper()[0]}. {entity.LastName.ToUpper()[0]}. {entity.CodePodr}";
           else
                return $"{entity.FirstName} {entity.Name.ToUpper()[0]}. {entity.LastName.ToUpper()[0]}.";
        }
    }
}
