using _4_6_TelegramBot.Entiti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace _4_6_TelegramBot.Reposditories;

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
        if (typeof(T) is UserProfile)
        {
            return "UsersProfile";
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

