using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model.Login
{
    /// <summary>
    /// 部门信息储存实体
    /// </summary>
    public class AccountUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户部门Id
        /// </summary>
        public long OrgId { get; set; }
    }
}
