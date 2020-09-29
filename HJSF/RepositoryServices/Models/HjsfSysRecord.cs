using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{
    [SugarTable("HJSF_SysRecord")]
    public  class HjsfSysRecord:BaseEntity
    {
        public string ChannelName { get; set; }
        public int RecordType { get; set; }
        public string Remark { get; set; }
        public bool IsSuccess { get; set; }
        public string Ip { get; set; }
        public string LinkUrl { get; set; }
        public string RequestParams { get; set; }
    }
}
