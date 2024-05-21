using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KelaniSTEAM_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumsService _albumsService;

    public AlbumsController(AlbumsService albumsService) =>
        _albumsService = albumsService;

    // Route: GET api/albums/getAll
    [HttpGet]
    [Route("getAll")]
    public async Task<List<Album>> Get() =>
        await _albumsService.GetRecentAsync();

    // Route: GET api/albums/getAlbumById/{id}
    [HttpGet]
    [Route("getAlbumById/{id:length(24)}")]
    public async Task<ActionResult<Album>> Get(string id)
    {
        var album = await _albumsService.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        return album;
    }

    // Route: POST api/albums/create
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Post(Album newAlbum)
    {
        await _albumsService.CreateAsync(newAlbum);

        return CreatedAtAction(nameof(Get), new { id = newAlbum.Id }, newAlbum);
    }

    // Route: PUT api/albums/updateAlbumById/{Id}
    [HttpPut]
    [Route("updateAlbumById/{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Album updatedAlbum)
    {
        var album = await _albumsService.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        updatedAlbum.Id = album.Id;

        await _albumsService.UpdateAsync(id, updatedAlbum);

        return NoContent();
    }

    [HttpDelete]
    [Route("deleteAlbumById/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var album = await _albumsService.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        await _albumsService.RemoveAsync(id);

        return NoContent();
    }
}
