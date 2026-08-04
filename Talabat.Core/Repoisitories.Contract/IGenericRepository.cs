using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;

namespace Talabat.Core.Repoisitories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();




    }
}
