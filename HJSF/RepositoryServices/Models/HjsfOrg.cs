using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    [SugarTable("HJSF_Org")]
    public class HjsfOrg:BaseEntity
    {
        public string CascadeCode { get; set; }
        public string OrgTitle { get; set; }
        public string ShortName { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public string Ocode { get; set; }
    }
}
