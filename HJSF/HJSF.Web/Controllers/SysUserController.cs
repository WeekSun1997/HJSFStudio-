using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using HJSF.ORM.Models;
using HJSF.Web.Model;
using HJSF.Web.Model.Login;
using Interface;
using ISqlSguar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryServices;
using Services;
using Utility;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 用户操作控制器
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class SysUserController : BaseApiController<HjsfSysUser, ISysUserServer>
    {
        /// <summary>
        ///  业务接口
        /// </summary>
        public ISysUserServer _server;
        /// <summary>
        /// 数据库接口
        /// </summary>
        public IDBServices _db;
        /// <summary>
        /// EF接口
        /// </summary>
        public IBaseRepository _baseRepository;
        /// <summary>
        /// 缓存接口
        /// </summary>
        public ICache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="base"></param>
        /// <param name="dB"></param>
        /// <param name="cache"></param>
        public SysUserController(ISysUserServer @server, IBaseRepository @base, IDBServices @dB, ICache @cache)
            : base(server, @base, @dB, @cache)
        {
            _server = @server;
            _db = @dB;
            _baseRepository = @base;
            _cache = @cache;

        }
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVerify")]
        public ActionResult GetVerify(string id)

        {
            var bs = VerifyHelper.Create();
            return File(bs, "image/png");
        }

        /// <summary>
        /// 登录方法-设置token
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ResponseModel<string>> Login([FromForm] SysLoginEntity entity)
        {
            var d = await _baseRepository.FisrtEntityAsync<HjsfSysUser>(a => a.Id == 1);
            var user = await base.FisrtEntityAsync<HjsfSysUser>(a => a.Id == 1);
            return null;
        }

    }
}
