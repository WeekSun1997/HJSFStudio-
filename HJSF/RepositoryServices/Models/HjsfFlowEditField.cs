using HJSF.RepositoryServices.Models;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowEditField:BaseEntity
    {
       
        public long FlowId { get; set; }
        public long FlowType { get; set; }
        public string Content { get; set; }
    }
}
