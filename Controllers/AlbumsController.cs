using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly AlbumsService _albumsService;

    public AlbumsController(AlbumsService albumsService) =>
        _albumsService = albumsService;

    [HttpGet]
    public async Task<List<Album>> Get() =>
        await _albumsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Album>> Get(string id)
    {
        var album = await _albumsService.GetAsync(id);

        if (album is null)
        {
            return NotFound();
        }

        return album;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Album newAlbum)
    {
        await _albumsService.CreateAsync(newAlbum);

        return CreatedAtAction(nameof(Get), new { id = newAlbum.Id }, newAlbum);
    }

    [HttpPut("{id:length(24)}")]
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

    [HttpDelete("{id:length(24)}")]
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
