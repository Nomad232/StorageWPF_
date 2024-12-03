using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace StorageWPF.Models
{
    public static class JsonUtils
    {
        public static void ToJsonFile<T>(T data, Type type)
        {
            string path;
            if (type == typeof(User))
            {
                path = "../../../Data/Users.json";
            }
            else if (type == typeof(Product))
            {
                path = "../../../Data/Products.json";
            }
            else
            {
                throw new ArgumentException(type.ToString());
            }
            try
            {
                // Створення директорії, якщо вона не існує
                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Серіалізація об'єкта в JSON
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                // Запис JSON у файл
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису у файл: {ex.Message}");
            }
        }

        public static T? FromJsonFile<T>(Type type)
        {
            string path;
            string currentDirectory = Directory.GetCurrentDirectory();
            if (type == typeof(User))
            {
                path = "../../../Data/Users.json";
            }
            else if (type == typeof(Product))
            {
                path = "../../../Data/Products.json";
            }
            else
            {
                throw new ArgumentException(type.ToString());
            }
            try
            {
                if (!File.Exists(path))
                {
                    return default;
                }

                // Читання JSON із файлу
                string json = File.ReadAllText(path);

                // Десеріалізація JSON у об'єкт
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання з файлу: {ex.Message}");
                return default;
            }
        }
    }
}
