using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KelaniSTEAM_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService) =>
        _projectService = projectService;

    // Route: GET api/project/getAll
    [HttpGet]
    [Route("getAll")]
    public async Task<List<Project>> Get() =>
        await _projectService.GetAsync();

    // Route: GET api/project/getProjectById/{id}
    [HttpGet]
    [Route("getProjectById/{id:length(24)}")]
    public async Task<ActionResult<Project>> Get(string id)
    {
        var project = await _projectService.GetAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        return project;
    }

    // Route: POST api/project/create
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Post(Project newProject)
    {
        await _projectService.CreateAsync(newProject);

        return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
    }

    // Route: DELETE api/project/deleteProjectById/{id}
    [HttpDelete]
    [Route("deleteProjectById/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var project = await _projectService.GetAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        await _projectService.RemoveAsync(id);

        return NoContent();
    }
}