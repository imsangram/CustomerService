using AutoMapper;
using Customer.Dto;

namespace Customer.BL
{
    public class Mapper
    {
        public static IMapper GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Entity.Customer, CustomerCreateDto>().ReverseMap();
                cfg.CreateMap<Entity.Customer, CustomerDto>().ReverseMap();
            });
            return config.CreateMapper();
        }
    }
}
