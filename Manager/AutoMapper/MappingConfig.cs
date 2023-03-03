using AutoMapper;
using TransfloDriver.Models;
using TransfloDriver.Models.Dto;

namespace TransfloDriver
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();
         

        }
    }
}
