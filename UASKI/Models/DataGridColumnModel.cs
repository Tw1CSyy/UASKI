using System;
using System.Windows.Forms;

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
        /// Тип колонки
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Сколько масимально символов может быть строка для определенния ее ширины
        /// </summary>
        public int MaxSymbolsByWidth { get; private set; } = 0;

        /// <summary>
        /// Мод заполнения колонки
        /// </summary>
        public DataGridViewAutoSizeColumnMode Mode { get; private set; } = DataGridViewAutoSizeColumnMode.Fill;

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="name">Название колонки</param>
        /// <param name="visible">Видни ли колонка</param>
        /// <param name="mode">Мод заполнения колонки</param>
        public DataGridColumnModel(string name, Type type, bool visible = true, DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.Fill)
        {
            Name = name;
            Visible = visible;
            Type = type;
            Mode = mode;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="name">Название колонки</param>
        /// <param name="visible">Видни ли колонка</param>
        /// <param name="mode">Мод заполнения колонки</param>
        public DataGridColumnModel(string name, bool visible = true, DataGridViewAutoSizeColumnMode mode = DataGridViewAutoSizeColumnMode.Fill)
        {
            Name = name;
            Visible = visible;
            Type = typeof(string);
            Mode = mode;
        }

        /// <summary>
        /// Создает объект класса
        /// </summary>
        /// <param name="name">Название колонки</param>
        /// <param name="maxSymbolsByWidth">Сколько масимально символов может быть строка для определенния ее ширины</param>
        public DataGridColumnModel(string name, int maxSymbolsByWidth)
        {
            Name = name;
            MaxSymbolsByWidth = maxSymbolsByWidth;
            Type = typeof(string);
            Visible = true;
        }

    }
}
