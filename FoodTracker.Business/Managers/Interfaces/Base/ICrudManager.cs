namespace FoodTracker.Business.Managers.Interfaces.Base
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FoodTracker.Core.Entities.Base;

    public interface ICrudManager<T> where T : class, IEntity
    {
        Task<List<TResult>> GetAsync<TResult>(Func<T, TResult> selector);
        Task<T> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(T model);
        Task UpdateAsync(T model);
        Task RemoveAsync(Guid id);
    }
}