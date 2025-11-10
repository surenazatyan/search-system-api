
namespace SearchSystemDomain.Entities;

public sealed class ServiceInfo
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public PositionInfo Position { get; set; } = new PositionInfo();

    public ServiceInfo() { }

    public ServiceInfo(int id, string name, PositionInfo position)
    {
        Id = id;
        Name = name;
        Position = position;
    }
}