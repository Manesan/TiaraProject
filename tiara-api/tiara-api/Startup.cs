using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using tiara_api.AutoMapper;
using tiara_api.DataContext;
using tiara_api.Models;
using tiara_api.Repository;
using tiara_api.Repository.IRepository;

namespace tiara_api
{
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
            services.AddControllers();

            services.AddDbContext<DataContextDB>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            #region CORS
            services.AddCors();
            #endregion

            #region IEnumerable Loading
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            #endregion


            #region AutoMapper
            services.AddAutoMapper(typeof(Mappings));
            #endregion


            #region Repository DI Container
            services.AddScoped<IThoughtsRepository, ThoughtsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IGenCRUDRepository<Milestone>, GenCRUDRepository<Milestone>>();
            services.AddTransient<IGenCRUDRepository<Post>, GenCRUDRepository<Post>>();
            services.AddTransient<IGenCRUDRepository<Album>, GenCRUDRepository<Album>>();
            services.AddTransient<IGenCRUDRepository<Photo>, GenCRUDRepository<Photo>>();
            services.AddTransient<IGenCRUDRepository<Thought>, GenCRUDRepository<Thought>>();
            services.AddTransient<IGenCRUDRepository<Message>, GenCRUDRepository<Message>>();
            #endregion

            #region JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret)
                ;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }; 
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //needed for cors

            app.UseAuthentication(); //needed for JWT
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
