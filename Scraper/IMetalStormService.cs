namespace Scraper;

public interface IMetalStormService
{
    Task<string> GetNewReleasesPageHtml(CancellationToken cancellationToken = default);
    Task<string> GetBandPageHtml(MetalStormId metalStormId, CancellationToken cancellationToken = default);
}