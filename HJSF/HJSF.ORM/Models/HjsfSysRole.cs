using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{

    [SugarTable("HjsfSysRole")]
    public partial class HjsfSysRole
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public int RoleParent { get; set; }
    }
}
