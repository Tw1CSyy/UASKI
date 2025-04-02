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
        private static readonly string DumpUtilePath = @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe";
        private static readonly string DumpSavePath = @"Z:\otdel\OUKS\UASKI_SOFT_BAK";
        private const int DAYS_BACKUP = 5;

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
        /// Создает резервную копию базы, если сегодня этого еще не делалось
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public static bool Dump()
        {
            var name = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_dump";

            if(!Directory.Exists(DumpSavePath))
            {
                Ai.AddMessage(TypeNotice.Error, "Папки для сохранения копии данных не существует");
                return false;
            }

            try
            {
                return CreateDump(Path.Combine(DumpSavePath, name));
            }
            catch (Exception)
            {
                Ai.AddMessage(TypeNotice.Error, "Системная ошибка при создании копии данных");
                return false;
            }
        }

        /// <summary>
        /// Удаляет старые дампы
        /// </summary>
        /// <returns>true - успешная операция</returns>
        public static bool RemoveDump()
        {
            if (!Directory.Exists(DumpSavePath))
            {
                Ai.AddWaitMessage(TypeNotice.Error, "Папки для сохранения копии данных не существует");
                return false;
            }

            try
            {
                var files = Directory.GetFiles(DumpSavePath);

                if (files.Count() == 0)
                    return false;

                int count = 0;

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);

                    if (fileInfo.CreationTime.Date <= DateTime.Today.AddDays(DAYS_BACKUP * -1))
                    {
                        count++;
                        fileInfo.Delete();
                    }
                }

                if(count > 0)
                    Ai.AddWaitMessage(TypeNotice.Default, "Удалены старые копии данных: " + count.ToString());

                return true;
            }
            catch (Exception)
            {
                Ai.AddWaitMessage(TypeNotice.Error, "Системная ошибка при удалении старых копий данных");
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

            if(!File.Exists(DumpUtilePath))
            {
                Ai.AddMessage(TypeNotice.Error, "Утилита для создании копии данных не найдена");
                return false;
            }

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
                    {
                        return true;
                    } 
                    else
                    {
                        Ai.AddMessage(TypeNotice.Error, "Ошибка при создании копии данных");
                        return false;
                    }
                        
                }
                catch (Exception)
                {
                    Ai.AddMessage(TypeNotice.Error, "Системная ошибка при создании копии данных");
                    Environment.SetEnvironmentVariable("PGPASSWORD", null);
                    return false;
                }
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