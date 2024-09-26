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
                (
                    Convert.ToInt32(reader.GetValue(0)),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(),
                    reader.GetValue(3).ToString(),
                    Convert.ToInt32(reader.GetValue(4))
                );

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
                (
                    reader.GetValue(0).ToString(),
                    Convert.ToInt32(reader.GetValue(1)),
                    Convert.ToInt32(reader.GetValue(2)),
                    Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToBoolean(reader.GetValue(4)),
                    Convert.ToDateTime(reader.GetValue(5)),
                    Convert.ToInt32(reader.GetValue(6)),
                    reader.GetValue(7).ToString()
                );

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
                (
                    Convert.ToInt32(reader.GetValue(1)),
                    Convert.ToDateTime(reader.GetValue(0))
                );

                result.Add(item);
            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Добавляет запись в таблицу Isp
        /// </summary>
        /// <param name="entity">Объект класса</param>
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
            var query = $"INSERT INTO \"Tasks\" (\"Cod\" , \"IdIsp\" , \"IdKon\" , \"Date\" , \"IsClose\" , \"DateClose\" , \"Otm\" , \"Number\") " +
                $"VALUES ('{entity.Code}' , '{entity.IdIsp}' , '{entity.IdCon}' , '{entity.Date.Date}' , '{entity.IsClose}' , '{entity.DateClose.Date}' , '{entity.Otm}' , '{entity.Num}')";

            return DataModel.Complite(query);

        }

        /// <summary>
        /// Добавляет запись в таблицу Holidays
        /// </summary>
        /// <param name="entity">Объект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(HolidayEntity entity)
        {
            var query = $"INSERT INTO \"Holidays\" (Id , Date) " +
                $"VALUES ('{entity.Id}' , '{entity.Date}')";

            return DataModel.Complite(query);
        }
    }

}
