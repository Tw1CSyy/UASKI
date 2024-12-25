using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASKI.Models
{
    /// <summary>
    /// Модель для истории страниц
    /// </summary>
    public class HistoryModel
    {
        /// <summary>
        /// Страница
        /// </summary>
        public BasePage Page { get; private set; }

        /// <summary>
        /// Является ли данная активной
        /// </summary>
        public bool IsSelect { get; set; }

        /// <summary>
        /// Создает модель класса
        /// </summary>
        /// <param name="page">Страница</param>
        public HistoryModel(BasePage page)
        {
            Page = page;
        }
    }
}
