using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;


namespace Talabat.Repoistory
{
    public class SpecificationEvalutor<T> where T : BaseEntity
    {

        //**** _dbContext.Set<T>().Where(spec.Criteria).Include(spec.Includes) ****

       
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            //check if the specification has a criteria, if it does, we apply it to the query
            if (spec.Criteria != null)
            {
                
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExperssion) => currentQuery.Include(includeExperssion));
            return query;
        }


    }
}
