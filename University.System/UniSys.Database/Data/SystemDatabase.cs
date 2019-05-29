namespace UniSys.Database.Data
{
    using Services;
    using Services.Interfaces;

    public static class SystemDatabase
    {
        public static IDbService GetDb()
        {
            return Context = new DbService();
        }

        public static IDbService Context { get; set; }
    }
}
