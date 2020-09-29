using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowNodeTemp
    {
        public long Id { get; set; }
        public long FlowSchemeId { get; set; }
        public string FlowSchemeCode { get; set; }
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
    }
}
