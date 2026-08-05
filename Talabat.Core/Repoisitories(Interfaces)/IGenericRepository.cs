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
        Task<IEnumerable<T>> GetAll_Async();
        Task<T> GetById_Async(int id);

        #endregion


        #region With Specification


        Task<IEnumerable<T>> GetAll_Async_Spec(ISpecification<T> Spec);
        Task<T> GetById_Async_Spec( ISpecification<T> Spec); 


        #endregion



    }
}
