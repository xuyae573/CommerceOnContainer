using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProductAPI.JWT;
using ProductAPI.Repository;
using ProductAPI.Repository.Configuration;
using ProductAPI.Service;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CatalogAPI
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
            AuthConfigurer.Configure(services, Configuration);
            AddJWTBearerSwaggerConfiguartion(services);
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<CompanyRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICompanyService, CompanyService>();

            var connectstring = Configuration["MongoDbSetting:ConnectionString"];
            services.AddSingleton<IMongoDbConfiguration>(new MongoDbConfiguration()
            {
                ConnectionString = Configuration["MongoDbSetting:ConnectionString"],
                DatabaseName = Configuration["MongoDbSetting:DatabaseName"]
            });
            services.AddTransient<IMongoDatabaseProvider, MongoDatabaseProvider>();

            //Caching Register
            var cacheConfiguration = new CacheConfiguration();
            Configuration.GetSection("CacheSetting").Bind(cacheConfiguration);
            services.AddSingleton(cacheConfiguration);
            services.AddTransient<ICacheManager, PerRequestCacheManager>();

            if (cacheConfiguration.EnableRedis)
            {
                services.AddSingleton<RedisConnectionWrapper>();
                services.AddSingleton<ILocker>(x => (ILocker)x.GetRequiredService<RedisConnectionWrapper>());
                services.AddSingleton<IRedisConnectionWrapper>(x => (IRedisConnectionWrapper)x.GetRequiredService<RedisConnectionWrapper>());
                services.AddTransient<IStaticCacheManager, RedisCacheManager>();
            }
            else
            {
                services.AddSingleton<MemoryCacheManager>();
                services.AddSingleton<ILocker>(x => (ILocker)x.GetRequiredService<MemoryCacheManager>());
                services.AddSingleton<IStaticCacheManager>(x => (IStaticCacheManager)x.GetRequiredService<MemoryCacheManager>());
            }

        }

        private static void AddJWTBearerSwaggerConfiguartion(IServiceCollection services)
        {
            var security = new Dictionary<string, IEnumerable<string>>
            {
                { "ApiKeyAuth", new string[]{ } },
            };

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new Info { Title = "Product API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("ApiKeyAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(security);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal Server Error",
                            Msg = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
            });
            app.UseCors(builder => builder.AllowAnyOrigin().AllowCredentials().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication(); //must be before UseMvc Method
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "SimpleCommerce Product Documentation";
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "Product API V1");
            });
            loggerFactory.AddLog4Net();
        }
    }
}
