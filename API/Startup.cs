using API.Data;
using API.Data.Entities;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Hubs;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API
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

            services.AddDbContextPool<DataContext>(options => options
                .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            //    , mySqlOptions => mySqlOptions
            //        .ServerVersion(new Version(7, 1, 2), ServerType.MySql)
            //));

            //,
            //    mySqlOptions => mySqlOptions
            //        .ServerVersion(new Version(8, 0, 18), ServerType.MySql);

            //services.AddDbContext<DataContext>(x =>
            //    x.UseSqlServer(Configuration
            //        .GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc()
                .AddJsonOptions(options => { options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //// CORS

            //var corsBuilder = new CorsPolicyBuilder();

            //corsBuilder.AllowAnyHeader();
            //corsBuilder.AllowAnyMethod();
            //corsBuilder.AllowAnyOrigin(); // For anyone access.

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            // database            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<UserRepository>();

            // services
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IMessageService, MessageService>();

            // websockets

            services.AddSignalR();
            //services.AddScoped<TestHub>();
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

            app.UseCors("SiteCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };

            webSocketOptions.AllowedOrigins.Add("https://localhost:3000");

            app.UseWebSockets(webSocketOptions);

            app.UseEndpoints(endpoints => {
                endpoints.MapHub<NotificationHub>("/notifications");
                endpoints.MapControllers();
            });
        }
    }
}