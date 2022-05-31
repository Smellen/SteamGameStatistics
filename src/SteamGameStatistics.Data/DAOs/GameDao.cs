namespace SteamGameStatistics.Data.DAOs
{
    public class GameDao
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public long AppId { get; set; }
        public string Name { get; set; }
        public long Playtime { get; set; }
        public string IconUrl { get; set; }
        public long WindowsPlaytime { get; set; }
        public long MacPlaytime { get; set; }
        public long LinuxPlaytime { get; set; }
    }
}
