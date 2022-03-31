using Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly MetalContext _metalContext;
    private readonly ILogger<AlbumsController> _logger;

    public AlbumsController(MetalContext metalContext, ILogger<AlbumsController> logger)
    {
        _metalContext = metalContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default) 
        => Ok(await _metalContext.Bands.Include(x => x.Albums).Include(x => x.BandGenres).ThenInclude(x => x.Genre).Select(band => new BandDto
        {
            Id = band.Id,
            Albums = band.Albums.Select(album => new AlbumDto
            {
                Id = album.Id,
                Title = album.Title
            }).ToList(),
            Name = band.Name,
            Genres = band.BandGenres.Select(bandGenre => new GenreDto
            {
                Id = bandGenre.Genre.Id,
                Name = bandGenre.Genre.Name,
                FromDate = bandGenre.From,
                ToDate = bandGenre.To
            }).ToList()
        }).ToListAsync(cancellationToken));
}