using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Repoistory.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Talabat.Repoistory
{

    public  class GenericRepo<T> : IGenericRepostiry<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepo(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
