namespace SearchSystemApi.RequestResponseDTOs;
public class SearchResponse
{
    public int TotalHits { get; set; }
    public int TotalDocuments { get; set; }
    public List<SearchResult> Results { get; set; }
}

public class SearchResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GeoLocation Position { get; set; }
    public double Score { get; set; }
    public double Distance { get; set; }
}
