using HJSF.RepositoryServices.Models;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HJSF.ORM.Models
{

    [SugarTable("HJSF_SysChannel")]
    public   class HjsfSysChannel:BaseEntity
    {
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

        /// <summary>
        /// 附加-按钮类型描述
        /// </summary>                                                                                                                                                                                                                                                                                                                                               
        [SugarColumn(IsIgnore = true)]
        public List<HjsfSysChannel> ButtonList { get; set; }

        /// <summary>
        /// 上级模块名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ParentChannelName { get; set; }

 
    }
}
