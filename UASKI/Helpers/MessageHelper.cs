using System.Windows.Forms;

namespace UASKI.Helpers
{
    public static class MessageHelper
    {
        public static void Error(string mes)
        {
            MessageBox.Show(mes, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
