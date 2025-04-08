using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UASKI.Core.Models;
using UASKI.StaticModels;
using UASKI.Enums;
using System.Windows.Forms;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для работы приложения
    /// </summary>
    public static class ApplicationHelper
    {
        private static readonly string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UASKI_Settings.json");
      
        /// <summary>
        /// Обработка настроек при запуске приложения
        /// </summary>
        public static bool Settings()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                    CreateSettings(SettingsPath);

                LoadSettings(SettingsPath);
                Ai.AddWaitMessage(TypeNotice.Default, "Настройки загружены");
                return true;
            }
            catch (Exception)
            {
                Ai.AddWaitMessage(TypeNotice.Error, "Системная ошибка при создании настроек");
                return false;
            }
        }

        /// <summary>
        /// Создает файл настроек
        /// </summary>
        /// <param name="filePath">Путь до каталога</param>
        private static void CreateSettings(string filePath)
        {
            var defult = new AppSettings
            {
                User = "user",
                Password = "password",
                Host = "localhost",
                Port = "5432",
                DateBase = "UASKI"
            };

            var json = JsonConvert.SerializeObject(defult, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Загружает файл настроек 
        /// </summary>
        /// <param name="filePath">Путь до каталога</param>
        private static void LoadSettings(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                SystemData.Settings = JsonConvert.DeserializeObject<AppSettings>(json);
            }
        }

        /// <summary>
        /// Добавляет празднечные дни на будущие выходные
        /// </summary>
        public static void AddHoliday()
        {
            var holidayList = HolidayModel.GetList();
            var result = 0;
            int countNullDate = 0;

            for (DateTime date = DateTime.Today.AddYears(3); date > DateTime.Today;)
            {
                if (countNullDate == 30)
                    break;

                if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    date = date.AddDays(-1);
                    continue;
                }

                var holy = holidayList.FirstOrDefault(c => c.Date == date);

                if (holy != null)
                {
                    date = date.AddDays(-1);
                    countNullDate++;
                    continue;
                }

                result++;
                var model = new HolidayModel(date);
                model.Add();
                date = date.AddDays(-1);
            }

            if (result > 0)
                Ai.AddWaitMessage(Enums.TypeNotice.Default, "Добавлены новые даты: " + result.ToString());

        }

    }
}