namespace _4_4_HttpClient.Reposditories;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    public Task SaveAllAsync(List<T> items);
}