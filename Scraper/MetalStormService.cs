namespace Scraper;

public sealed class MetalStormService : IMetalStormService
{
    public const string BaseUrl = "https://metalstorm.net/";
    private readonly HttpClient _httpClient;

    public MetalStormService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<string> GetNewReleasesPageHtml(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.GetAsync("events/new_releases.php", cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseHtml = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseHtml;
    }
}