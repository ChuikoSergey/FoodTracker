namespace FoodTracker
{
    using System.Linq;
    using System.Reflection;
    using FoodTracker.Auth;
    using FoodTracker.Business.Managers.Implementations.Base;
    using FoodTracker.Business.Managers.Interfaces.Base;
    using FoodTracker.Data.Context;
    using FoodTracker.Data.Repository.Implementations;
    using FoodTracker.Data.Repository.Interfaces;
    using FoodTracker.Data.UnitOfWork.Implementations;
    using FoodTracker.Data.UnitOfWork.Interfaces;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database

            services.AddDbContext<DataContext>();
            services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Transient));

            #endregion

            #region Authentication

            services.AddAuthentication(ao =>
            {
                ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.Key
                    };
                });

            #endregion

            #region Managers

            var assembly = typeof(BaseCrudManager<>).Assembly;
            var assemblyTypes = assembly.DefinedTypes;
            var managerInterfaces = assemblyTypes.Where(t => t.IsInterface && t.ImplementedInterfaces.Any(ii => ii.GetGenericTypeDefinition() == typeof(ICrudManager<>)));
            foreach (var managerInterface in managerInterfaces)
            {
                var managerImplementationType = assemblyTypes.FirstOrDefault(t => t.ImplementedInterfaces.Any(ii => ii == managerInterface));
                services.Add(new ServiceDescriptor(managerInterface, managerImplementationType, ServiceLifetime.Transient));
            }

            #endregion

            services.AddRouting();
            services.AddAuthorization();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
