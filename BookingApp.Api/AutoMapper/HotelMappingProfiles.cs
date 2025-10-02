using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.domain.Models;

namespace BookingApp.Api.Automapper;

public class HotelMappingProfiles : Profile
{
    public HotelMappingProfiles()
    {
        CreateMap<HotelCreateDto, Hotel>();
        CreateMap<Hotel, HotelGetDto>();
    }
}