namespace UASKI.Models
{
    /// <summary>
    /// Объект пункта списка меню второго уровня
    /// </summary>
    public class ItemMenuLevel2
    {
        
        /// <summary>
        /// Текст пункта
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Страница для отображения
        /// </summary>
        public BasePage Page { get; set; }
    }
}
