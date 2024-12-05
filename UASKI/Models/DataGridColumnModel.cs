using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASKI.Models
{
    public class DataGridColumnModel
    {
        /// <summary>
        /// Название колонки
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Видна ли колонка
        /// </summary>
        public bool Visible { get; private set; }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="name">Название колонки</param>
        /// <param name="visible">Видни ли колонка</param>
        public DataGridColumnModel(string name , bool visible = true)
        {
            Name = name;
            Visible = visible;
        }
    }
}
