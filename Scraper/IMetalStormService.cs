namespace Scraper;

public interface IMetalStormService
{
    Task<string> GetNewReleasesPageHtml(CancellationToken cancellationToken = default);
}