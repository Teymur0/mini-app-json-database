using mini_app_json_database.Models;
using Newtonsoft.Json;

namespace mini_app_json_database.Database.Services
{
    public class CustomDataBaseService
    {
        private const string path = "C:\\Users\\tnabi\\OneDrive\\Рабочий стол\\C# homework\\mini-app-json-database\\mini-app-json-database\\Database\\";

        public static void SaveFiles(Classroom classroom)
        {
            using (StreamWriter sw = new StreamWriter(path + @"database.json",true))
            {
                var json = JsonConvert.SerializeObject(classroom, Formatting.Indented);
                sw.WriteLine(json);
            }

        }
        public static List<Classroom> LoadFiles()
        {
            if (!File.Exists(path))
            {
                return new List<Classroom>();
            }

            using (StreamReader sr = new StreamReader(path))
            {
                var json = sr.ReadToEnd();
                var classrooms = JsonConvert.DeserializeObject<List<Classroom>>(json);

                if (classrooms == null)
                {
                    return new List<Classroom>();
                }

                return classrooms;
            }
        }
    }
}


