using AutoMapper;
using AutoMapper.Execution;
using Talabat.Core.Entites;
using Talbat.APIs.DTOs;

namespace Talbat.APIs.Helpers
{

    //                                  source , distination , pictureUrl datatype at productToReurtnDto
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {

        private readonly IConfiguration _Configrations;

        public ProductPictureUrlResolver(IConfiguration configrations)
        {
            _Configrations = configrations;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {

                return $"{_Configrations["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;


        }
    }
}
