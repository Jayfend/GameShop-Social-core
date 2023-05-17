using FluentValidation.AspNetCore;
using GameShop.Application;
using GameShop.Application.Common;
using GameShop.Application.Module;
using GameShop.Application.Services;
using GameShop.Application.Services.Carts;
using GameShop.Application.Services.Categories;
using GameShop.Application.Services.Charts;
using GameShop.Application.Services.Checkouts;
using GameShop.Application.Services.Comments;
using GameShop.Application.Services.Contacts;
using GameShop.Application.Services.Games;
using GameShop.Application.Services.Publishers;
using GameShop.Application.Services.Wishlists;
using GameShop.Application.System.Roles;
using GameShop.Application.System.Users;
using GameShop.Application.Utilities;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities;
using GameShop.Utilities.Configurations;
using GameShop.Utilities.Constants;
using GameShop.Utilities.Redis;
using GameShop.ViewModels.System.Users;
using Hangfire;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace GameShop
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

            services.AddDbContext<GameShopDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));
            services.AddIdentity<AppUser, AppRole>()
               .AddEntityFrameworkStores<GameShopDbContext>()
               .AddDefaultTokenProviders();
            services.AddScoped<ITransactionCustom, TransactionCustom>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IWishlistService, WishlistService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IChartService, ChartService>();
            services.AddTransient<ISaveFileService, SaveFileService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddScoped<IRedisConnectionFactory, RedisConnectionFactory>();
            services.AddScoped<IRedisUtil, RedisUtil>();
            services.AddSignalR();
            services.AddSingleton<IDictionary<string, AppUser>>(opts => new Dictionary<string, AppUser>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITransactionCustom, TransactionCustom>();
            services.AddScoped<IElasticSearchUlti, ElasticSearchUlti>();
            services.AddTransient<ITOTPService, TOTPService>();
            services.Configure<RedisConfig>(Configuration.GetSection("Redis"));
            services.Configure<ElasticSearchConfig>(Configuration.GetSection("ElasticSearch"));
            services.Configure<ESConfig>(Configuration.GetSection("RemoteServices:ESService"));
            services.AddElasticsearch(Configuration);
            //services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddControllers()
             .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger GameShop Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(Configuration.GetConnectionString("Hangfire"));

                x.UseConsole();

                x.UseRecurringJob("recurringjob.json");
            });
            services.AddHangfireServer();
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5000").AllowAnyMethod().AllowAnyHeader();
                builder.WithOrigins("http://localhost:5003").AllowAnyMethod().AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = null,
                IgnoreAntiforgeryToken = true
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger GameShop Api V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}