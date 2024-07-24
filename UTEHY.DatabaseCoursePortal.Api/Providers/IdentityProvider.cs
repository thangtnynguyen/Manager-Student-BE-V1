using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using UTEHY.DatabaseCoursePortal.Api.Constants;

namespace UTEHY.DatabaseCoursePortal.Api.Providers
{
    public static class IdentityProvider
    {
        public static IServiceCollection AddIdentityProvider(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddIdentity<User, Data.Entities.Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = null;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            var jwtKey = builder.Configuration.GetValue<string>("Jwt:Key");
            var audience = builder.Configuration.GetValue<string>("Jwt:Audience");
            var issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

            var permissionType = typeof(Constants.Permission);
            var permissionFields = permissionType.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && !f.IsInitOnly)
            .Select(f => (string)f.GetValue(null))
            .ToList();

            services.AddAuthorization(options =>
            {
                foreach (var permission in permissionFields)
                {
                    options.AddPolicy(permission, policy =>
                    {
                        policy.RequireClaim("Permission", permission);
                    });
                }
            });

            return services;
        }
    }
}
