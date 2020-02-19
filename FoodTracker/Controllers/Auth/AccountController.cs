namespace FoodTracker.Controllers.Auth
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FoodTracker.Auth;
    using FoodTracker.Auth.DTO;
    using FoodTracker.Business.Managers.Interfaces;
    using FoodTracker.Core.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    [Route("account")]
    public class AccountController : Controller
    {
        #region Fields

        private IUserManager UserManager { get; }

        #endregion

        #region Constructors

        public AccountController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        #endregion

        #region Actions

        [Authorize, HttpPost, Route("token")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginModel)
        {
            var user = await UserManager.GetByCredentialsAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                //...
            }
            var now = DateTime.Now;

            var jwt = new JwtSecurityToken
            (
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.Key, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                access_token = encodedJwt,
                username = user.Email
            });
        }

        [AllowAnonymous, HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpDTO model)
        {
            var user = new User
            {
                Email = model.Email,
                Password = model.Password
            };
            await UserManager.CreateAsync(user);
            return Ok();
        }

        #endregion
    }
}