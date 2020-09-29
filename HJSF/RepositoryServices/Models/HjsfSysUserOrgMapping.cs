using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{

    [SugarTable("HJSF_SysUserOrgMapping")]
    public  class HjsfSysUserOrgMapping
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long OrgId { get; set; }
    }
}
