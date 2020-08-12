using Cache;
using Interface.ISqlSguar;
using ISqlSguar;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public static class DenpendencyExtensions
    {
        /// <summary>
        /// 添加Ico注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="iocDllList"></param>
        /// <returns></returns>
        public static IServiceCollection AddDenpendency(this IServiceCollection services, string[] iocDllList)
        {
            //注入特殊类型

            //静态对象
            //services.AddSingleton<IService, Service>();
            //范围对象
            //services.AddScoped<IService, Service>();
            //临时对象
            //services.AddTransient<IService, Service>();


            //// Memory缓存
            //services.AddSingleton<ICache, MemoryCacheHelper>();
            //// Redis缓存
            services.AddSingleton<ICache, RedisHelp>();
           

            //// 添加其他服务
            services.AddSingleton<IDBServices, DBServices>();

            //// 图像验证码
            //services.AddTransient<VerifyHelper>();

            //// JWT
            //services.AddScoped<IAuthorizationHandler, JwtBearerHandler>();
            ////services.AddScoped<Utility.Auth.IAuthBearer, AccountService>();

            //if (iocDllList == null || !iocDllList.Any()) return services;

            //foreach (var dll in iocDllList)
            //{
            //    var dllSplit = dll.Split(',');
            //    ////接口层所在程序集命名空间
            //    var interfaceList = Assembly.Load(dllSplit[1]);
            //    ////业务逻辑层所在程序集命名空间
            //    var serviceList = Assembly.Load(dllSplit[0]);

            //    var interfaceTypes = interfaceList.GetTypes().Where(t => t.IsInterface && t.Name.EndsWith("Service"));
            //    var implementTypes = serviceList.GetTypes().Where(t => t.IsClass && t.Name.EndsWith("Service"));

            //    foreach (var implementType in implementTypes)
            //    {
            //        var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
            //        if (interfaceType != null)
            //            services.AddScoped(interfaceType, implementType);
            //    }

            //}

            //ServiceProviderHelper.ServiceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}
