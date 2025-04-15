using UASKI.Enums;

namespace UASKI.Models
{
    /// <summary>
    /// Абстрактный класс для объектов страницы
    /// </summary>
    public abstract class BasePageEdit : BasePage
    {
        public BasePageEdit(int index, TypePage type) : base(index, type) { }
    }
}
