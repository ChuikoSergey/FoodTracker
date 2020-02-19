namespace FoodTracker.Data.Repository.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using FoodTracker.Core.Entities.Base;

    public interface IRepository<T> where T: class, IEntity
    {
        Task<IQueryable<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate, Func<T, TResult> selector);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T model);
        Task UpdateAsync(T model);
        Task RemoveAsync(Guid id);
    }
}