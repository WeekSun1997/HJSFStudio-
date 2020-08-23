using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginCode { get; set; }
        public string Phone { get; set; }
        public string Eamil { get; set; }
        public string Address { get; set; }
        public string OpenId { get; set; }
        public int? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
