using _4_6_TelegramBot.DTOs;
using _4_6_TelegramBot.Entiti;
using _4_6_TelegramBot.Reposditories;

namespace _4_6_TelegramBot.Services;

public class UserService
{
    private readonly Repository<User> _repository;

    public UserService()
    {
        _repository = new Repository<User>();
    }

    public async Task<User> ProcessUserAsync(long chatId, string username, RegistrDto registrDto)
    {
        // 1. Fayldagi barcha foydalanuvchilarni o'qiymiz
        var users = await _repository.GetAllAsync();

        // 2. ChatId orqali tekshiramiz
        var existingUser = users.FirstOrDefault(u => u.ChatId == chatId);

        if (existingUser != null)
        {
            // Foydalanuvchi bor bo'lsa, countni oshiramiz
            existingUser.Count++;
            await _repository.SaveAllAsync(users);
            return existingUser;
        }
        else
        {
            // Foydalanuvchi yo'q bo'lsa, yangi entity yaratamiz (Registratsiya)
            var newUser = new User
            {
                ChatId = chatId,
                Nik_User = username,
                Ismi = registrDto.Ism,
                Familiyasi = registrDto.Familiyasi,
                TelefonRaqam = registrDto.TelefonRaqam,
                RegistirTime = DateTime.Now,
                Count = 1
            };

            users.Add(newUser);
            await _repository.SaveAllAsync(users); // Faylga saqlash
            return newUser;
        }
    }
}