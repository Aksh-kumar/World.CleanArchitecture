using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            /******************  Response Mappping to Domain Entity *********/
            _ = CreateMap<GetCountryResponse, DomainEnt.Country>()
                .ForMember(x => x.Name, m => m.MapFrom(obj => obj.Name.ToString().Trim()))
                .ReverseMap()
                .ForMember(x => x.Name, m => m.MapFrom(obj => obj.Name.ToString().Trim()));
            /****************************************************************/

            /******************* Command Mapping to Domain Entity ***********/
            _ = CreateMap<CreateCountryCommand, DomainEnt.Country>()
                .ForMember(x => x.Name, m => m.MapFrom(obj => obj.Name.ToString().Trim()))
                .ReverseMap()
                .ForMember(x => x.Name, m => m.MapFrom(obj => obj.Name.ToString().Trim()));
            /****************************************************************/
        }
    }
}
