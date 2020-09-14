using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysRecord
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public string ChannelName { get; set; }
        public int RecordType { get; set; }
        public string Remark { get; set; }
        public bool IsSuccess { get; set; }
        public string Ip { get; set; }
        public string LinkUrl { get; set; }
        public string RequestParams { get; set; }
    }
}
