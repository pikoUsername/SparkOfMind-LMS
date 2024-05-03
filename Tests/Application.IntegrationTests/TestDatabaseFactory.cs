
namespace Tests.Application.IntegrationTests
{
    public static class TestDatabaseFactory
    {
        public static async Task<ITestDatabase> CreateAsync()
        {
// I DONT KNOW HOW TO MAKE AND SET VALUES OF DIRECTIVES VARIABLES... 
//#if (UseSQLite)
        var database = new SqliteTestDatabase();
//#else
//#if DEBUG
//            var database = new PostgresqlTestDatabase();
//#endif
            await database.InitialiseAsync();

            return database;
        }
    }
}
