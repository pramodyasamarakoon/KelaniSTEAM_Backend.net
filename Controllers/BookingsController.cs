using KelaniSTEAM_Backend.Models;
using KelaniSTEAM_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KelaniSTEAM_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService) =>
        _bookingService = bookingService;

    // Route: GET api/booking/getAll
    [HttpGet]
    [Route("getAll")]
    public async Task<List<Booking>> Get() =>
        await _bookingService.GetAsync();

    // Route: GET api/booking/getBookingById/{id}
    [HttpGet]
    [Route("getBookingById/{id:length(24)}")]
    public async Task<ActionResult<Booking>> Get(string id)
    {
        var booking = await _bookingService.GetAsync(id);

        if (booking is null)
        {
            return NotFound();
        }

        return booking;
    }

    // Route: POST api/booking/create
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Post(Booking newBooking)
    {
        await _bookingService.CreateAsync(newBooking);

        return CreatedAtAction(nameof(Get), new { id = newBooking.Id }, newBooking);
    }

    // Route: PUT api/booking/updateBookingById/{id}
    [HttpPut]
    [Route("updateBookingById/{id:length(24)}")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] bool status)
    {
        var booking = await _bookingService.GetAsync(id);
        if (booking is null)
        {
            return NotFound();
        }

        await _bookingService.UpdateStatusAsync(id, status);

        return NoContent();
    }

    // Route: DELETE api/booking/deleteBookingById/{id}
    [HttpDelete]
    [Route("deleteBookingById/{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var booking = await _bookingService.GetAsync(id);

        if (booking is null)
        {
            return NotFound();
        }

        await _bookingService.RemoveAsync(id);

        return NoContent();
    }
}
