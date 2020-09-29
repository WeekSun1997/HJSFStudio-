using System;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Cache;
using HJSF.AOP;
using HJSF.Enum;
using HJSF.RepositoryServices;
using Interface.ISqlSguar;
using ISqlSguar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middleware;
using Middleware.JwtMiddleware;
using RepositoryServices;
using Utility;
namespace HJSF.Web
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 注入方法
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            // 缓存基本信息
            services.Configure<AppSettingModel>(Configuration.GetSection("AppSetting"));
            services.AddScoped<IAuthorizationHandler, Middleware.JwtBearerHandler>();
             Constant.AppSetting = Configuration.GetSection("AppSetting").Get<AppSettingModel>();

            services.AddSingleton<IDBServices, DBServices>(x => new DBServices(Configuration["AppSetting:DataBase:ContextConn"]));
          
            services.AddControllers();
            services.AddMemoryCache();
            #region Jwt
           

            #endregion
            // 添加跨域控制
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                    //.AllowAnyOrigin() //允许任何来源的主机访问
                    .WithOrigins(Constant.AppSetting.App.Cors)//.SetIsOriginAllowedToAllowWildcardSubdomains()//设置允许访问的域
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 添加Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
            });
          
            services.AddControllersWithViews()
         .AddControllersAsServices();//这里要写
            services.AddJwtBearer(Constant.AppSetting);
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Web接口",
                    Version = "版本1"

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                option.IncludeXmlComments(xmlPath, true);
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求Header中需要添加的JWT授权代码：Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                };
                option.AddSecurityDefinition("Bearer", securityScheme);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterType<RedisAOP>();
            builder.RegisterType<RedisHelp>()
                .As<ICache>()
                 .WithParameter("_connectionString", Configuration["AppSetting:Redis:RedisHostConnection"])
                .PropertiesAutowired()//开始属性注入
                .InstancePerLifetimeScope();//即为每一个依赖或调用创建一个单一的共享的实例
            Assembly services = Assembly.LoadFrom("Services");
            Assembly repository = Assembly.Load("Interface");
            builder.RegisterAssemblyTypes(services, repository).
            Where(x => x.Name.EndsWith("Server", StringComparison.OrdinalIgnoreCase))
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors();
            // .InterceptedBy(typeof(RedisAOP));
            //builder.RegisterType<BaseRepository>()
            //                .As<IBaseRepository>()
            //                 .PropertiesAutowired()//开始属性注入
            //    .InstancePerLifetimeScope();//即为每一个依赖或调用创建一个单一的共享的实例



            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            // 配置Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = "doc";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseRouting();
            app.UseSession();
         
          
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            // 配置授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
