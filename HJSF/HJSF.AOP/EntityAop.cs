using Castle.DynamicProxy;
using HJSF.ORM.Models;
using Library;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Attributes;

namespace HJSF.AOP
{
    public class EntityAop : IInterceptor
    {

        private readonly IHttpContextAccessor _accessor;

        public EntityAop(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var InsertEntity = method.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(InsertEntityAttribute));
            if (InsertEntity != null)
            {
                if (InsertEntity is InsertEntityAttribute)
                {
                    var Properties = invocation.Arguments.FirstOrDefault().GetType().GetProperties();
                    if (Properties.Where(a => a.Name == "CreateUserId").Any())
                    {
                        var param = invocation.Arguments.FirstOrDefault();
                        var type = invocation.Arguments.FirstOrDefault().GetType();
                        _accessor.HttpContext.Session.TryGetValue("User", out byte[] userBytes);
                        var user = Other.SerializeToObject<HjsfSysUserInfo>(userBytes);
                        type.GetProperty("CreateUserId").SetValue(param, user.Id);
                        type.GetProperty("CreateUserName").SetValue(param, user.UserName);
                        type.GetProperty("CreateDate").SetValue(param, DateTime.Now);
                    }
                }
            }
            var EditEntity = method.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(EditEntityAttribute));
            if (EditEntity != null)
            {
                if (EditEntity is EditEntityAttribute)
                {
                    var Properties = invocation.Arguments.FirstOrDefault().GetType().GetProperties();
                    if (Properties.Where(a => a.Name == "UpdateUserId").Any())
                    {
                        var param = invocation.Arguments.FirstOrDefault();
                        var type = invocation.Arguments.FirstOrDefault().GetType();
                        _accessor.HttpContext.Session.TryGetValue("User", out byte[] userBytes);
                        var user = Other.SerializeToObject<HjsfSysUserInfo>(userBytes);
                        type.GetProperty("UpdateUserId").SetValue(param, user.Id);
                        type.GetProperty("UpdateUserName").SetValue(param, user.UserName);
                        type.GetProperty("UpdateDate").SetValue(param, DateTime.Now);
                    }

                }
            }
            invocation.Proceed();
        }
    }
}
