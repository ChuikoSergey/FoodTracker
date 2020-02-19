namespace FoodTracker.Business.Managers.Implementations
{
    using System;
    using System.Threading.Tasks;
    using FoodTracker.Business.Managers.Implementations.Base;
    using FoodTracker.Business.Managers.Interfaces;
    using FoodTracker.Core.Entities;
    using FoodTracker.Data.UnitOfWork.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UserManager : BaseCrudManager<User>, IUserManager
    {

        #region Constructors

        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region Interface members

        public override Task<Guid> CreateAsync(User model) 
        {
            var passwordHasher = new PasswordHasher<User>();
            model.Password = passwordHasher.HashPassword(model, model.Password);
            model.Height = 200;
            model.BirthDay = DateTime.Now;
            return base.CreateAsync(model);
        }

        public async Task<User> GetByCredentialsAsync(string login, string password)
        {
            var user = await Repository.GetFirstAsync(u => u.Email == login);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                if (passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Failed)
                {
                    return user;
                }
            }
            return null;
        }

        #endregion
    }
}