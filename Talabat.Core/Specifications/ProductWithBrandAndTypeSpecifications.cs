using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        // ** get all product  **
        //When get all product we want to include the brand and category of product 
        public ProductWithBrandAndTypeSpecifications( string sort) : base()
        {

            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

        }

        //** Get by id specification **
        //when get product by id we want to include the brand and category of product
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }

    }
}
