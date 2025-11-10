using System.Text.Json.Serialization;

namespace SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

public sealed class ServiceInfoDatabaseDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("position")]
    public PositionInfoDatabaseDto Position { get; init; } = new PositionInfoDatabaseDto();

    public ServiceInfoDatabaseDto() { }

    public ServiceInfoDatabaseDto(int id, string name, PositionInfoDatabaseDto position)
    {
        Id = id;
        Name = name;
        Position = position;
    }
}