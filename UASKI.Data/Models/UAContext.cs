using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Npgsql;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;

namespace UASKI.Data
{
    public class UAContext
    {
        private CancellationTokenSource _token;
        public static DataModel Connection { get; set; }
        public static DataModel ListenConnection { get; set; }
        private bool IsUpdateData { get; set; } = false;

        public List<IspEntity> Isps { get; private set; }
        public List<TaskEntity> Tasks { get; private set; }
        public List<HolidayEntity> Holidays { get; private set; }
        public List<ArhivEntity> Arhiv { get; private set; }
        public List<PretEntity> Prets { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        public UAContext()
        {
            UploadContext();

            using (var cmd = new NpgsqlCommand("LISTEN tasks_channel;LISTEN arhiv_channel;LISTEN isps_channel;LISTEN pret_channel;LISTEN holy_channel;", ListenConnection.Get()))
            {
                cmd.ExecuteNonQuery();
                _token = new CancellationTokenSource();
                Task.Run(() => ListenForNotifications(_token.Token));
            }
        }

        /// <summary>
        /// Обновляет данные в Context
        /// </summary>
        public void UploadContext()
        {
            Isps = SelectIsp();
            Tasks = SelectTasks();
            Holidays = SelectHolidays();
            Arhiv = SelectArhiv();
            Prets = SelectPret();
        }

        /// <summary>
        /// Настраивает слежку за тригерами
        /// </summary>
        /// <param name="token">Токен ассихроности</param>
        /// <returns></returns>
        private async Task ListenForNotifications(CancellationToken token)
        {
            ListenConnection.Get().Notification += (sender, e) =>
            {
                if(!IsUpdateData)
                {
                    switch (e.Channel)
                    {
                        case "tasks_channel":
                            Tasks = SelectTasks();
                            break;
                        case "arhiv_channel":
                            Arhiv = SelectArhiv();
                            break;
                        case "holy_channel":
                            Holidays = SelectHolidays();
                            break;
                        case "pret_channel":
                            Prets = SelectPret();
                            break;
                        case "isps_channel":
                            Isps = SelectIsp();
                            break;

                    }
                }

                IsUpdateData = false;
            };

            while (true)
            {
                await ListenConnection.Get().WaitAsync(token);

            }
        }

        /// <summary>
        /// Выборка из таблицы Isp
        /// </summary>
        /// <returns>Коллекцию объектов</returns>
        private List<IspEntity> SelectIsp()
        {
            var result = new List<IspEntity>();
            var query = $"SELECT * FROM \"Isp\"";

            var command = new NpgsqlCommand(query,Connection.Get());
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

            var command = new NpgsqlCommand(query,Connection.Get());
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

            var command = new NpgsqlCommand(query,Connection.Get());
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

            var command = new NpgsqlCommand(query,Connection.Get());
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

            var command = new NpgsqlCommand(query,Connection.Get());
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
            IsUpdateData = true;
            var query = $"INSERT INTO \"Isp\" (\"Code\", \"FirstName\", \"Name\", \"LastName\", \"CodePodr\" ) " +
                $"VALUES ('{entity.Code}', '{entity.FirstName}', '{entity.Name}', '{entity.LastName}', '{entity.CodePodr}')";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var id = GetMaxIdIsp();
            var newEntity = new IspEntity(id, entity.FirstName, entity.Name, entity.LastName, entity.CodePodr);
            Isps.Add(newEntity);
            return result;
            
        }

        /// <summary>
        /// Добавляет запись в таблицу Tasks
        /// </summary>
        /// <param name="entity">Объект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(TaskEntity entity)
        {
            IsUpdateData = true;
            var query = $"INSERT INTO \"Tasks\" (\"Cod\", \"IdIsp\", \"IdKon\", \"Date\", \"IsDouble\")" +
                $"VALUES ('{entity.Code}', '{entity.IdIsp}', '{entity.IdCon}', '{entity.Date.Date}', '{entity.IsDouble}')";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var id = GetMaxIdTasks();
            var newEntity = new TaskEntity(entity.Code, entity.IdIsp, entity.IdCon, entity.Date, id , entity.IsDouble);
            Tasks.Add(newEntity);
            return result;
            
        }

        /// <summary>
        /// Добавляет запись в таблицу Holidays
        /// </summary>
        /// <param name="entity">Объект класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Add(HolidayEntity entity)
        {
            IsUpdateData = true;
            var query = $"INSERT INTO \"Holidays\" (\"Date\") " +
                $"VALUES ('{entity.Date.Date}')";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var id = GetMaxIdHoliday();
            var newEntity = new HolidayEntity(id, entity.Date);
            Holidays.Add(newEntity);
            return result;
        }

        /// <summary>
        /// Добавляет запись в таблицу Arhiv
        /// </summary>
        /// <param name="entity">Объкт класса</param>
        /// <returns></returns>
        public bool Add(ArhivEntity entity)
        {
            IsUpdateData = true;
            var query = $"INSERT INTO \"Arhiv\" (\"Cod\", \"IdIsp\", \"IdKon\", \"Date\", \"DateClose\", \"Otm\", \"IsDouble\") " +
               $"VALUES ('{entity.Code}', '{entity.IdIsp}', '{entity.IdCon}', '{entity.Date.Date}', '{entity.DateClose}', '{entity.Otm}', '{entity.IsDouble}')";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var id = GetMaxIdArhiv();
            var newEntity = new ArhivEntity(entity.Code, entity.IdIsp, entity.IdCon, entity.Date, entity.DateClose, entity.Otm, id, entity.IsDouble);
            Arhiv.Add(newEntity);
            return result;
            
        }

        /// <summary>
        /// Добавляет запись в таблицу Pret
        /// </summary>
        /// <param name="entity">Объкт класса</param>
        /// <returns></returns>
        public bool Add(PretEntity entity)
        {
            IsUpdateData = true;
            var query = $"INSERT INTO \"Pret\" (\"Code\", \"IdTask\", \"Date\", \"Otm\", \"Type\") " +
               $"VALUES ('{entity.Code}', '{entity.IdTask}', '{entity.Date}', '{entity.Otm}', '{entity.Type}')";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var id = GetMaxIdPret();
            var newEntity = new PretEntity(id, entity.Code, entity.IdTask, entity.Date, entity.Otm, entity.Type);
            Prets.Add(newEntity);
            return result;
        }

        /// <summary>
        /// Обновляет данные в таблице Pret
        /// </summary>
        /// <param name="pret">Объект entity</param>
        /// <param name="Id">id</param>
        /// <returns></returns>
        public bool Update(PretEntity pret, int Id)
        {
            IsUpdateData = true;

            var query = $"UPDATE \"Pret\" SET " +
                $"\"Code\" = '{pret.Code}'," +
                $"\"IdTask\" = '{pret.IdTask}'," +
                $"\"Date\" = '{pret.Date}'," +
                $"\"Otm\" = '{pret.Otm}', " +
                $"\"Type\" = '{pret.Type}'" +
                $"WHERE \"Id\" = '{Id}'";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var en = Prets.First(c => c.Id == Id);
            Prets.Remove(en);
            Prets.Add(pret);
            return result;
        }

        /// <summary>
        /// Обновляет исполнителя
        /// </summary>
        /// <param name="isp">Модель исполнителя</param>
        /// <param name="code">Код исполнителя</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(IspEntity isp, int code)
        {
            IsUpdateData = true;

            var query = $"UPDATE \"Isp\" SET " +
                $"\"Code\" = '{isp.Code}', " +
                $"\"FirstName\" = '{isp.FirstName}'," +
                $"\"Name\" = '{isp.Name}'," +
                $"\"LastName\" = '{isp.LastName}'," +
                $"\"CodePodr\" = '{isp.CodePodr}'" +
                $"WHERE \"Code\" = '{code}'";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var en = Isps.First(c => c.Code == code);
            Isps.Remove(en);
            Isps.Add(isp);
            return result;
        }

        /// <summary>
        /// Обновляет задачу
        /// </summary>
        /// <param name="task">Модель задачи</param>
        /// <param name="Id">Id задачи</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(TaskEntity task, int Id)
        {
            IsUpdateData = true;

            var query = $"UPDATE \"Tasks\" SET " +
                $"\"Cod\" = '{task.Code}'," +
                $"\"IdIsp\" = '{task.IdIsp}'," +
                $"\"IdKon\" = '{task.IdCon}'," +
                $"\"IsDouble\" = '{task.IsDouble}'," +
                $"\"Date\" = '{task.Date}' " +
                $"WHERE \"Id\" = '{Id}'";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var en = Tasks.First(c => c.Id == Id);
            Tasks.Remove(en);
            Tasks.Add(task);
            return result;
        }

        /// <summary>
        /// Обновляет празднечный день
        /// </summary>
        /// <param name="holiday">Модель празднечного дня</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(HolidayEntity holiday)
        {
            IsUpdateData = true;
            var query = $"UPDATE \"Holidays\" SET \"Date\" = '{holiday.Date}' WHERE \"Id\" = '{holiday.Id}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var en = Holidays.First(c => c.Id == holiday.Id);
            Holidays.Remove(en);
            Holidays.Add(holiday);
            return result;
        }

        /// <summary>
        /// Обновляет задачу в архиве
        /// </summary>
        /// <param name="task">Модель задачи</param>
        /// <param name="Id">Id задачи</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Update(ArhivEntity task, int Id)
        {
            IsUpdateData = true;

            var query = $"UPDATE \"Arhiv\" SET " +
                $"\"Cod\" = '{task.Code}'," +
                $"\"IdIsp\" = '{task.IdIsp}'," +
                $"\"IdKon\" = '{task.IdCon}'," +
                $"\"Date\" = '{task.Date}', " +
                $"\"DateClose\" = '{task.DateClose}'," +
                $"\"IsDouble\" = '{task.IsDouble}'," +
                $"\"Otm\" = '{task.Otm}'" +
                $"WHERE \"Id\" = '{Id}'";

            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var en = Arhiv.First(c => c.Id == Id);
            Arhiv.Remove(en);
            Arhiv.Add(task);
            return result;
        }

        /// <summary>
        /// Удаляет данные из таблицы Isp
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(IspEntity entity)
        {
            IsUpdateData = true;
            var query = $"DELETE FROM \"Isp\" WHERE \"Code\" = '{entity.Code}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var element = Isps.First(c => c.Code == entity.Code);
            Isps.Remove(element);
            return result;
        }

        /// <summary>
        /// Удаляет данные из таблицы Tasks
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(TaskEntity entity)
        {
            IsUpdateData = true;
            var query = $"DELETE FROM \"Tasks\" WHERE \"Id\" = '{entity.Id}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var element = Tasks.First(c => c.Id == entity.Id);
            Tasks.Remove(element);
            return result;
        }

        /// <summary>
        /// Удаляет данные из таблицы Arhiv
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(ArhivEntity entity)
        {
            IsUpdateData = true;
            var query = $"DELETE FROM \"Arhiv\" WHERE \"Id\" = '{entity.Id}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var element = Arhiv.First(c => c.Id == entity.Id);
            Arhiv.Remove(element);
            return result;
        }

        /// <summary>
        /// Удаляет данные из таблицы Holidays
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(HolidayEntity entity)
        {
            IsUpdateData = true;
            var query = $"DELETE FROM \"Holidays\" WHERE \"Id\" = '{entity.Id}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var element = Holidays.First(c => c.Id == entity.Id);
            Holidays.Remove(element);
            return result;
        }

        /// <summary>
        /// Удаляет данные из таблицы Pret
        /// </summary>
        /// <param name="entity">Модель класса</param>
        /// <returns>Положительный или отрицательный ответ</returns>
        public bool Delete(PretEntity entity)
        {
            IsUpdateData = true;
            var query = $"DELETE FROM \"Pret\" WHERE \"Id\" = '{entity.Id}'";
            var result = Connection.Complite(query);

            if (result == false)
                return false;

            var element = Prets.First(c => c.Id == entity.Id);
            Prets.Remove(element);
            return result;
        }

        /// <summary>
        /// Возвращает максимальный Id в таблице Isp
        /// </summary>
        /// <returns>Max Id или 0</returns>
        public int GetMaxIdIsp()
        {
            var command = new NpgsqlCommand($"SELECT MAX(\"Code\") FROM \"Isp\"", Connection.Get());
            var reader = command.ExecuteReader();
            int result = 0;

            while (reader.Read())
            {
                result = Convert.ToInt32(reader.GetValue(0));
            }

            reader.Close();
            return result;
        }

        /// <summary>
        /// Возвращает максимальный Id в таблице Tasks
        /// </summary>
        /// <returns>Max Id или 0</returns>
        public int GetMaxIdTasks()
        {
            return Connection.GetMaxId("Tasks");
        }

        /// <summary>
        /// Возвращает максимальный Id в таблице Arhiv
        /// </summary>
        /// <returns>Max Id или 0</returns>
        public int GetMaxIdArhiv()
        {
            return Connection.GetMaxId("Arhiv");
        }

        /// <summary>
        /// Возвращает максимальный Id в таблице Holidays
        /// </summary>
        /// <returns>Max Id или 0</returns>
        public int GetMaxIdHoliday()
        {
            return Connection.GetMaxId("Holidays");
        }

        /// <summary>
        /// Возвращает максимальный Id в таблице Pret
        /// </summary>
        /// <returns>Max Id или 0</returns>
        public int GetMaxIdPret()
        {
            return Connection.GetMaxId("Pret");
        }
    }
}
