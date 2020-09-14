using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowInstance
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string FlowCode { get; set; }
        public string Description { get; set; }
        public int FlowStatus { get; set; }
        public int FlowType { get; set; }
        public long Infoid { get; set; }
        public long FlowSchemeId { get; set; }
        public string FlowSchemeContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ApprovalUserCode { get; set; }
        public string ApprovalUserName { get; set; }
        public bool IsWeChat { get; set; }
    }
}
