namespace UASKI.Models
{
    /// <summary>
    /// Объект пункта списка меню первого уровня
    /// </summary>
    public class ItemMenuLevel1
    {
        /// <summary>
        /// Текст пункта
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Элементы второго уровня списка
        /// </summary>
        public ItemMenuLevel2[] Items { get; set; }

    }
}
