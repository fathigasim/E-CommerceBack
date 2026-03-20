using EcommerceApplication.Common.Settings;
using EcommerceApplication.Interfaces;
using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using EcommerceInfrastructure.Payments;
using EcommerceInfrastructure.Persistance;
using EcommerceInfrastructure.Repository;
using EcommerceInfrastructure.Services;
using MediaRTutorialApplication.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace EcommerceInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // Stripe configuration
            var stripeSettings = configuration
                .GetSection(StripeSettings.SectionName)
                .Get<StripeSettings>()!;

            StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            services.Configure<StripeSettings>(
                configuration.GetSection(StripeSettings.SectionName));

            // DbContext (REGISTER ONCE)
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // JWT options (OPTIONS PATTERN)
            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            // Authentication
            var jwtSection = configuration.GetSection("JwtSettings");

            services.Configure<JwtSettings>(jwtSection);

            services.AddAuthentication(options=> { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidAudience = jwtSection["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAuthorization();
            // Infrastructure services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IPagedRepository<>), typeof(PagedRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();


            services.AddScoped<IPaymentService, StripePaymentService>();
            services.AddScoped<IBasketContextAccessor, BasketContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbSeeder, DbSeeder>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}