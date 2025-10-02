using BookingApp.Dal;
using BookingApp.domain.Abstractions.Repositories;
using BookingApp.domain.Abstractions.Services;
using BookingApp.domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IHotelsRepository _hotelsRepository;
    private readonly DataContext _ctx;

    public ReservationService(IHotelsRepository hotelsRepository, DataContext ctx)
    {
        _hotelsRepository = hotelsRepository;
        _ctx = ctx;
    }
    
    public async Task<Reservation> MakeReservationAsync(Reservation reservation)
    {
        var hotel = await _hotelsRepository.GetHotelByIdAsync(reservation.HotelId);

        var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

        if (room == null || hotel == null)
        {
            return null;
        }

        bool isBusy = await _ctx.Reservations.AnyAsync(r => 
            (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckOutDate) && 
            (reservation.CheckOutDate >= r.CheckInDate && reservation.CheckOutDate <= r.CheckOutDate));

        if (isBusy || room.NeedsRepair)
        {
            return null;
        }
        
        _ctx.Rooms.Update(room);
        _ctx.Reservations.Add(reservation);
        
        await _ctx.SaveChangesAsync();
        
        return reservation;
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _ctx.Reservations.Include(r => r.Hotel).Include(r => r.Room).ToListAsync();
    }

    public async Task<Reservation> GetReservationByIdAsync(int id)
    {
        return await _ctx.Reservations.Include(r => r.Hotel).Include(r => r.Room).FirstOrDefaultAsync(r => r.ReservationId == id);
    }

    public async Task<Reservation> DeleteReservationAsync(int id)
    {
        var reservation = await _ctx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
        if (reservation != null)
        {
            _ctx.Reservations.Remove(reservation);
        }
        await _ctx.SaveChangesAsync();
        return reservation;
        
    }
}