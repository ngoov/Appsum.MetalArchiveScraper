using System.Text.RegularExpressions;

using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

using Data;

using Flurl;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using MoreLinq.Extensions;

using NodaTime;

namespace Scraper;

public class Scraper
{
    private const int MAX_PAGES = 10;
    private readonly IMetalStormService _metalStormService;
    private readonly MetalContext _metalContext;

    public Scraper(IMetalStormService metalStormService, MetalContext metalContext)
    {
        _metalStormService = metalStormService;
        _metalContext = metalContext;
    }

    public async Task Run(CancellationToken cancellationToken = default)
    {
        // await using IDbContextTransaction transaction = await _metalContext.Database.BeginTransactionAsync(cancellationToken);
        var page = 1;
        while (page <= MAX_PAGES)
        {
            string html = await _metalStormService.GetNewReleasesPageHtml(page, cancellationToken);
            var parser = new HtmlParser();
            IHtmlDocument? document = await parser.ParseDocumentAsync(html);

            IHtmlCollection<IElement>? elements = document.QuerySelectorAll(".album-title-row > .album-title .megatitle");
            foreach (IElement el in elements)
            {
                List<IHtmlAnchorElement> splitEl = parser.ParseFragment(el.InnerHtml, el).Where(x => x is IHtmlAnchorElement).Cast<IHtmlAnchorElement>().ToList();
                if (splitEl.Count != 2)
                {
                    throw new HtmlParsingException($"Could not parse Album HTML element and split in 3 parts: {el.InnerHtml}");
                }

                IHtmlAnchorElement albumEl = splitEl[1];
                var albumUrl = new Url(Url.Combine(MetalStormService.BaseUrl, albumEl.PathName, albumEl.Search));
                int albumId = int.Parse(albumUrl.QueryParams.First(x => x.Name == "album_id").Value.ToString() ?? throw new InvalidOperationException());
                Album? album = await _metalContext.Albums.FirstOrDefaultAsync(x => x.MetalStormId == albumId, cancellationToken);
                if (album != null)
                {
                    // album already exists
                    continue;
                }

                IHtmlAnchorElement bandEl = splitEl[0];
                var bandUrl = new Url(Url.Combine(MetalStormService.BaseUrl, bandEl.PathName, bandEl.Search));
                int bandId = int.Parse(bandUrl.QueryParams.First(x => x.Name == "band_id").Value.ToString() ?? throw new InvalidOperationException());
                string bandName = bandEl.Text;
                Band? band = await _metalContext.Bands.Include(x => x.Albums).Include(x => x.BandGenres).ThenInclude(x => x.Genre).FirstOrDefaultAsync(x => x.MetalStormId == bandId, cancellationToken);
                if (band == null)
                {
                    band = new Band(Guid.NewGuid(), new MetalStormId(bandId), bandName, bandUrl);
                    await _metalContext.Bands.AddAsync(band, cancellationToken);
                    await _metalContext.SaveChangesAsync(cancellationToken);
                }

                string albumTitle = albumEl.Text;
                album = new Album(Guid.NewGuid(), new MetalStormId(albumId), new AlbumTitle(albumTitle), albumUrl);
                await _metalContext.Albums.AddAsync(album, cancellationToken);
                await _metalContext.SaveChangesAsync(cancellationToken);
                band.AddAlbum(album);

                string bandHtml = await _metalStormService.GetBandPageHtml(band.MetalStormId, cancellationToken);

                IHtmlDocument bandDocument = await parser.ParseDocumentAsync(bandHtml);
                IEnumerable<IElement> trEls = bandDocument.QuerySelectorAll("#page-content table table table tr").Reverse();
                await _metalContext.SaveChangesAsync(cancellationToken);
                foreach (IElement genreEl in trEls)
                {
                    List<string> genreTableData = parser.ParseFragment(genreEl.InnerHtml, genreEl).Where(x => x is IHtmlTableDataCellElement).Select(x => x.TextContent.Trim()).ToList();
                    var yearsRegex = new Regex(@"(?<from>\d{4})-(?<to>\d{4})?");
                    List<List<string>> genreBatches = genreTableData.Batch(2).Select(x => x.ToList()).ToList();
                    foreach (List<string> genreBatch in genreBatches)
                    {
                        Match genreDatesMatch = yearsRegex.Match(genreBatch[0]);
                        if (!genreDatesMatch.Success)
                        {
                            break;
                        }

                        Instant from = Instant.FromUtc(int.Parse(genreDatesMatch.Groups["from"].Value), 1, 1, 0, 0);
                        Instant? to = !string.IsNullOrWhiteSpace(genreDatesMatch.Groups["to"].Value) ? Instant.FromUtc(int.Parse(genreDatesMatch.Groups["to"].Value), 1, 1, 0, 0) : null;
                        Genre? genre = await _metalContext.Genres.FirstOrDefaultAsync(x => x.Name.ToUpper() == genreBatch[1].ToUpper(), cancellationToken);
                        if (genre == null)
                        {
                            genre = new Genre(Guid.NewGuid(), genreBatch[1]);
                            await _metalContext.Genres.AddAsync(genre, cancellationToken);
                            await _metalContext.SaveChangesAsync(cancellationToken);
                        }
                        band.AddGenre(genre, from, to);
                    }
                }
                _metalContext.Update(band);
                await _metalContext.SaveChangesAsync(cancellationToken);
                
            }
            await _metalContext.SaveChangesAsync(cancellationToken);
            page++;
        }
        // foreach (Album album in albums)
        // {
        //     Console.WriteLine($"{album.Band.Name} - {album.Title}");
        //     foreach (BandGenre bandGenre in album.Band.BandGenres)
        //     {
        //         Console.WriteLine($"  - {bandGenre.Genre} ({bandGenre.From.InUtc().Year}-{bandGenre.To?.InUtc().Year.ToString() ?? "*"})");
        //     }
        // }

        // await transaction.CommitAsync(cancellationToken);

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