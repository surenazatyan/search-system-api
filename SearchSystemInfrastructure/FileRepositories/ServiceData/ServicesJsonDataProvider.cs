using Newtonsoft.Json;
using SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

namespace SearchSystemInfrastructure.FileRepositories.ServiceData;

public static class ServicesJsonDataProvider
{
    public static List<ServiceInfoFileDto> GetDataFromJson()
    {
        string path = "services-data.json";
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "FileRepositories", "ServiceData", path);
        using var streamReader = new StreamReader(jsonPath);
        var json = streamReader.ReadToEnd();
        var deserializedObject = JsonConvert.DeserializeObject<List<ServiceInfoFileDto>>(json);
        return deserializedObject;
    }
}