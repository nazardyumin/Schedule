using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Schedule.Models
{
    public static class Serializer
    {
        public static List<Day> Load()
        {    
            string fileName = "ListDays.json";
            var path = Configurator.Load().PathToListDays + fileName;
            if (File.Exists(path))
            {
                var file = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Day>>(file)!;
            }
            else return new List<Day>();
        }
        public static void Save(IEnumerable<Day> days)
        {
            var path = Configurator.Load().PathToListDays;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path!);
            }
            string fileName = "ListDays.json";
            path += fileName;
            var file = JsonSerializer.Serialize(days, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            });
            File.WriteAllText(path, file);
        }
    }
}
