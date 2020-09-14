using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUser
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string LoginId { get; set; }
        public string PassWord { get; set; }
        public int LoginCount { get; set; }
        public DateTime? LoginDate { get; set; }
        public string LoginIp { get; set; }
    }
}
