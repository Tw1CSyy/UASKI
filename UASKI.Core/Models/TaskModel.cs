using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UASKI.Data;
using UASKI.Data.Entityes;
using UASKI.Data.Entyties;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class TaskModel
    {
        private static UAContext context = new UAContext();

        /// <summary>
        /// Код задания
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int IdIsp { get; private set; }

        /// <summary>
        /// Идентификатор контроллера
        /// </summary>
        public int IdCon { get; private set; }

        /// <summary>
        /// Дата срока
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary> 
        /// Идентификатор задачи
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public IspModel Isp { get; private set; }

        /// <summary>
        /// Контроллер
        /// </summary>
        public IspModel Con { get; private set; }

        /// <summary>
        /// Дней опазданий с учетом выходных и праздников
        /// </summary>
        public int DaysOpz { get; private set; }

        /// <summary>
        /// Создает экземпляр задачи
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <param name="idIsp">Код исполнителя</param>
        /// <param name="idCon">Код котроллера</param>
        /// <param name="date">Дата срока</param>
        public TaskModel(string code, int idIsp, int idCon, DateTime date)
        {
            Code = code;
            IdIsp = idIsp;
            IdCon = idCon;
            Date = date;
        }

        /// <summary>
        /// Создает экземпляр задачи
        /// </summary>
        /// <param name="entity">Объект TaskEntity</param>
        internal TaskModel(TaskEntity entity , IspModel isp , IspModel con , int dayOpz)
        {
            Code = entity.Code;
            IdIsp = entity.IdIsp;
            IdCon = entity.IdCon;
            Date = entity.Date;
            Id = entity.Id;

            Isp = isp;
            Con = con;
            DaysOpz = dayOpz;
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект TaskEntity</returns>
        private TaskEntity Get()
        {
            return new TaskEntity(Code, IdIsp, IdCon, Date , Id);
        }

        /// <summary>
        /// Добавляет новую задачу
        /// </summary>
        /// <returns>true - успешное выполнение</returns>
        public bool Add()
        {
            var entity = Get();
            return context.Add(entity);
        }

        /// <summary>
        /// Изменяет задачу
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public bool Update(int id)
        {
            var entity = Get();
            return context.Update(entity, id);
        }

        /// <summary>
        /// Удаляет задачу
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            var entity = context.Tasks.FirstOrDefault(c => c.Id == Id);
            var result = context.Delete(entity);
            return result;
        }

        /// <summary>
        /// Удаляет задачу из Task и добавляет в Arhiv
        /// </summary>
        /// <returns>Положительный или отрицательный результат</returns>
        public bool Close(int otm, DateTime dateClose)
        {
            var arhiv = new ArhivModel(this, dateClose, otm);
            var result = context.Add(arhiv.Get());
            
            if (!result)
                return false;

            return context.Delete(Get());

        }

        /// <summary>
        /// Меняет код исполнителя или котроллера в задаче
        /// </summary>
        /// <param name="oldCode">Старый код</param>
        /// <param name="newCode">Новый код</param>
        /// <returns>true - Успешная операция</returns>
        public bool EditCodeIsp(int oldCode , int newCode)
        {
            if(IdIsp == oldCode)
            {
                IdIsp = newCode;
            }

            if(IdCon == oldCode)
            {
                IdCon = newCode;
            }

            return context.Update(Get() , Id);
        }

        /// <summary>
        /// Возращает задачу по Id
        /// </summary>
        /// <param name="code">Id задачи</param>
        /// <returns></returns>
        public static TaskModel GetById(int code)
        {
            return GetList().FirstOrDefault(c => c.Id == code);
        }

        /// <summary>
        /// Возращает список задач
        /// </summary>
        public static List<TaskModel> GetList()
        {
            var ispList = context.Isps;
            var taskList = context.Tasks;
            var holidayList = context.Holidays;
            var result = new List<TaskModel>();

            foreach (var task in taskList)
            {
                var isp = ispList.FirstOrDefault(c => c.Code == task.IdIsp);
                var con = ispList.FirstOrDefault(c => c.Code == task.IdCon);
                var days = 0;

                for (DateTime date = task.Date; date < DateTime.Today;)
                {
                    if (holidayList.FirstOrDefault(c => c.Date == date) != null)
                    {
                        date = date.AddDays(1);
                        continue;
                    }

                    days++;
                    date = date.AddDays(1);
                }

                var item = new TaskModel(task, new IspModel(isp), new IspModel(con), days);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Проверяет код на правильность заполнения
        /// </summary>
        /// <param name="code">Код задачи</param>
        /// <returns>true - положительный ответ</returns>
        public static bool CheckCode(string code)
        {
            var c = code.ToArray();

            if (int.TryParse(c[1].ToString(), out int j1) || int.TryParse(c[2].ToString(), out int j2))
            {
                return false;
            }

            for (int i = 0; i < c.Length; i++)
            {
                if (i == 1 || i == 2)
                    continue;

                if (!int.TryParse(c[i].ToString(), out int j))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
