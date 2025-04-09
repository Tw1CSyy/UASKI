using System.Drawing;
using UASKI.Enums;

namespace UASKI.Models
{
    public class Notice
    {
        public string Message { get; private set; }
        public TypeNotice Type { get; private set; }
        public Color Color { get; private set; }

        public Notice(string mes , TypeNotice type)
        {
            Message = mes;
            Type = type;

            switch (type)
            {
                case TypeNotice.Default:
                    Color = Color.White;
                    break;
                case TypeNotice.Error:
                    Color = Color.Red;
                    break;
                case TypeNotice.Comlite:
                    Color = Color.LightGreen;
                    break;
                case TypeNotice.Warning:
                    Color = Color.Yellow;
                    break;
                default:
                    break;
            }
        }
    }
}
