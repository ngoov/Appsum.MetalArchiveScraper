using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

using Flurl;

namespace Scraper;

public class Scraper
{
    private readonly IMetalStormService _metalStormService;

    public Scraper(IMetalStormService metalStormService)
    {
        _metalStormService = metalStormService;
    }

    public async Task Run()
    {
        string html = await _metalStormService.GetNewReleasesPageHtml();
        var parser = new HtmlParser();
        IHtmlDocument? document = await parser.ParseDocumentAsync(html);
        
        IHtmlCollection<IElement>? elements = document.QuerySelectorAll(".album-title-row > .album-title .megatitle");
        List<Album> albums = new();
        foreach (IElement el in elements)
        {
            var splitEl = parser.ParseFragment(el.InnerHtml, el).Where(x => x is IHtmlAnchorElement).Cast<IHtmlAnchorElement>().ToList();
            if (splitEl.Count != 2)
            {
                throw new HtmlParsingException($"Could not parse Album HTML element and split in 3 parts: {el.InnerHtml}");
            }

            IHtmlAnchorElement bandEl = splitEl[0];
            var bandUrl = new Url(Url.Combine(MetalStormService.BaseUrl, bandEl.PathName, bandEl.Search));
            int bandId = int.Parse(bandUrl.QueryParams.First(x => x.Name == "band_id").Value.ToString() ?? throw new InvalidOperationException());
            string bandName = bandEl.Text;
            var band = new Band(new MetalStormId(bandId), bandName, bandUrl);

            IHtmlAnchorElement albumEl = splitEl[1];
            var albumUrl = new Url(Url.Combine(MetalStormService.BaseUrl, albumEl.PathName, albumEl.Search));
            int albumId = int.Parse(albumUrl.QueryParams.First(x => x.Name == "album_id").Value.ToString() ?? throw new InvalidOperationException());
            string albumTitle = albumEl.Text;

            var bandHtml = await _metalStormService.GetBandPageHtml(band.MetalStormId);

            IHtmlDocument bandDocument = await parser.ParseDocumentAsync(bandHtml);
            IHtmlCollection<IElement> genreEls = bandDocument.QuerySelectorAll("#page-content table table table tr:last-child");

            albums.Add(new Album(new MetalStormId(albumId), new AlbumTitle(albumTitle), band, albumUrl));

        }
        foreach (Album album in albums)
        {
            Console.WriteLine($"{album.Band.Name} - {album.Title}");
        }
        Console.WriteLine("Done found");
    }
    /*
     * Use AngleSharp (https://anglesharp.github.io/)
     * Scrape the bottom part list https://metalstorm.net/events/new_releases.php#142331
     */
}

public class HtmlParsingException : Exception
{
    public HtmlParsingException(string message) : base(message) { }
}