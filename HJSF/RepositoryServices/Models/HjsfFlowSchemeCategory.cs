using HJSF.RepositoryServices.Models;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfFlowSchemeCategory:BaseEntity
    {
       
        public string Title { get; set; }
        public int Sort { get; set; }
    }
}
