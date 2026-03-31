using _4_4_HttpClient.Entitis;
using System.Text.Json;

namespace _4_4_HttpClient.Reposditories;

public class Repository<T> : IRepository<T>
{
    private readonly string FilePath;

    public Repository(string fileName = "")
    {
        var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        fileName = fileName == string.Empty ? GetFileName() : fileName;
        fileName = $"{fileName}.json";


        FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
        if (!File.Exists(FilePath))
        {
            var stream = File.Create(FilePath);
            stream.Close();
        }
    }

    private string GetFileName()
    {
        if (typeof(T) is Car)
        {
            return "Cars";
        }
        //if (typeof(T) is FeedBack)
        //{
        //    return "FeedBacks";
        //}

        return "";
    }

    public async Task<List<T>> GetAllAsync()
    {
        var json = await File.ReadAllTextAsync(FilePath);
        if (string.IsNullOrEmpty(json))
        {
            return new List<T>();
        }

        var items = JsonSerializer.Deserialize<List<T>>(json);
        return items;
    }

    public async Task SaveAllAsync(List<T> items)
    {
        var json = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(FilePath, json);
    }
}
