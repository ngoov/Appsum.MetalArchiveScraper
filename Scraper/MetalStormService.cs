using System.Text.Json;
using System.Text.Json.Serialization;

using Data;

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

    public async Task PostStudioFilterOnReleasesPageHtml(CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("filter_media_types[1]","1"),
            new KeyValuePair<string, string>("filter_media_types_submit","Filter"),
        } ;
        await _httpClient.PostAsync("events/new_releases.php", new FormUrlEncodedContent(data), cancellationToken);
    }

    public async Task<string> GetNewReleasesPageHtml(int page, CancellationToken cancellationToken = default)
    {
        // Post with form data filter_media_types%5B1%5D=1&filter_media_types_submit=Filter
        HttpResponseMessage response = await _httpClient.GetAsync($"events/new_releases.php?page={page}", cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseHtml = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseHtml;
    }

    public async Task<string> GetBandPageHtml(MetalStormId metalStormId, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"bands/band.php?band_id={metalStormId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseHtml = await response.Content.ReadAsStringAsync(cancellationToken);
        return responseHtml;
    }
}