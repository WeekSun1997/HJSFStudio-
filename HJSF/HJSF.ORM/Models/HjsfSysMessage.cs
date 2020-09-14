using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysMessage
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int MessageType { get; set; }
        public long ToUserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
