using Talabat.Core.Entites;

namespace Talbat.APIs.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }  // FK 
        public string Brand { get; set; } // navigation prop [ One ]

        public int CategoryId { get; set; } // FK   ( msh ma7tag Fluent Api wala data annotation ) 
        public string Category { get; set; } //Nav prop [ one ]

    }
}
