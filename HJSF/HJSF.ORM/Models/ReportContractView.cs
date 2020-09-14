using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class ReportContractView
    {
        public long Id { get; set; }
        public int TenderStatus { get; set; }
        public string Title { get; set; }
        public int ContractType { get; set; }
        public long Orgid { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TheActualAmount { get; set; }
        public long ProjectId { get; set; }
    }
}
