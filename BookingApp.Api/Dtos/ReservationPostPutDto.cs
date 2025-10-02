namespace BookingApp.Api.Dtos;

public class ReservationPostPutDto
{
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Customer { get; set; }
}