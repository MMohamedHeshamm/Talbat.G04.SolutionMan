using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Repoistory.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talabat.Core.Specifications;

namespace Talabat.Repoistory
{

    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        // normal function to get all data from the database return iEnumerable of T
        #region WithOut Spec
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>)await _dbContext.Products.Include(b => b.Brand).Include(c => c.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }



        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await _dbContext.Products.Where(p => p.Id == id).Include(b => b.Brand).Include(c => c.Category).FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }


        public async Task<T> GetByIdWithSpecAsync(int id, ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }
    }
}
