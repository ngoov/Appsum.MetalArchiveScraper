using Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly MetalContext _metalContext;
    private readonly ILogger<GenresController> _logger;

    public GenresController(MetalContext metalContext, ILogger<GenresController> logger)
    {
        _metalContext = metalContext;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default) 
        => Ok(await _metalContext.Genres.Include(x => x.BandGenres).Select(genre => 
            new
            {
                Id = genre.Id,
                Name = genre.Name,
                BandCount = genre.BandGenres.Count
            }
        ).ToListAsync(cancellationToken));
    
    [HttpGet("{id:guid}/bands")]
    public async Task<IActionResult> GetBands(Guid id, CancellationToken cancellationToken = default) 
        => Ok(await _metalContext.BandGenres
                                 .Include(x => x.Band)
                                 .AsNoTracking()
                                 .Where(x => x.Genre.Id == id)
                                 .Select(genre =>
                                                                               new BandDto
                                                                               {
                                                                                   Id = genre.Band.Id,
                                                                                   Name = genre.Band.Name,
                                                                               }
              ).ToListAsync(cancellationToken));
}