namespace FoodTracker.Data.UnitOfWork.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using FoodTracker.Core.Entities.Base;
    using FoodTracker.Data.Repository.Interfaces;

    public interface IUnitOfWork : IDisposable
    {
         IRepository<T> GetRepository<T>() where T: class, IEntity;
         Task SaveChangesAsync();
    }
}