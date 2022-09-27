using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Schedule.Models
{
    public class Configurator
    {
        public string? PathToListDays { get; set; }
        public int DaysCount { get; set; }  
        public ObservableCollection<int>? Years { get; set; }
        public static Configurator Load()
        {
            var file = File.ReadAllText("ScheduleConfig.json");
            var config = JsonSerializer.Deserialize<Configurator>(file)!;
            if (config.PathToListDays == "" || config.PathToListDays is null)
            {
                config.PathToListDays = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Schedule\\";
            }
            if (config.Years == null || config.Years.Count==0)
            {
                config.Years = new ObservableCollection<int>() { 2022, 2023, 2024 };
                var firstDay = new DateTime(2022, 1, 1);
                var lastDay=new DateTime(2024, 12, 31);
                var result = lastDay - firstDay;
                config.DaysCount = int.Parse(result.TotalDays.ToString());
            }
            else
            {
                var firstDay = new DateTime(config.Years[0], 1, 1);
                var lastDay = new DateTime(config.Years[^1], 12, 31);
                var result = lastDay - firstDay;
                config.DaysCount = int.Parse(result.TotalDays.ToString());
            }
            return config;
        }
    }
}
