using System;
using System.Collections.Generic;
using System.Text;

namespace SteamGameStatistics.Domain.Interfaces
{
    public interface IEnvironmentVariablesService
    {
        string GetSteamId();
        string SetSteamId(string steamId);
        string GetSteamKey();
        string SetSteamKey(string steamKey);
    }
}
