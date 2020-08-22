using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HFJS.Entity.ResponseModel;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryServices;

namespace HJSF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController<T, TService>: ControllerBase
        where T : class, new()
        where TService : IBaseServer<T>
    {
        /// <summary>
        /// 默认使用接口
        /// </summary>
        protected TService _defaultService;
 
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="service"></param>
        public BaseApiController(TService service)
        {
            _defaultService = service;
        }

        

    }
}
