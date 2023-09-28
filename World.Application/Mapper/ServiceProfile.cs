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
            CreateMap<GetCountryResponse, DomainEnt.Country>().ReverseMap();
            /****************************************************************/

            /******************* Command Mapping to Domain Entity ***********/
            CreateMap<CreateCountryCommand, DomainEnt.Country>().ReverseMap();
            /****************************************************************/
        }
    }
}
