using Npgsql;
using System;
using System.Collections.Generic;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;

namespace UASKI.Data
{
    public class UAContext
    {
        public List<IspEntity> Isps { get => SelectIsp(); }
        public List<TaskEntity> Tasks { get => SelectTasks(); }
        public List<HolidayEntity> Holidays { get => SelectHolidays(); }
        public List<ArhivEntity> Arhiv { get => SelectArhiv(); }
        public List<PretEntity> Prets { get => SelectPret(); }


        /// <summary>
        /// Выборка из таблицы Isp
        /// </summary>
        /// <returns>Коллекцию объектов</returns>
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
        /// <returns>Коллекцию объектов</returns>
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
                    Convert.ToInt32(reader.GetValue(4)),
                    Convert.ToBoolean(reader.GetValue(5))
                );

                result.Add(item);

            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Выборка из таблицы Holidays
        /// </summary>
        /// <returns>Коллекцию объектов</returns>
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
        /// Выборка из таблицы Arhiv
        /// </summary>
        /// <returns>Коллекцию объектов</returns>
        private List<ArhivEntity> SelectArhiv()
        {
            var result = new List<ArhivEntity>();
            var query = "SELECT * FROM \"Arhiv\"";

            var command = new NpgsqlCommand(query, DataModel.Get());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new ArhivEntity
                (
                    reader.GetValue(0).ToString(),
                    Convert.ToInt32(reader.GetValue(1)),
                    Convert.ToInt32(reader.GetValue(2)),
                    Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToDateTime(reader.GetValue(4)),
                    Convert.ToInt32(reader.GetValue(5)),
                    Convert.ToInt32(reader.GetValue(6)),
                    Convert.ToBoolean(reader.GetValue(7))
                );

                result.Add(item);

            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Выборка из таблицы Pret
        /// </summary>
        /// <returns></returns>
        private List<PretEntity> SelectPret()
        {
            var result = new List<PretEntity>();
            var query = "SELECT * FROM \"Pret\"";

            var command = new NpgsqlCommand(query, DataModel.Get());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new PretEntity
                (
                    Convert.ToInt32(reader.GetValue(0)),
                    reader.GetValue(1).ToString(),
                    Convert.ToInt32(reader.GetValue(2)),
                    Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToInt32(reader.GetValue(4)),
                    Convert.ToInt32(reader.GetValue(5))
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
            var query = $"INSERT INTO \"Isp\" (\"Code\" , \"FirstName\" , \"Name\" , \"LastName\" , \"CodePodr\" ) " +
                $"VALUES ('{entity.Code}' , '{entity.FirstName}' , '{entity.Name}' , '{entity.LastName}' , '{entity.CodePodr}')";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Добавляет запись в таблицу Tasks
        /// </summary>
        /// <param name="entity">Объект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(TaskEntity entity)
        {
            var query = $"INSERT INTO \"Tasks\" (\"Cod\" , \"IdIsp\" , \"IdKon\" , \"Date\" , \"IsDouble\")" +
                $"VALUES ('{entity.Code}' , '{entity.IdIsp}' , '{entity.IdCon}' , '{entity.Date.Date}' , '{entity.IsDouble}')";

            return DataModel.Complite(query);

        }

        /// <summary>
        /// Добавляет запись в таблицу Holidays
        /// </summary>
        /// <param name="entity">Объект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(HolidayEntity entity)
        {
            var query = $"INSERT INTO \"Holidays\" (\"Date\") " +
                $"VALUES ('{entity.Date.Date}')";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Добавляет запись в таблицу Arhiv
        /// </summary>
        /// <param name="entity">Объкт класса</param>
        /// <returns></returns>
        public bool Add(ArhivEntity entity)
        {
            var query = $"INSERT INTO \"Arhiv\" (\"Cod\" , \"IdIsp\" , \"IdKon\" , \"Date\" , \"DateClose\" , \"Otm\" , \"IsDouble\") " +
               $"VALUES ('{entity.Code}' , '{entity.IdIsp}' , '{entity.IdCon}' , '{entity.Date.Date}' , '{entity.DateClose}' , '{entity.Otm}' , '{entity.IsDouble}')";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Добавляет запись в таблицу Pret
        /// </summary>
        /// <param name="entity">Объкт класса</param>
        /// <returns></returns>
        public bool Add(PretEntity entity)
        {
            var query = $"INSERT INTO \"Pret\" (\"Code\" , \"IdTask\" , \"Date\" , \"Otm\" , \"Type\") " +
               $"VALUES ('{entity.Code}' , '{entity.IdTask}' , '{entity.Date}' , '{entity.Otm}' , '{entity.Type}')";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Обновляет данные в таблице Pret
        /// </summary>
        /// <param name="pret">Объект entity</param>
        /// <param name="Id">id</param>
        /// <returns></returns>
        public bool Update(PretEntity pret, int Id)
        {

            var query = $"UPDATE \"Pret\" SET " +
                $"\"Code\" = '{pret.Code}' ," +
                $"\"IdTask\" = '{pret.IdTask}' ," +
                $"\"Date\" = '{pret.Date}' ," +
                $"\"Otm\" = '{pret.Otm}' , " +
                $"\"Type\" = '{pret.Type}'" +
                $"WHERE \"Id\" = '{Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Обновляет исполнителя
        /// </summary>
        /// <param name="isp">Модель исполнителя</param>
        /// <param name="code">Код исполнителя</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(IspEntity isp , int code)
        {
            
            var query = $"UPDATE \"Isp\" SET " +
                $"\"Code\" = '{isp.Code}', " +
                $"\"FirstName\" = '{isp.FirstName}' ," +
                $"\"Name\" = '{isp.Name}' ," +
                $"\"LastName\" = '{isp.LastName}' ," +
                $"\"CodePodr\" = '{isp.CodePodr}'" +
                $"WHERE \"Code\" = '{code}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Обновляет задачу
        /// </summary>
        /// <param name="task">Модель задачи</param>
        /// <param name="Id">Id задачи</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(TaskEntity task , int Id)
        {
            var query = $"UPDATE \"Tasks\" SET " +
                $"\"Cod\" = '{task.Code}' ," +
                $"\"IdIsp\" = '{task.IdIsp}' ," +
                $"\"IdKon\" = '{task.IdCon}' ," +
                $"\"IsDouble\" = '{task.IsDouble}' ," +
                $"\"Date\" = '{task.Date}' " +
                $"WHERE \"Id\" = '{Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Обновляет празднечный день
        /// </summary>
        /// <param name="holiday">Модель празднечного дня</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(HolidayEntity holiday)
        {
            var query = $"UPDATE \"Holidays\" SET \"Date\" = '{holiday.Date}' WHERE \"Id\" = '{holiday.Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Обновляет задачу в архиве
        /// </summary>
        /// <param name="task">Модель задачи</param>
        /// <param name="Id">Id задачи</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(ArhivEntity task, int Id)
        {
            
            var query = $"UPDATE \"Arhiv\" SET " +
                $"\"Cod\" = '{task.Code}' ," +
                $"\"IdIsp\" = '{task.IdIsp}' ," +
                $"\"IdKon\" = '{task.IdCon}' ," +
                $"\"Date\" = '{task.Date}' , " +
                $"\"DateClose\" = '{task.DateClose}' ," +
                $"\"IsDouble\" = '{task.IsDouble}' ," +
                $"\"Otm\" = '{task.Otm}'" +
                $"WHERE \"Id\" = '{Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Удаляет данные из таблицы Isp
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(IspEntity entity)
        {
            var query = $"DELETE FROM \"Isp\" WHERE \"Code\" = '{entity.Code}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Удаляет данные из таблицы Tasks
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(TaskEntity entity)
        {
            var query = $"DELETE FROM \"Tasks\" WHERE \"Id\" = '{entity.Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Удаляет данные из таблицы Arhiv
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(ArhivEntity entity)
        {
            var query = $"DELETE FROM \"Arhiv\" WHERE \"Id\" = '{entity.Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Удаляет данные из таблицы Holidays
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(HolidayEntity entity)
        {
            var query = $"DELETE FROM \"Holidays\" WHERE \"Id\" = '{entity.Id}'";

            return DataModel.Complite(query);
        }

        /// <summary>
        /// Удаляет данные из таблицы Pret
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(PretEntity entity)
        {
            var query = $"DELETE FROM \"Pret\" WHERE \"Id\" = '{entity.Id}'";

            return DataModel.Complite(query);
        }
    }
}
