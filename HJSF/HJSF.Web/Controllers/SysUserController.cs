using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HJSF.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 用户操作控制器
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class SysUserController : Controller
    {
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
    }
}
