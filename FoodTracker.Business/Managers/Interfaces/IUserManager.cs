namespace FoodTracker.Business.Managers.Interfaces
{
    using System.Threading.Tasks;
    using FoodTracker.Business.Managers.Interfaces.Base;
    using FoodTracker.Core.Entities;

    public interface IUserManager : ICrudManager<User>
    {
        Task<User> GetByCredentialsAsync(string login, string password);
    }
}