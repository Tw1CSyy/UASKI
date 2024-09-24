using Npgsql;
using System;
using System.Collections.Generic;
using UASKI.Data.Entityes;
using UASKI.Models;

namespace UASKI.Data.Context
{
    public class UAContext
    {
        public List<IspEntity> Isps { get => SelectIsp(); }
        public List<TaskEntity> Tasks { get => SelectTasks(); }
        public List<HolidayEntity> Holidays { get => SelectHolidays(); }



        /// <summary>
        /// Выборка из таблицы Isp
        /// </summary>
        /// <returns>Колекцию объектов</returns>
        private List<IspEntity> SelectIsp()
        {
            var result = new List<IspEntity>();
            var query = $"SELECT * FROM \"Isp\"";

            var command = new NpgsqlCommand(query, DataModel.Get());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new IspEntity
                {
                    Code = Convert.ToInt32(reader.GetValue(0)),
                    FirstName = reader.GetValue(1).ToString(),
                    Name = reader.GetValue(2).ToString(),
                    LastName = reader.GetValue(3).ToString(),
                    CodePodr = Convert.ToInt32(reader.GetValue(4))
                };

                result.Add(item);
            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Выборка из таблицы Tasks
        /// </summary>
        /// <returns>Колекцию объектов</returns>
        private List<TaskEntity> SelectTasks()
        {
            var result = new List<TaskEntity>();
            var query = "SELECT * FROM \"Tasks\"";

            var command = new NpgsqlCommand(query, DataModel.Get());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new TaskEntity
                {
                    Code = reader.GetValue(0).ToString(),
                    IdIsp = Convert.ToInt32(reader.GetValue(1)),
                    IdCon = Convert.ToInt32(reader.GetValue(2)),
                    Date = Convert.ToDateTime(reader.GetValue(3)),
                    IsClose = Convert.ToBoolean(reader.GetValue(4)),
                    DateClose = Convert.ToDateTime(reader.GetValue(5)),
                    Otm = Convert.ToInt32(reader.GetValue(6)),
                    Num = reader.GetValue(7).ToString()
                };

                result.Add(item);

            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Выборка из таблицы Holidays
        /// </summary>
        /// <returns>Колекцию объектов</returns>
        private List<HolidayEntity> SelectHolidays()
        {
            var result = new List<HolidayEntity>();
            var query = "SELECT * FROM \"Holidays\"";

            var command = new NpgsqlCommand(query, DataModel.Get());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new HolidayEntity
                {
                    Date = Convert.ToDateTime(reader.GetValue(0)),
                    Id = Convert.ToInt32(reader.GetValue(1))
                };

                result.Add(item);
            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Добавляет запись в таблицу Isp
        /// </summary>
        /// <param name="entity">Обхект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(IspEntity entity)
        {
            var query = $"INSERT INTO \"Isp\" (Code , FirstName , Name , LastName , CodePodr) " +
                $"VALUES ('{entity.Code}' , '{entity.FirstName}' , '{entity.Name}' , '{entity.LastName}' , '{entity.CodePodr}')";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Добавляет запись в таблицу Tasks
        /// </summary>
        /// <param name="entity">Обхект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(TaskEntity entity)
        {
            if (!entity.IsClose)
            {
                var query = $"INSERT INTO \"Tasks\" (\"Cod\" , \"IdIsp\" , \"IdKon\" , \"Date\" , \"IsClose\") " +
                $"VALUES ('{entity.Code}' , '{entity.IdIsp}' , '{entity.IdCon}' , '{entity.Date}' , '{entity.IsClose}')";

                return DataModel.Complite(query);
            }
            else
            {
                var query = $"INSERT INTO \"Tasks\" (\"Cod\" , \"IdIsp\" , \"IdKon\" , \"Date\" , \"IsClose\" , \"DateClose\" , \"Otm\" , \"Num\") " +
                $"VALUES ('{entity.Code}' , '{entity.IdIsp}' , '{entity.IdCon}' , '{entity.Date}' , '{entity.IsClose}' , '{entity.DateClose}' , '{entity.Otm}' , '{entity.Num}')";

                return DataModel.Complite(query);
            }

        }

        /// <summary>
        /// Добавляет запись в таблицу Holidays
        /// </summary>
        /// <param name="entity">Обхект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(HolidayEntity entity)
        {
            var query = $"INSERT INTO \"Holidays\" (Id , Date) " +
                $"VALUES ('{entity.Id}' , '{entity.Date}')";

            return DataModel.Complite(query);
        }
    }

}
