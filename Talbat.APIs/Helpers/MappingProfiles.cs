using AutoMapper;
using Talabat.Core.Entites;
using Talbat.APIs.DTOs;

namespace Talbat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // dest --> destination class 
            // opt --> options
            // src --> source class
            CreateMap<Product, ProductToReturnDto>()

                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))

                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))

                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

        }
    }
}
