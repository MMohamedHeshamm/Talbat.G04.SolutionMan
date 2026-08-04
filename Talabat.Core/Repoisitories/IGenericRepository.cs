using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repoisitories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region WithoutSpecification
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        #endregion


        #region With Specification

        Task<T> GetByIdWithSpecAsync(int id, ISpecification<T> Spec); 

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);


        #endregion



    }
}
