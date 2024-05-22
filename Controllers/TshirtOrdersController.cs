using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KelaniSTEAM_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TshirtOrderController : ControllerBase
{
    private readonly TshirtOrderService _tshirtOrderService;

    public TshirtOrderController(TshirtOrderService tshirtOrderService) =>
        _tshirtOrderService = tshirtOrderService;

    // Route: GET api/tshirtOrder/getAll
    [HttpGet]
    [Route("getAll")]
    public async Task<List<TshirtOrder>> Get() =>
        await _tshirtOrderService.GetAsync();

    // Route: GET api/tshirtOrder/getTshirtOrderById/{id}
    [HttpGet]
    [Route("getTshirtOrderById/{id:length(24)}")]
    public async Task<ActionResult<TshirtOrder>> Get(string id)
    {
        var tshirtOrder = await _tshirtOrderService.GetAsync(id);

        if (tshirtOrder is null)
        {
            return NotFound();
        }

        return tshirtOrder;
    }

    // Route: POST api/tshirtOrder/create
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Post(TshirtOrder newTshirtOrder)
    {
        await _tshirtOrderService.CreateAsync(newTshirtOrder);

        return CreatedAtAction(nameof(Get), new { id = newTshirtOrder.Id }, newTshirtOrder);
    }

    // Route: PUT api/tshirtOrder/updateTshirtOrderById/{id}
    [HttpPut]
    [Route("updateTshirtOrderById/{id:length(24)}")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] bool status)
    {
        var tshirtOrder = await _tshirtOrderService.GetAsync(id);
        if (tshirtOrder is null)
        {
            return NotFound();
        }

        await _tshirtOrderService.UpdateStatusAsync(id, status);

        return NoContent();
    }

    // Route: DELETE api/booking/deleteTshirtOrderById/{id}
    [HttpDelete]
    [Route("deleteTshirtOrderById/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var tshirtOrder = await _tshirtOrderService.GetAsync(id);

        if (tshirtOrder is null)
        {
            return NotFound();
        }

        await _tshirtOrderService.RemoveAsync(id);

        return NoContent();
    }
}
