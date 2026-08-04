using System;
using System.Collections.Generic;
using System.Text;

using Talabat.Core.Entites;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


        public BaseSpecifications()
        {

        }

        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }


    }
}
