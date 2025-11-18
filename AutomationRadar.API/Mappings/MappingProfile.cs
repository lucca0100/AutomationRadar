using AutoMapper;
using AutomationRadar.Model.DTOs;
using AutomationRadar.Model.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomationRadar.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Occupation, OccupationCreateDto>().ReverseMap();
            CreateMap<Occupation, OccupationUpdateDto>().ReverseMap();

            CreateMap<AutomationRisk, AutomationRiskCreateDto>().ReverseMap();
            CreateMap<AutomationRisk, AutomationRiskUpdateDto>().ReverseMap();

            CreateMap<CareerTransition, CareerTransitionCreateDto>().ReverseMap();
            CreateMap<CareerTransition, CareerTransitionUpdateDto>().ReverseMap();
        }
    }
}
