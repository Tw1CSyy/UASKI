using System;

using UASKI.Data;

namespace UASKI.Core
{
    public class DataConnection
    {
        /// <summary>
        /// Удалось ли установить соедиение
        /// </summary>
        public bool IsConnection { get; private set; }

        /// <summary>
        /// Модель базы
        /// </summary>
        internal static UAContext Context { get; private set; }
       
        /// <summary>
        /// Создает объект класса, создает и открывает подключение
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        public DataConnection(string connectionString)
        {
            try
            {
                UAContext.Connection = new DataModel(connectionString);
                UAContext.ListenConnection = new DataModel(connectionString);
                UAContext.Connection.Open();
                UAContext.ListenConnection.Open();
                IsConnection = true;
                Context = new UAContext();
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
                UAContext.Connection.Close();
                UAContext.ListenConnection.Close();
                
            }
            catch (Exception)
            {

            }
        }
    }
}
