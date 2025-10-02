using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.domain.Models;

namespace BookingApp.Api.Automapper;

public class ReservationMappingProfiles : Profile
{
    public ReservationMappingProfiles()
    {
        CreateMap<ReservationPostPutDto, Reservation>();
        CreateMap<Reservation, ReservationGetDto>();
    }
}