using System;
using System.Collections.Generic;
using UASKI.Data.Entityes;
using UASKI.Data.Requests;

namespace UASKI.Data.Context
{
    public class UAContext
    {
        public List<IspEntity> Isps { get => SelectIsp(); }
        public List<HolidayEntity> Holidays;

        #region Select

        /// <summary>
        /// Заполняет данные из таблицы Isp
        /// </summary>
        /// <returns>Колекцию объектов таблицы</returns>
        private List<IspEntity> SelectIsp()
        {
            var request = new SelectRequest("Isp", "Code", "FirstName", "Name", "LastName", "CodePodr");
            var result = ContextService.SelectValues(request);

            var model = new List<IspEntity>();

            foreach (var item in result)
            {
                model.Add(new IspEntity()
                {
                    Code = Convert.ToInt32(item[0]),
                    FirstName = item[1],
                    Name = item[2],
                    LastName = item[3],
                    CodePodr = Convert.ToInt32(item[4])
                });
            }

            return model;
        }

        #endregion



    }
}
