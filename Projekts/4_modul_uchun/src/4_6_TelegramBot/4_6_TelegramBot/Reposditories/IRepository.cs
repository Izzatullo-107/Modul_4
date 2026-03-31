using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_6_TelegramBot.Reposditories
{
    internal interface IRepository<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task SaveAllAsync(List<T> items);
    }
}
