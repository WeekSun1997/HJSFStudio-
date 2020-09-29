using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowLineTemp
    {
        public long Id { get; set; }
        public long FlowSchemeId { get; set; }
        public string FlowSchemeCode { get; set; }
        public string LineId { get; set; }
        public string FromNodeCode { get; set; }
        public string ToNodeCode { get; set; }
        public string FromNodeId { get; set; }
        public string ToNodeId { get; set; }
        public string LineCondition { get; set; }
    }
}
