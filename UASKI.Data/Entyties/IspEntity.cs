namespace UASKI.Data.Entityes
{
    /// <summary>
    /// Модель элемента таблицы Isp
    /// </summary>
    public class IspEntity
    {
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
        /// Создает объект класса
        /// </summary>
        /// <param name="code">Код сотрудника</param>
        /// <param name="firstName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="lastName">Отчество</param>
        /// <param name="codePodr">Код подразделения</param>
        public IspEntity(int code , string firstName , string name , string lastName , int codePodr)
        {
            Code = code;
            FirstName = firstName;
            Name = name;
            LastName = lastName;
            CodePodr = codePodr;
        }
    }
}
