using System;

namespace UASKI.Core
{
    public class DataConnection
    {
        /// <summary>
        /// Удалось ли установить соедиение
        /// </summary>
        public bool IsConnection { get; private set; }

        /// <summary>
        /// Создает объект класса, создает и открывает подключение
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        public DataConnection(string connectionString)
        {
            try
            {
                DataModel.CreateConnection(connectionString);
                DataModel.Open();
                IsConnection = true;
            }
            catch (Exception)
            {
                IsConnection = false;
            }
        }

        /// <summary>
        /// Закрывает подключение
        /// </summary>
        public static void Close()
        {
            try
            {
                DataModel.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
