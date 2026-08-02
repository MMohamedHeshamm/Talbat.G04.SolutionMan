using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }


        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }


        public int BrandId { get; set; }  // FK 

        public ProductBrand Brand { get; set; } // navigation prop [ One ]


        public int CategoryId { get; set; } // FK   ( msh ma7tag Fluent Api wala data annotation ) 

        public ProductCategory Category { get; set; } //Nav prop [ one ]

    }
}
