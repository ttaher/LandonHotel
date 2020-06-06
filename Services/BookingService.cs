using LandonHotel.Data;
using LandonHotel.Repositories;

namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingsRepository _bookingRepo;
        private readonly IRoomsRepository _roomsRepo;

        public BookingService(IBookingsRepository bookingRepo, IRoomsRepository roomsRepo)
        {
            _bookingRepo = bookingRepo;
            _roomsRepo = roomsRepo;
        }

        public bool IsBookingValid(int roomId, Booking booking)
        {

            var guestIsSmoking = booking.IsSmoking;
            var guestIsBringingPets = booking.HasPets;
            var numberOfGuests = booking.NumberOfGuests;

            if (guestIsSmoking)
            {
                return false;
            }
            var room = _roomsRepo.GetRoom(roomId);
            if (guestIsBringingPets && !room.ArePetsAllowed)
                return false;
            if (numberOfGuests > room.Capacity)
                return false;
            return true;
        }
    }
}
