using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUserOrgMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long OrgId { get; set; }
    }
}
