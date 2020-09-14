using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowEditField
    {
        public long Id { get; set; }
        public long FlowId { get; set; }
        public long FlowType { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Content { get; set; }
    }
}
