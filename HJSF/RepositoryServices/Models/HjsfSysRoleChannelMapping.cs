using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    [SugarTable("HJSF_SysRoleChannelMapping")]
    public class HjsfSysRoleChannelMapping
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public long ChannelId { get; set; }
        public long RoleId { get; set; }
    }
}
