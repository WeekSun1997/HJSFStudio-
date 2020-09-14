using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUserInfo
    {
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
