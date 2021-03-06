using AutoMapper;
using HotelListing.Data;
using HotelListing.Data.Entities;
using HotelListing.Models;

namespace HotelListing.Config
{
    public class MapperInit : Profile
    {
        public MapperInit()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CountryCreateDTO>().ReverseMap();
            CreateMap<Hotel,HotelDTO>().ReverseMap();
            CreateMap<Hotel, HotelCreateDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
