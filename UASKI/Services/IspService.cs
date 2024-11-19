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
        public static List<IspEntity> GetList(bool isActive = true)
        {
            return context.Isps.Where(c => c.IsActive == isActive).ToList();
        }

        /// <summary>
        /// Преобразовывает список в модели для вывода в DataGridView
        /// </summary>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid(List<IspEntity> model)
        {
            var result = new List<DataGridRowModel>();

            foreach (var item in model.OrderBy(c => c.FirstName).ThenBy(c => c.Name).ThenBy(c => c.LastName))
            {
                var d = new DataGridRowModel(item.Code.ToString() , item.FirstName , item.Name , item.LastName , item.CodePodr.ToString());
                result.Add(d);
            }

            return result;
        }

        /// <summary>
        /// Возвращает объект исполнителя по фамилии
        /// </summary>
        /// <param name="FirstName"></param>
        /// <returns></returns>
        public static IspEntity GetByFirstName(string FirstName , List<IspEntity> list)
        {
            var result = list.FirstOrDefault(c => c.FirstName.ToLower().Equals(FirstName.ToLower()));
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
        /// Добавляет в базу исполнительно, предварительно валидируя
        /// </summary>
        /// <param name="FirstName">Фамилия сотрудника</param>
        /// <param name="Name">Имя сотрудника</param>
        /// <param name="LastName">Отчество сотрудника</param>
        /// <param name="Code">Код сотрудника</param>
        /// <param name="Podr">Код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public static bool Add(TextBoxElement FirstName , TextBoxElement Name , TextBoxElement LastName , TextBoxElement Code , TextBoxElement Podr)
        {
            var result = Validation(FirstName, Name, LastName, Code, Podr);

            if (!result)
                return false;

            var item = new IspEntity(Convert.ToInt32(Code.Value), FirstName.Value, Name.Value, LastName.Value, Convert.ToInt32(Podr.Value), true);
            var context = new UAContext();
            result = context.Add(item);

            return result;
        }

        /// <summary>
        /// Валидация исполнителя
        /// </summary>
        /// <param name="FirstName">Фамилия сотрудника</param>
        /// <param name="Name">Имя сотрудника</param>
        /// <param name="LastName">Отчество сотрудника</param>
        /// <param name="Code">Код сотрудника</param>
        /// <param name="Podr">Код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        private static bool Validation(TextBoxElement FirstName, TextBoxElement Name, TextBoxElement LastName, TextBoxElement Code, TextBoxElement Podr)
        {
            var result = true;

            FirstName.Dispose();
            Name.Dispose();
            LastName.Dispose();
            Code.Dispose();
            Podr.Dispose();

            if (FirstName.IsNull)
            {
                FirstName.Error("Поле не заполнено");
                result = false;
            }

            if (Name.IsNull)
            {
                Name.Error("Поле не заполнено");
                result = false;
            }

            if (LastName.IsNull)
            {
                LastName.Error("Поле не заполнено");
                result = false;
            }

            if (Code.IsNull)
            {
                Code.Error("Поле не заполнено");
                result = false;
            }

            if (Podr.IsNull)
            {
                Podr.Error("Поле не заполнено");
                result = false;
            }

            if (!Code.IsNumber)
            {
                Code.Error("Поле имеет не числовой тип");
                result = false;
            }

            if (!Podr.IsNumber)
            {
                Podr.Error("Поле имеет не числовой тип");
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
        /// <param name="code">Код сотрудника (старый)</param>
        /// <param name="FirstName">Фамилия сотрудника</param>
        /// <param name="Name">Имя сотрудника</param>
        /// <param name="LastName">Отчество сотрудника</param>
        /// <param name="Code">Код сотрудника (новый)</param>
        /// <param name="Podr">Код подразделения сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public static bool Update(int code , TextBoxElement FirstName, TextBoxElement Name, TextBoxElement LastName, TextBoxElement Code, TextBoxElement Podr)
        {
            var result = Validation(FirstName , Name , LastName , Code , Podr);

            if (!result)
                return result;

            var entity = GetByCode(code , GetList());
            var item = new IspEntity(Convert.ToInt32(Code.Value), FirstName.Value, Name.Value, LastName.Value, Convert.ToInt32(Podr.Value) , entity.IsActive);
            result = context.Update(item , code);

            if(!result)
                return false;

            if (code != Convert.ToInt32(Code.Value))
                result = TasksService.EditIsp(code, Convert.ToInt32(Code.Value));

            return result;
        }
    }
}
