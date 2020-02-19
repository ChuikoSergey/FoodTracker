namespace FoodTracker.Data.Repository.Implementations
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using FoodTracker.Core.Entities.Base;
    using FoodTracker.Data.Context;
    using FoodTracker.Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        #region Fields

        private DataContext Context { get; set; }
        private DbSet<T> Set { get; }

        #endregion


        #region Constructors

        public Repository(DataContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        #endregion

        #region Interface members

        public Task CreateAsync(T model)
        {
            Context.Entry(model).State = EntityState.Added;
            return Task.CompletedTask;
        }

        public Task<IQueryable<TResult>> GetAsync<TResult>(Expression<Func<T, bool>> predicate, Func<T, TResult> selector)
        {
            return Task.FromResult(Set.Where(predicate).Select(selector).AsQueryable());
        }

        public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public Task RemoveAsync(Guid id)
        {
            var entity = Set.FirstOrDefault(e => e.Id == id);
            Context.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T model)
        {
            Context.Entry(model).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        #endregion
    }
}