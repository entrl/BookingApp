using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.domain.Abstractions.Services;
using BookingApp.domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IMapper _mapper;
    public ReservationsController(IReservationService reservationService,  IMapper mapper)
    {
        _reservationService = reservationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservationsAsync()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        var mappedReservations = _mapper.Map<List<ReservationGetDto>>(reservations);
        
        return Ok(mappedReservations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationByIdAsync(int reservationId)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

        if (reservation == null)
        {
            return NotFound();
        }
        
        var mappedReservation = _mapper.Map<ReservationGetDto>(reservation);
        
        return Ok(mappedReservation);
    }

    [HttpPost]
    public async Task<IActionResult> MakeReservation([FromBody] ReservationPostPutDto reservationPostPutDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationPostPutDto);
        var resoult = await _reservationService.MakeReservationAsync(reservation);

        if (resoult == null)
        {
            return BadRequest();
        }
        
        var mapped = _mapper.Map<ReservationGetDto>(resoult);
        
        return Ok(mapped);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservationAsync(int id)
    {
        var result = await _reservationService.DeleteReservationAsync(id);

        if (result == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}