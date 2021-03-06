using System;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IBookingService
    {
        Task<Booking> BookResource(Booking booking);
        Task<int> RemoveBookingByBookingId(int bookingId);
    }
}