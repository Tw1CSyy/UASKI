using System.Drawing;
using UASKI.StaticModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для обработки уведомлений
    /// </summary>
    public static class ErrorHelper
    {
        /// <summary>
        /// Изменяет статус на ошибку
        /// </summary>
        public static void StatusError()
        {
            var form = SystemData.Form;
            Close();

            form.LabelStatus.Text = "Ой... Что то не так";
            form.LabelStatus.ForeColor = Color.Red;
            form.LabelStatus.Visible = true;
            SystemData.IsQuery = false;
        }

        /// <summary>
        /// Изменяет статус на успех
        /// </summary>
        public static void StatusComlite()
        {
            var form = SystemData.Form;
            Close();
          
            form.LabelStatus.Text = "Все сделано!";
            form.LabelStatus.ForeColor = Color.Green;
            form.LabelStatus.Visible = true;
            SystemData.IsQuery = false;
        }

        /// <summary>
        /// Изменяет статус на подтверждение
        /// </summary>
        public static void StatusQuery()
        {
            var form = SystemData.Form;
            Close();

            form.LabelStatus.Text = "И еще раз для подтверждения";
            form.LabelStatus.ForeColor = Color.OrangeRed;
            form.LabelStatus.Visible = true;
            SystemData.IsQuery = true;
        }

        /// <summary>
        /// Закрывает и скрывает статус
        /// </summary>
        private static void Close()
        {
            var form = SystemData.Form;

            form.LabelStatus.Visible = false;
            form.timerTick = 0;
        }

        /// <summary>
        /// Статус, что программа подключилась к базе
        /// </summary>
        public static void StatusConnection()
        {
            var form = SystemData.Form;
            SystemData.IsQuery = false;
            Close();

            form.LabelStatus.Text = "Подключение установлено. Поработаем";
            form.LabelStatus.ForeColor = Color.Green;
            form.LabelStatus.Visible = true;
        }
    }
}
