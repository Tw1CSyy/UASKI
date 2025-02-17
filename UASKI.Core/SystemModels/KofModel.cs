using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASKI.Core.SystemModels
{
    public class KofModel
    {
        /// <summary>
        /// Инициалы исполнителя
        /// </summary>
        public string Isp { get; set; }

        /// <summary>
        /// Количество заданий за выбраный период
        /// </summary>
        public int CountPeriod { get; set; }

        /// <summary>
        /// Количество заданий за месяц
        /// </summary>
        public int CountMonth { get; set; }

        /// <summary>
        /// Количество опазданий за выбраный период
        /// </summary>
        public int CountOpzPeriod { get; set; }

        /// <summary>
        /// Количество опазданий за месяц
        /// </summary>
        public int CountOpzMonth { get; set; }

        /// <summary>
        /// Количество дней опазданий за выбранный период
        /// </summary>
        public int CountDayPeriod { get; set; }

        /// <summary>
        /// Количество дней опазданий за месяц
        /// </summary>
        public int CountDayMonth { get; set; }

        /// <summary>
        /// Коэффициент качества за выбранный период
        /// </summary>
        public double KofPeriod { get; set; }

        /// <summary>
        /// Коэффициент качества за месяц
        /// </summary>
        public double KofMonth { get; set; }
        
        /// <summary>
        /// Строка коф. за период
        /// </summary>
        public string KofPeriodString { get => GetKofPeriod(); }

        /// <summary>
        /// Строка коф. за месяц
        /// </summary>
        public string KofMonthString { get => GetKofMonth(); }

        /// <summary>
        /// Возвращает коф. за период в строковом виде
        /// </summary>
        /// <returns></returns>
        private string GetKofPeriod()
        {
            if (KofPeriod < 0)
                return "0";
            else if (KofPeriod == 0 && CountPeriod == 0)
                return "-";
            else
                return KofPeriod.ToString();
        }

        /// <summary>
        /// Возвращает коф. за месяц в строковом виде
        /// </summary>
        /// <returns></returns>
        private string GetKofMonth()
        {
            if (KofMonth < 0)
                return "0";
            else if (KofMonth == 0 && CountMonth == 0)
                return "-";
            else
                return KofMonth.ToString();
        }
    }
}
