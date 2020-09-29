using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowScheme
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public string SchemeContent { get; set; }
        public long OrgId { get; set; }
        public long CategoryId { get; set; }
        public long FormId { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public string ModelType { get; set; }
    }
}
