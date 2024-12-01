using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StorageWPF.Models
{
    public static class JsonUtils
    {
        public static void ToJsonFile<T>(T data, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            try
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }); // Сериализация с отступами
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(json);
                }

                Console.WriteLine($"Дані успішно збережені в файл: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при сериалізації: {ex.Message}");
            }
        }

        public static T FromJsonFile<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Вказаний шлях не може бути порожнім");
            }

            try
            {
                using (var reader = new StreamReader(path))
                {
                    var json = reader.ReadToEnd();
                    var deserializedObject = JsonSerializer.Deserialize<T>(json);
                    Console.WriteLine($"Дані успішно завантажені з файлу: {path}");
                    return deserializedObject;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл не знайдено: {path}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при десериалізації: {ex.Message}");
                throw;
            }
        }
    }
}
