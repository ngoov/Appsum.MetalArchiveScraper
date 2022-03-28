using Data;

namespace Scraper;

public interface IMetalStormService
{
    Task<string> GetNewReleasesPageHtml(int page, CancellationToken cancellationToken = default);
    Task<string> GetBandPageHtml(MetalStormId metalStormId, CancellationToken cancellationToken = default);
}