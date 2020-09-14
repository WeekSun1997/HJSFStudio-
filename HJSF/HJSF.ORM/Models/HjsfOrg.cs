using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfOrg
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string CascadeCode { get; set; }
        public string OrgTitle { get; set; }
        public string ShortName { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public string Ocode { get; set; }
    }
}
