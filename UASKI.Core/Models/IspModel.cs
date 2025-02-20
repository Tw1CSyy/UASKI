using System;
using System.Collections.Generic;
using System.Linq;
using UASKI.Core.SystemModels;
using UASKI.Data;
using UASKI.Data.Entityes;

namespace UASKI.Core.Models
{
    /// <summary>
    /// Модель Исполнителя/Котроллера
    /// </summary>
    public class IspModel
    {
        private static readonly UAContext context = new UAContext();

        /// <summary>
        /// Код сотрудника
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string FirstName { get; private set; } = string.Empty;

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string LastName { get; private set; } = string.Empty;

        /// <summary>
        /// Код подразделения
        /// </summary>
        public int CodePodr { get; private set; }

        /// <summary>
        /// Инициалы с кодом подразделения
        /// </summary>
        public string InizByCode { get => $"{CodePodr} {FirstName} {Name.ToUpper()[0]}. {LastName.ToUpper()[0]}."; }

        /// <summary>
        /// Инициалы без кода предприятия
        /// </summary>
        public string InizNotCode { get => $"{FirstName} {Name.ToUpper()[0]}. {LastName.ToUpper()[0]}."; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код сотрудника</param>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="lastName">Отчество</param>
        /// <param name="codePodr">Код подразделения</param>
        public IspModel(int code, string firstName, string name, string lastName, int codePodr)
        {
            Code = code;
            FirstName = firstName;
            Name = name;
            LastName = lastName;
            CodePodr = codePodr;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="entity">Сущность сотрудника</param>
        internal IspModel(IspEntity entity)
        {
           if(entity != null)
           {
                Code = entity.Code;
                FirstName = entity.FirstName;
                Name = entity.Name;
                LastName = entity.LastName;
                CodePodr = entity.CodePodr;
           }
        }

        /// <summary>
        /// Формирует объект Entity
        /// </summary>
        /// <returns>Объект IspEntity</returns>
        public IspEntity Get() 
        {
            return new IspEntity(Code, FirstName, Name, LastName, CodePodr);
        }

        /// <summary>
        /// Добавляет в базу исполнителя
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public bool Add()
        {
            var item = Get();
            return context.Add(item);
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="oldCode">Старый код сотрудника</param>
        /// <returns>true - успешная операция</returns>
        public bool Update(int oldCode)
        {
            var item = Get();
            var result = context.Update(item, oldCode);

            if (!result)
                return false;

            if (Code != oldCode)
            {
                var taskList = TaskModel.GetList().Where(c => c.IdCon == oldCode || c.IdIsp == oldCode).ToList();
                var arhivList = ArhivModel.GetList().Where(c => c.IdCon == oldCode || c.IdIsp == oldCode).ToList();

                foreach (var task in taskList)
                {
                    task.EditCodeIsp(oldCode, Code);
                }

                foreach (var task in arhivList)
                {
                    task.EditCodeIsp(oldCode, Code);
                }
            }

            return result;
        }

        /// <summary>
        /// Удаляет Исполнителя-Котроллера если на нем нет задач
        /// </summary>
        /// <returns>false - На исполнителе есть задачи или неуспешное выполнение запроса</returns>
        public bool Delete()
        {
            var taskList = TaskModel.GetList().Count(c => c.IdCon == Code || c.IdIsp == Code);

            if (taskList != 0)
                return false;

            taskList = ArhivModel.GetList().Count(c => c.IdCon == Code || c.IdIsp == Code);

            if (taskList != 0)
                return false;

            return context.Delete(Get());
        }

        /// <summary>
        /// Возращает список пользователей
        /// </summary>
        /// <returns></returns>
        public static List<IspModel> GetList()
        {
            return context.Isps
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.Name)
                .ThenBy(c => c.LastName)
                .Select(c => new IspModel(c))
                .ToList();
        }

        /// <summary>
        /// Возвращает объект исполнителя по коду
        /// </summary>
        /// <param name="code">Код</param>
        /// <returns>Объект класса</returns>
        public static IspModel GetByCode(int code)
        {
            return GetList().FirstOrDefault(c => c.Code == code);
        }

        /// <summary>
        /// Расчитывает коэфициент качества по списку задач
        /// </summary>
        /// <param name="tasks">Список архивных задач, отфильтрованный по дате и IspId</param>
        /// <param name="listPret">Список претензий из базы</param>
        /// <param name="holy">Список выходных из базы</param>
        /// <param name="contextTask">Список задач отфильтровнный по опазданию и IdIsp</param>
        /// <returns></returns>
        private double GetCof(List<ArhivModel> tasks, List<PretModel> listPret, List<HolidayModel> holy, List<TaskModel> contextTask)
        {
            if (tasks.Count == 0)
            {
                return 0;
            }

            int countTask = 0, countPret = 0, countRez = 0, countOpz = 0, countCof = 0;
           
            foreach (var task in tasks)
            {
                countTask += Convert.ToInt32(task.Code[0].ToString()) * task.Otm;

                var prets = listPret.Where(c => c.IdTask == task.Id && c.Type == 1).ToList();
                var rezs = listPret.Where(c => c.IdTask == task.Id && c.Type == 2).ToList();

                countPret += prets.Sum(c => Convert.ToInt32(c.Code[0].ToString()) * c.Otm);
                countRez += rezs.Sum(c => Convert.ToInt32(c.Code[0].ToString()) * c.Otm);

                if (task.GetDaysOpz(holy) != 0)
                {
                    countOpz += Convert.ToInt32(task.Code[0].ToString()) * task.GetDaysOpz(holy);
                }

                countCof += Convert.ToInt32(task.Code[0].ToString()) + prets.Sum(c => Convert.ToInt32(c.Code[0].ToString())) + rezs.Sum(c => Convert.ToInt32(c.Code[0].ToString()));
            }

            foreach (var t in contextTask)
            {
                countOpz += Convert.ToInt32(t.Code[0].ToString()) * t.GetDaysOpz(holy);
            }

            double result = (countTask + countPret + countRez - 0.2 * countOpz) / (5 * countCof);
            result = Math.Round(result, 2);

            return result;
        }

        /// <summary>
        /// Расчитывает данные для коофициента качества для исполнителя
        /// </summary>
        /// <param name="contextTask">Список задач отфильтровнный по опазданию и IdIsp</param>
        /// <param name="contextArhiv">Список архивных задач, отфильтрованный по дате и IspId</param>
        /// <param name="holy">Список выходных из базы</param>
        /// <param name="prets">Список претензий из базы</param>
        /// <returns></returns>
        public KofModel GetKofModel(List<TaskModel> contextTask, List<ArhivModel> contextArhiv, List<HolidayModel> holy , List<PretModel> prets)
        {
            var item = new KofModel();

            item.Isp = InizByCode;
            item.Count = contextArhiv.Count() + contextTask.Count;
            item.CountOpz = contextArhiv.Count(c => c.GetDaysOpz(holy) != 0) + contextTask.Count(c => c.GetDaysOpz(holy) != 0);
            item.CountDay = contextArhiv.Sum(c => c.GetDaysOpz(holy)) + contextTask.Sum(c => c.GetDaysOpz(holy));
            item.Kof = GetCof(contextArhiv, prets, holy, contextTask);

            return item;
        }
    }

}
