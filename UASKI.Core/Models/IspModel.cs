using System;
using System.Collections.Generic;
using System.Linq;
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
        public IspModel(IspEntity entity)
        {
            Code = entity.Code;
            FirstName = entity.FirstName;
            Name = entity.Name;
            LastName = entity.LastName;
            CodePodr = entity.CodePodr;
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
            return new IspModel(context.IspFirstOrDefult(code));
        }

    }

}
