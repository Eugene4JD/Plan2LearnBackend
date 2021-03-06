using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            this._bookingService = bookingService;
        }

        [HttpPost]
        [Route("/api/booking")]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {
            try
            {
                return Ok(await _bookingService.BookResource(booking));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("/api/booking")]
        public async Task<ActionResult<int>> DeleteBooking([FromQuery] int bookingId)
        {
            try
            {
                return Ok(await _bookingService.RemoveBookingByBookingId(bookingId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}