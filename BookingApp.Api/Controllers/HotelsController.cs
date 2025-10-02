using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Dal;
using BookingApp.domain.Abstractions.Repositories;
using BookingApp.domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : Controller
{
    private readonly IHotelsRepository _hotelRepo;
    private readonly IMapper _mapper;
    public HotelsController(IHotelsRepository repo, IMapper mapper)
    {
        _hotelRepo = repo;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelRepo.GetAllHotelsAsync();
        var hotelsGet =  _mapper.Map<List<HotelGetDto>>(hotels);
        
        return Ok(hotelsGet);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelRepo.GetHotelByIdAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }
        
        var hotelGet = _mapper.Map<HotelGetDto>(hotel);
        
        return Ok(hotelGet);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
    {
        var domainHotel = _mapper.Map<Hotel>(hotel);
        
        await _hotelRepo.CreateHotelAsync(domainHotel);
        
        var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);
        
        return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDto update, int id)
    {
        var toUpdate = _mapper.Map<Hotel>(update);
        toUpdate.HotelId = id;
        
        await _hotelRepo.UpdateHotelAsync(toUpdate);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _hotelRepo.DeleteHotelAsync(id);

        if (hotel == null)
        {
            return NotFound();  
        }
        
        return NoContent();
    }
    
    [HttpGet("{hotelId}/rooms")]
    public async Task<IActionResult> GetAllHotelRooms( int hotelId)
    {
        var rooms = await _hotelRepo.ListHotelRoomsAsync(hotelId);
        
        if (rooms == null)
        {
            return NotFound();
        }
        
        var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);
        
        return Ok(mappedRooms);
    }

    [HttpGet("{hotelId}/rooms/{roomId}")]
    public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
    {
        var room = await _hotelRepo.GetHotelRoomByIdAsync(hotelId, roomId);
        if (room == null)
        {
            return NotFound();
        }
        var mappedRoom = _mapper.Map<RoomGetDto>(room);
        
        return Ok(mappedRoom);
    }

    [HttpPost("{hotelId}/rooms")]
    public async Task<IActionResult> AddHotelRoom([FromBody] RoomPostPutDto newRoom, int hotelId)
    {
        var room = _mapper.Map<Room>(newRoom);

        await _hotelRepo.CreateHotelRoomAsync(hotelId, room);
        
        var mappedRoom = _mapper.Map<RoomGetDto>(room);
        
        return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = mappedRoom.RoomId }, mappedRoom);
    }

    [HttpPut("{hotelId}/rooms/{roomId}")]
    public async Task<IActionResult> UpdateHotelRoom([FromBody] RoomPostPutDto updatedRoom, int hotelId, int roomId)
    {
        var toUpdate = _mapper.Map<Room>(updatedRoom);
        toUpdate.HotelId = hotelId;
        toUpdate.RoomId = roomId;
        
        await _hotelRepo.UpdateHotelRoomAsync(hotelId, toUpdate);
        
        return NoContent();
    }

    [HttpDelete("{hotelId}/rooms/{roomId}")]
    public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomId)
    {
        var room = await _hotelRepo.DeleteHotelRoomAsync(hotelId, roomId);
        
        if (room == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    
}