using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //sign for property for where condation [where(p=> p.Id == Id)]
        public Expression<Func<T, bool>> Criteria { get; set; }

        //sign for property for include condation [include(p=> p.ProductType).include(p=> p.ProductBrand)]
        public List<Expression<Func<T, object>>> Includes { get; set; }


    }
}
