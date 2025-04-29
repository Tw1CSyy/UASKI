namespace UASKI.Core.SystemModels
{
    public class KofModel
    {
        /// <summary>
        /// Код подразделения исполнителя
        /// </summary>
        public int CodePodrIsp { get; set; }

        /// <summary>
        /// Инициалы исполнителя
        /// </summary>
        public string Isp { get; set; }

        /// <summary>
        /// Количество заданий
        /// </summary>
        public int Count{ get; set; }

        /// <summary>
        /// Количество опазданий
        /// </summary>
        public int CountOpz { get; set; }

        /// <summary>
        /// Количество дней опазданий
        /// </summary>
        public int CountDay { get; set; }

        /// <summary>
        /// Коэффициент качества
        /// </summary>
        public double Kof { get; set; }

        /// <summary>
        /// Строка Коэффициент качества
        /// </summary>
        public string KofString { get => GetKof(); }

        /// <summary>
        /// Возвращает коф. за период в строковом виде
        /// </summary>
        /// <returns></returns>
        private string GetKof()
        {
            if (Kof < 0)
                return "0";
            else if (Kof == 0 && Count == 0)
                return "-";
            else
                return Kof.ToString();
        }

    }
}
