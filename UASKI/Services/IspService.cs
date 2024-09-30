using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
using UASKI.Helpers;
using UASKI.Models;

namespace UASKI.Services
{
    public static class IspService
    {
        /// <summary>
        /// Возвращает список пользователей
        /// </summary>
        public static List<IspEntity> GetList()
        {
            var context = new UAContext();
            return context.Isps;
        }

        /// <summary>
        /// Преобразовывает список в модели для вывода в DataGridView
        /// </summary>
        /// <returns></returns>
        public static List<DataGridRowModel> GetListByDataGrid()
        {
            var model = GetList();

            var result = new List<DataGridRowModel>();

            foreach (var item in model)
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
        public static IspEntity GetByFirstName(string FirstName)
        {
            var list = GetList();
            var result = list.FirstOrDefault(c => c.FirstName.ToLower().Equals(FirstName.ToLower()));
            return result;
        }

        /// <summary>
        /// Проверяет поля ввода для добавления
        /// </summary>
        /// <param name="FirstName">ТекстБох Фамилия</param>
        /// <param name="Name">ТекстБох Имя</param>
        /// <param name="LastName">ТекстБох Отчество</param>
        /// <param name="Code">ТекстБох кода сторудника</param>
        /// <param name="Podr">ТекстБох кода подразделения</param>
        /// <returns>Положительный или отрицательный результат</returns>
        public static bool CheckAdd(TextBox FirstName , TextBox Name , TextBox LastName , TextBox Code , TextBox Podr)
        {
            if(string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(LastName.Name) || string.IsNullOrEmpty(Code.Text) || string.IsNullOrEmpty(Podr.Text))
            {
                MessageHelper.Error("Все полня должны быть заполнены");
                return false;
            }

            if(!int.TryParse(Code.Text , out int i))
            {
                MessageHelper.Error("Код сотрудника имеет не числовой тип");
                return false;
            }

            if (!int.TryParse(Code.Text, out int ii))
            {
                MessageHelper.Error("Код подразделения имеет не числовой тип");
                return false;
            }

            return true;
        }

        //public static bool Add(string FirstName , string Name , string LastName , string Code , string Podr)
        //{

        //}
    }
}
