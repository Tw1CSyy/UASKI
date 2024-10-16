using System;
using System.Windows.Forms;

namespace UASKI.Models.Elements
{
    public class MonthElement : BaseElement
    {
        public MonthCalendar MonthCalendar { get; set; }
        public DateTime DateFrom { get => MonthCalendar.SelectionRange.Start.Date; }
        public DateTime DateTo { get => MonthCalendar.SelectionRange.End.Date; }

        public MonthElement(MonthCalendar calendar , Label error)
        {
            MonthCalendar = calendar;
            ErrorLabel = error;
        }
    }
}
