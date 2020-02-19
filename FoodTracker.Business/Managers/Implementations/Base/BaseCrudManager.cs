namespace FoodTracker.Business.Managers.Implementations.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FoodTracker.Business.Managers.Interfaces.Base;
    using FoodTracker.Core.Entities.Base;
    using FoodTracker.Data.Repository.Interfaces;
    using FoodTracker.Data.UnitOfWork.Interfaces;

    public class BaseCrudManager<T> : ICrudManager<T> where T : class, IEntity
    {
        #region Fields
            
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<T> repository;

        #endregion

        #region Properties

        protected IUnitOfWork UnitOfWork { get => unitOfWork; }
        protected IRepository<T> Repository { get => repository; }
            
        #endregion

        #region Constructors

        protected BaseCrudManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<T>();
        }
            
        #endregion

        #region Interface members        

        public virtual async Task<Guid> CreateAsync(T model)
        {
            await Repository.CreateAsync(model);
            await UnitOfWork.SaveChangesAsync();
            return model.Id;
        }

        public virtual async Task<List<TResult>> GetAsync<TResult>(Func<T, TResult> selector)
        {
            return (await Repository.GetAsync(e => true, selector)).ToList();
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return Repository.GetFirstAsync(e => e.Id == id);
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            await Repository.RemoveAsync(id);
            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T model)
        {
            await Repository.UpdateAsync(model);
            await UnitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}