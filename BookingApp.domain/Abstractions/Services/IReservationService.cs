using BookingApp.domain.Models;

namespace BookingApp.domain.Abstractions.Services;

public interface IReservationService
{
    Task<List<Reservation>> GetAllReservationsAsync();
    Task<Reservation> GetReservationByIdAsync(int id);
    Task<Reservation> MakeReservationAsync(Reservation reservation);
    Task<Reservation> DeleteReservationAsync(int id);
    
    
}