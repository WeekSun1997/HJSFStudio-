using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.AttributeEntity;

namespace Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtBearerRequirement : IAuthorizationRequirement
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class JwtBearerHandler : AuthorizationHandler<JwtBearerRequirement>
    {
        private readonly IAccountServer _accountService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        public JwtBearerHandler(IAccountServer accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtBearerRequirement requirement)
        {
            //是否经过验证
            if (context.Resource is Endpoint endpoint)
            {
                var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                //是否过滤权限验证
                bool isIgnoreRole = actionDescriptor.EndpointMetadata.Where(o => o.GetType() == typeof(IgnoreRoleAttribute)).Any();
                if (isIgnoreRole)
                {
                    context.Succeed(requirement);
                    
                }
            }
            else
            {
                var isAuthenticated = context.User.Identity.IsAuthenticated;
                if (!isAuthenticated)
                {
                    context.Fail();
                }
                else
                {
                    if (context.Resource is Endpoint endpoint1)
                    {
                        var actionDescriptor = endpoint1.Metadata.GetMetadata<ControllerActionDescriptor>();
                        //是否过滤权限验证
                        var isIgnoreRole = actionDescriptor.EndpointMetadata.Where(o => o.GetType() == typeof(IgnoreRoleAttribute)).Any();
                        if (isIgnoreRole)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            string url = $"{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}";
                            var result = await _accountService.CheckAuth(url?.ToLower());
                            result = false;
                            if (result)
                            {
                                context.Succeed(requirement);
                            }
                            else
                            {
                                context.Fail();
                            }
                        }
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }
          
        }
    }
}
