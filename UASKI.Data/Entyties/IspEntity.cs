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
        public int Code { get; set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Код подразделения
        /// </summary>
        public int CodePodr { get; set; }
    }
}
