using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowHistory
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
        public string FlowNodeCode { get; set; }
        public int NodeStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remark { get; set; }
        public string Client { get; set; }
        public string Ip { get; set; }
    }
}
