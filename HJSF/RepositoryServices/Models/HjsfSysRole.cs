using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{

    [SugarTable("HJSF_SysRole")]
    public partial class HjsfSysRole:BaseEntity
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public int RoleParent { get; set; }
    }
}
