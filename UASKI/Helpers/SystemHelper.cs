using System.Windows.Forms;

namespace UASKI.Helpers
{
    public static class SystemHelper
    {
        /// <summary>
        /// Добавляет в listBox список элементов
        /// </summary>
        /// <param name="listBox">Объект listBox</param>
        /// <param name="items">Массив элементов</param>
        /// <param name="IsUppend">Элементы капсом</param>
        public static void WriteListBox(ListBox listBox , string[] items , bool? IsUppend = null)
        {
            listBox.Items.Clear();

            foreach (var item in items)
            {
                if (!IsUppend.HasValue)
                {
                    listBox.Items.Add(item);
                }
                else if (IsUppend.Value)
                {
                    listBox.Items.Add(item.ToUpper());
                }
                else
                {
                    listBox.Items.Add(item.ToLower());
                }
            }
        }
    }
}
