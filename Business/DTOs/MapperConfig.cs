using AutoMapper;
using Entities.Models;

namespace Application.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Person, Person_DTO>();
                cfg.CreateMap<Person_DTO, Person>();

            });
        }
    }
}
