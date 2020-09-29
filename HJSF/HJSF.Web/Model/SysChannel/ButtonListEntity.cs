using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model.SysChannel
{ /// <summary>
  /// 按钮列表实体
  /// </summary>
    public class ButtonListEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 模块链接
        /// </summary>
        public string ChannelLink { get; set; }

        /// <summary>
        /// 事件名称（类型为按钮作为点击事件触发名称，类型为菜单作为菜单标识）
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 模块样式
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 模块图标
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// 视图链接
        /// </summary>
        public string ViewLink { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
