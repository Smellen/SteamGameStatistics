namespace SteamGameStatistics.Data
{
    public static class DatabaseInitializer
    {
        public static void Seed(GameDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
