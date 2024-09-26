using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Data.Context;
using UASKI.Data.Entityes;
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




    }
}
