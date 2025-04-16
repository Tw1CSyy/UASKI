using System;

namespace UASKI.StaticModels
{
    public class AppSettings
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string DateBase { get; set; }
        public int CountAdd { get; set; }
        public int CountClose { get; set; }
        public int CountPrint { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
