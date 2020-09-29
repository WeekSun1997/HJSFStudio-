using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{

    [SugarTable("HJSF_SysUser")]
    public partial class HjsfSysUser:BaseEntity
    {
      
        public string LoginId { get; set; }
        public string PassWord { get; set; }
        public int LoginCount { get; set; }
        public DateTime? LoginDate { get; set; }
        public string LoginIp { get; set; }
    }
}
