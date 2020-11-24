using HJSF.RepositoryServices;
using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    [SugarTable("HJSF_SysUserInfo")]
    public partial class HjsfSysUserInfo: IRepositoryEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string ShortName { get; set; }
        public int Sex { get; set; }
        public long EmpStatus { get; set; }
        public long OrgId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string WeChatOpenId { get; set; }
        public string Birthday { get; set; }
        public bool IsSalesMan { get; set; }
        public bool IsOperator { get; set; }
        public string Signature { get; set; }
    }
}
