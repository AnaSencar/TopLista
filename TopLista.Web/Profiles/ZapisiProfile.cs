using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLista.Web.Helpers;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Profiles
{
    public class ZapisiProfile : Profile
    {
        public ZapisiProfile()
        {
            CreateMap<Zapis, ZapisViewModel>()
                .ForMember(
                    dest => dest.Vrijeme,
                    opt => opt.MapFrom(src => src.VrijemeUSekundama.GetStringRepresentationOfTime())
                );

            CreateMap<ZapisForCreationViewModel, Zapis>()
                .ForMember(
                    dest => dest.VrijemeUSekundama,
                    opt => opt.MapFrom(src => TimeConversion.GetTimeInSeconds(src.Sat, src.Min, src.Sec))
                )
                .ForMember(
                dest => dest.Odobreno,
                opt => opt.MapFrom(src => false)
                )
                .ForMember(dest => dest.DatumUnosa,
                opt => opt.MapFrom(src => DateTime.Now)
                );
        }
    }
}
