using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowLine
    {
        public long Id { get; set; }
        public long FlowId { get; set; }
        public string FlowCode { get; set; }
        public string LineId { get; set; }
        public string FromNodeCode { get; set; }
        public string ToNodeCode { get; set; }
        public string FromNodeId { get; set; }
        public string ToNodeId { get; set; }
    }
}
