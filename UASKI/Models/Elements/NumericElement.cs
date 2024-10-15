using System.Windows.Forms;

namespace UASKI.Models.Elements
{
    /// <summary>
    /// Модель для NumericUpDown
    /// </summary>
    public class NumericElement : BaseElement
    {
        /// <summary>
        /// NumericUpDown
        /// </summary>
        public NumericUpDown NumericUpDown { get; set; }

        /// <summary>
        /// Значение элемента
        /// </summary>
        public int Value { get => (int)NumericUpDown.Value; }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="num">NumericUpDown</param>
        /// <param name="label">Label ошибки</param>
        public NumericElement(NumericUpDown num , Label label)
        {
            NumericUpDown = num;
            ErrorLabel = label;
        }
    }
}
