using Microsoft.AspNetCore.Mvc;
using SearchSystemApi.RequestResponseDTOs;
using SearchSystemApi.Utils;
using SearchSystemApplication.MappingExtensions;
using SearchSystemApplication.Services;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceInfoService _serviceInfoService;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(IServiceInfoService serviceInfoService, ILogger<ServicesController> logger)
    {
        this._serviceInfoService = serviceInfoService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all registered services and their metadata.
    /// </summary>
    /// <remarks>
    /// Returns a list of service objects containing identifying and descriptive metadata.
    /// Sample response:
    /// [
    ///   {
    ///     "id": "service-1",
    ///     "name": "Service One",
    ///     "description": "Performs task X",
    ///     "status": "Healthy"
    ///   },
    ///   {
    ///     "id": "service-2",
    ///     "name": "Service Two",
    ///     "description": "Performs task Y",
    ///     "status": "Degraded"
    ///   }
    /// ]
    /// </remarks>
    /// <returns>200 with a JSON array of service DTOs; 500 on server error.</returns>
    [HttpGet("List")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetAllServicesResponse), 200)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "List available services",
        Description = "Retrieves all registered services with metadata (id, name, description, status). Useful for UI lists and diagnostics.",
        OperationId = "GetAllServices")]
    public async Task<IActionResult> GetAllServices()
    {
        _logger.LogInformation($"{nameof(ServicesController.GetAllServices)} Fetching all services.");
        var allServices = await _serviceInfoService.GetServiceInfosAsync();
        var response = new GetAllServicesResponse
        {
            Services = allServices?.Count > 0 ? allServices : null
        };
        _logger.LogInformation($"{nameof(ServicesController.GetAllServices)} Fetched {response.Services?.Count ?? 0} services.");
        return Ok(response.Services);
    }

    /// <summary>
    /// Searches for services by name and location.
    /// </summary>
    /// <remarks>
    /// Accepts a search request with a service name and position, and returns a ranked list of matching services.
    /// Each result includes the service's id, name, position, similarity score, and distance from the requested position.
    /// Sample request:
    /// {
    ///   "serviceName": "Service One",
    ///   "position": {
    ///     "latitude": 40.7128,
    ///     "longitude": -74.0060
    ///   }
    /// }
    /// Sample response:
    /// {
    ///   "totalHits": 1,
    ///   "totalDocuments": 2,
    ///   "results": [
    ///     {
    ///       "id": "service-1",
    ///       "name": "Service One",
    ///       "position": {
    ///         "latitude": 40.7128,
    ///         "longitude": -74.0060
    ///       },
    ///       "score": 0.95,
    ///       "distance": 0.0
    ///     }
    ///   ]
    /// }
    /// </remarks>
    /// <param name="request">The search criteria including service name and position.</param>
    /// <returns>200 with a JSON object containing search results; 400 for invalid input; 500 on server error.</returns>
    [HttpPost("Search")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SearchResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [SwaggerOperation(
        Summary = "Search for services",
        Description = "Searches for services by name and location, returning a ranked list of matches with similarity scores and distances.",
        OperationId = "SearchServices")]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        var allServices = await _serviceInfoService.GetServiceInfosAsync();
        var results = allServices?
            .Select(service =>
            {
                var responsePosition = service.Position.ToResponseDto();
                var searchResult = new SearchResult
                {
                    Id = service.Id,
                    Name = service.Name,
                    Position = responsePosition,
                    Score = Utils.CalculateScore(request.ServiceName, service.Name),
                    Distance = Utils.CalculateDistance(request.Position, responsePosition)
                };
                return searchResult;
            })
            .Where(r => r.Score > 0)
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.Distance)
            .ToList();

        var response = new SearchResponse
        {
            TotalHits = results.Count,
            TotalDocuments = allServices.Count,
            Results = results
        };

        return Ok(response);
    }

}
