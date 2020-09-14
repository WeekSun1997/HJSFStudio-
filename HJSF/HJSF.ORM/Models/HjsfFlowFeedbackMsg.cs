using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowFeedbackMsg
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public long FlowId { get; set; }
        public string Remark { get; set; }
        public string Client { get; set; }
        public string Ip { get; set; }
    }
}
