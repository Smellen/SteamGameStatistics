namespace SteamGameStatistics.Interfaces
{
    public interface IEnvironmentVariablesService
    {
        string GetSteamId();
        bool SetSteamId(string steamId);
        string GetSteamKey();
        bool SetSteamKey(string steamKey);
    }
}
