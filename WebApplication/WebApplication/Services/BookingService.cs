using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.DataAccess;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class BookingService : IBookingService
    {
        private ResourceBookingDbContext _context;

        public BookingService(ResourceBookingDbContext resourceBookingDbContext)
        {
            this._context = resourceBookingDbContext;
        }

        public async Task<Booking> BookResource(Booking booking)
        {
            try
            {
                if (booking.DateTo.CompareTo(booking.DateFrom) <= 0)
                {
                    throw new Exception("err:DateTo should be after DateFrom");
                }

                if (booking.BookedQuantity <= 0)
                {
                    throw new Exception("err:Amount is less than 1");
                }
                var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == booking.ResourceId);
                if (resource == null)
                {
                    throw new Exception("err:Resource not found");
                }
                var overLapBookings = _context.Bookings.Where(b =>
                    b.DateFrom.CompareTo(booking.DateTo) <= 0 && b.DateTo.CompareTo(booking.DateFrom) >= 0 &&
                    b.ResourceId == booking.ResourceId).ToList();
                var edgesOfPeriods = new List<TriplePair>();
                Console.WriteLine(overLapBookings.Count);
                for (int i = 0; i < overLapBookings.Count; i++)
                {
                    edgesOfPeriods.Add(new TriplePair(overLapBookings[i].DateFrom, i, 1));
                    edgesOfPeriods.Add(new TriplePair(overLapBookings[i].DateTo, i, -1));
                }
                List<TriplePair> sortedPairsList = edgesOfPeriods.OrderBy(ed => ed.DateTime).ToList();
                Console.WriteLine(edgesOfPeriods.Count);
                var sum = 0;
                var maxQuantityDuringPeriod = 0;

                for (int i = 0; i < sortedPairsList.Count; i++)
                {
                    if (sortedPairsList[i].Value == 1)
                    {
                        sum += overLapBookings[sortedPairsList[i].Index].BookedQuantity;
                    }

                    if (sortedPairsList[i].Value == -1)
                    {
                        if (sum > maxQuantityDuringPeriod)
                        {
                            maxQuantityDuringPeriod = sum;
                        }

                        sum -= overLapBookings[sortedPairsList[i].Index].BookedQuantity;
                    }
                }

                if (resource.Quantity - maxQuantityDuringPeriod >= booking.BookedQuantity)
                {
                    var bookingWithProperId = _context.Bookings.Add(booking).Entity;
                    await _context.SaveChangesAsync();
                    Console.WriteLine(maxQuantityDuringPeriod);
                    return bookingWithProperId;
                }

                throw new Exception("err:this quantity is not possible for this period");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> RemoveBookingByBookingId(int bookingId)
        {
            try
            {
                var bookingToRemove = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
                if (bookingToRemove == null)
                {
                    throw new Exception("err:booking to remove not found");
                }

                _context.Bookings.Remove(bookingToRemove);
                await _context.SaveChangesAsync();
                return bookingToRemove.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}