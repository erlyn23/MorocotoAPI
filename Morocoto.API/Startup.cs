using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Morocoto.Domain.Contracts;
using Morocoto.Domain.DbContexts;
using Morocoto.Infraestructure.Implementations;
using Morocoto.Infraestructure.Services;
using Morocoto.Infraestructure.Services.Contracts;
using Morocoto.Infraestructure.Tools;
using Morocoto.Infraestructure.Tools.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.API
{
    public class Startup
    {
        private readonly string _myCors = "MyCorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(builder =>
            {
                builder.AddPolicy(_myCors, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Morocoto.API", Version = "v1" });
            });
            services.AddDbContext<MorocotoDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("MorocotoDbConnection")
            ));

            var key = Encoding.ASCII.GetBytes(Configuration["MySecretKey"]);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => 
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Dependency Injection: Unit Of Work.
            services.AddTransient<IAsyncUnitOfWork, UnitOfWork>();

            //Dependency Injection: Account.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAsyncUserRepository, UserRepository>();
            services.AddTransient<IAsyncUserAddressRepository, UserAddressRepository>();
            services.AddTransient<IAsyncUserPhoneNumberRepository, UserPhoneNumberRepository>();
            services.AddTransient<IAccountTools, AccountTools>();

            //Dependency Injection: Business.
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<IAsyncBusinessRepository, BusinessRepository>();
            services.AddTransient<IAsyncBusinessPhoneNumberRepository, BusinessPhoneNumberRepository>();
            services.AddTransient<IAsyncBusinessAddressRepository, BusinessAddressRepository>();
            services.AddTransient<IAsyncBusinessBillRepository, BusinessBillRepository>();

            //Dependency Injection: Email.
            services.AddSingleton<IEmailTools, EmailTools>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Morocoto.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(_myCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
