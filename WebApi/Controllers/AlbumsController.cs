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
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default) => Ok(await _metalContext.Albums.Select(x => new AlbumDto{ Id = x.Id, Title = x.Title }).ToListAsync(cancellationToken));
}