using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUserRoleMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
