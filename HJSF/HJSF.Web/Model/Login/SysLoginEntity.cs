using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model.Login
{
    /// <summary>
    /// 用户登录实体
    /// </summary>
    public class SysLoginEntity
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Verify { get; set; }
    }
}
