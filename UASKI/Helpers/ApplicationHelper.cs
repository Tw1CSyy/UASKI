using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UASKI.Core.Models;
using UASKI.StaticModels;

namespace UASKI.Helpers
{
    /// <summary>
    /// Хелпер для работы приложения
    /// </summary>
    public static class ApplicationHelper
    {
        private static readonly string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UASKI_Settings.json");
        private static readonly string DumpUtilePath = @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe";
        private static readonly string DumpSavePath = @"C:\Program Files\PostgreSQL\16\bin\BAK";

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
                return true;
            }
            catch (Exception)
            {
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
        /// Создает резервную копию базы, если сегодня этого еще не делалось
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public static bool Dump()
        {
            var name = $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}_dump";

            if (File.Exists(Path.Combine(DumpSavePath, name)))
                return false;

            try
            {
                return CreateDump(Path.Combine(DumpSavePath, name));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Выполняет процесс создания резервной копии базы данных
        /// </summary>
        /// <param name="nameDump"></param>
        /// <returns>true - успешная операция</returns>
        private static bool CreateDump(string nameDump)
        {
            var settings = SystemData.Settings;

            var process = new ProcessStartInfo()
            {
                FileName = DumpUtilePath,
                Arguments = $"-U {settings.User} -F c -b -v -p {settings.Port} -f \"{nameDump}\" \"{settings.DateBase}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Environment.SetEnvironmentVariable("PGPASSWORD", settings.Password);

            using (Process proces = new Process())
            {
                proces.StartInfo = process;

                try
                {
                    proces.Start();

                    string output = proces.StandardOutput.ReadToEnd();
                    string error = proces.StandardError.ReadToEnd();

                    proces.WaitForExit();
                    Environment.SetEnvironmentVariable("PGPASSWORD", null);

                    if (proces.ExitCode == 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    Environment.SetEnvironmentVariable("PGPASSWORD", null);
                    return false;
                }
            }
        }

        /// <summary>
        /// Удаляет прошедшие празднечные дни, если они есть
        /// </summary>
        /// <returns>Список удаленных дат</returns>
        public static DateTime[] DeleteHoliday()
        {
            var dateList = HolidayModel.GetList().Where(c => c.Date < DateTime.Today);
           
            foreach (var item in dateList)
            {
                item.Delete();
            }

            return dateList.Select(c => c.Date).ToArray();
        }

        /// <summary>
        /// Добавляет празднечные дни на будущие выходные
        /// </summary>
        /// <returns>Список добавленных дат</returns>
        public static DateTime[] AddHoliday()
        {
            var holidayList = HolidayModel.GetList();
            var result = new List<DateTime>();

            for (DateTime date = DateTime.Today.AddDays(4); date < DateTime.Today.AddDays(25);)
            {
                if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    date = date.AddDays(1);
                    continue;
                }

                var holy = holidayList.FirstOrDefault(c => c.Date == date);

                if (holy != null)
                {
                    date = date.AddDays(1);
                    continue;
                }

                result.Add(date);
                var model = new HolidayModel(date);
                model.Add();
                date = date.AddDays(1);
            }

            return result.ToArray();
        }

    }
}
