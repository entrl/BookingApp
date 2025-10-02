using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.domain.Models;

namespace BookingApp.Api.Automapper;

public class RoomMappingProfiles : Profile
{
    public RoomMappingProfiles()
    {
        CreateMap<Room, RoomGetDto>();
        CreateMap<RoomPostPutDto, Room>();
    }
}