using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Phone { get; set; }
        public string Eamil { get; set; }
        public string Address { get; set; }
        public bool? Stuats { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
