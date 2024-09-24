using System;
using System.Collections.Generic;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;
using UASKI.Data.Requests;
using UASKI.Data.Models;

namespace UASKI.Data.Context
{
    public class UAContext
    {
        public List<IspEntity> Isps { get => SelectIsp(); }
        public List<TaskEntity> Tasks { get => SelectTasks(); }
        public List<HolidayEntity> Holidays;


        /// <summary>
        /// Заполняет данные из таблицы Isp
        /// </summary>
        /// <returns>Колекцию объектов таблицы</returns>
        private List<IspEntity> SelectIsp()
        {
            var request = new SelectRequest("Isp", "Code", "FirstName", "Name", "LastName", "CodePodr");
            var result = ContextService.GetData(request);

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

        /// <summary>
        /// Заполняет данные из таблицы Tasks
        /// </summary>
        /// <returns>Колекцию объектов таблицы</returns>
        private List<TaskEntity> SelectTasks()
        {
            var req = new SelectRequest("Tasks", "Cod", "IdIsp", "IdKon", "Date", "IsClose", "DateClose", "Otm", "Num");
            var result = ContextService.GetData(req);

            var model = new List<TaskEntity>();

            foreach (var item in result)
            {
                var itm = new TaskEntity
                {
                    Code = item[0],
                    IdIsp = Convert.ToInt32(item[1]),
                    IdCon = Convert.ToInt32(item[2]),
                    Date = Convert.ToDateTime(item[3]),
                    IsClose = Convert.ToBoolean(item[4]),
                    Num = item[7]
                };

                if (!string.IsNullOrEmpty(item[5]))
                {
                    itm.DateClose = Convert.ToDateTime(item[5]);
                }

                if (!string.IsNullOrEmpty(item[6]))
                {
                    itm.Otm = Convert.ToInt32(item[6]);
                }

                model.Add(itm);
            }

            return model;
        }

        public void Add(TaskEntity entity)
        {
            var req = new AddRequest("Tasks" , new List<ColumnValueModel>
            {
                new ColumnValueModel("Cod" , entity.Code),
                new ColumnValueModel("IdIsp" , entity.IdIsp),
                new ColumnValueModel("IdKon" , entity.IdCon),
                new ColumnValueModel("Date" , entity.Date),
                new ColumnValueModel("IsClose" , entity.IsClose)
            });
        }
    }
}
