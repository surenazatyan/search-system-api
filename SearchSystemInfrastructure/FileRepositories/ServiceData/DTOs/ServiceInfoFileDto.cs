using System.Text.Json.Serialization;

namespace SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

public sealed class ServiceInfoFileDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("position")]
    public PositionInfoFileDto Position { get; init; } = new PositionInfoFileDto();

    public ServiceInfoFileDto() { }

    public ServiceInfoFileDto(int id, string name, PositionInfoFileDto position)
    {
        Id = id;
        Name = name;
        Position = position;
    }
}