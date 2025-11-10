using SearchSystemApplication.MappingExtensions;
using SearchSystemInfrastructure.FileRepositories.ServiceData;

namespace SearchSystemInfrastructure;
public static class DbSeeder
{
    public static void Seed(SearchSystemDbContext db)
    {
        if (!db.Services.Any())
        {
            var servicesFromJson = ServicesJsonDataProvider.GetDataFromJson();

            db.Services.AddRange(servicesFromJson.Select(s => s.ToDatabaseDto()));
        }
        db.SaveChanges();
    }
}
