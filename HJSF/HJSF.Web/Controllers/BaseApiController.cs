using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HFJS.Entity.ResponseModel;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HJSF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController<T, TService> : ControllerBase
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

        /// <summary>
        /// 添加保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>新增结果</returns>
        [HttpPost("add")]
        //[ValidateModel]
        public virtual async Task<BaseResponse> Add([FromForm] T entity)
        {
            var result = await _defaultService.InsertAsync(entity);

            return new BaseResponse(); ;
        }

    }
}
