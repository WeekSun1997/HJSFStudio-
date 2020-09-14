using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowNode
    {
        public long Id { get; set; }
        public long FlowId { get; set; }
        public string FlowCode { get; set; }
        public string FlowNodeCode { get; set; }
        public int NodeType { get; set; }
        public string NodeName { get; set; }
        public string NodeId { get; set; }
        public int BackType { get; set; }
        public string CallBack { get; set; }
        public string NodeRemark { get; set; }
        public long? UserId { get; set; }
        public int? UserType { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int NodeStatus { get; set; }
        public int? ActionType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Message { get; set; }
        public string Client { get; set; }
        public string Ip { get; set; }
        public int OrderNumber { get; set; }
    }
}
