using HJSF.RepositoryServices.Models;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public   class HjsfSysMessage:BaseEntity
    {
        
        public string Title { get; set; }
        public string Content { get; set; }
        public int MessageType { get; set; }
        public long ToUserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
