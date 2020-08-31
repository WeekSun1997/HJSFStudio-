using HJSF.RepositoryServices;
using Library;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace Middleware.JwtMiddleware
{
    public static class JwtBearerExtensions
    {
        /// <summary>
        /// 添加AddJwtBearer
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appSetting">基本配置信息</param>
        public static void AddJwtBearer(this IServiceCollection services, AppSettingModel appSetting)
        {

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new JwtBearerRequirement()).Build();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ClockSkew = TimeSpan.FromMinutes(1),

                        ValidIssuer = appSetting.Jwt.JwtIssuer,
                        ValidAudience = appSetting.Jwt.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.Jwt.JwtSecurityKey)),
                    };

                    x.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = StatusCodes.Status200OK;
                            c.Response.ContentType = "application/json; charset=utf-8";
                            if (c.Exception is SecurityTokenExpiredException)
                            {
                                await c.Response.WriteAsync(Other.JsonToString(new ResultHelp(HJSF.Enum.ResponseCode.NonLogin, "登录超时，请重新登录")));
                            }
                            else
                            {
                                await c.Response.WriteAsync(Other.JsonToString(new ResultHelp(HJSF.Enum.ResponseCode.NonLogin, "信息验证失败，请重新登录")));
                            }
                        },
                        OnForbidden = async c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = StatusCodes.Status200OK;
                            c.Response.ContentType = "application/json; charset=utf-8";
                            await c.Response.WriteAsync(Other.JsonToString(new ResultHelp(HJSF.Enum.ResponseCode.NonRole, "没有操作权限")));
                        },
                        OnChallenge = async c =>
                        {
                            c.HandleResponse();
                            c.Response.StatusCode = StatusCodes.Status200OK;
                            c.Response.ContentType = "application/json; charset=utf-8";
                            await c.Response.WriteAsync(Other.JsonToString(new ResultHelp(HJSF.Enum.ResponseCode.NonLogin, "未登录或者登录超时")));
                        }
                    };
                });
        }

    }
}
