namespace SteamGameStatistics.Cache
{
    public interface ICacheService
    {
        object TryGet(string key);
        void Create(string key, object data);
        void Delete(string key);
    }
}
