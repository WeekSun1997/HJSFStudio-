using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysRoleChannelMapping
    {
        public long Id { get; set; }
        public long ChannelId { get; set; }
        public long RoleId { get; set; }
    }
}
