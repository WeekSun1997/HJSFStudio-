using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    public partial class HjsfSysChannel
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUserId { get; set; }
        public string UpdateUserName { get; set; }
        public int ChannelType { get; set; }
        public long ParentId { get; set; }
        public int LevelNumber { get; set; }
        public string LevelPath { get; set; }
        public string ChannelName { get; set; }
        public string ChannelLink { get; set; }
        public string ViewLink { get; set; }
        public int Sort { get; set; }
        public string EventName { get; set; }
        public string ClassName { get; set; }
        public string IconName { get; set; }
        public bool IsShow { get; set; }
    }
}
