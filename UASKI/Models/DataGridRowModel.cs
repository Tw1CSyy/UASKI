namespace UASKI.Models
{
    /// <summary>
    /// Модель строчки в DataGridView
    /// </summary>
    public class DataGridRowModel
    {
        /// <summary>
        /// Список значений
        /// </summary>
       public string[] Values { get; private set; }
       
        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="param">Список значений</param>
       public DataGridRowModel(params string[] param)
       {
            Values = param;
       }
    }
}
