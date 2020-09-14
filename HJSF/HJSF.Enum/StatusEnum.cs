using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HJSF.Enum
{
    /// <summary>
    /// 默认状态枚举
    /// </summary>
    public enum StatusEnum : int
    {

        /// <summary>
        /// 禁用状态
        /// </summary>
        [Description("禁用")]
        Disable = 0,
        /// <summary>
        /// 启用状态
        /// </summary>
        [Description("启用")]
        Enable = 1,
        ///// <summary>
        ///// 回收状态
        ///// </summary>
        //[Description("回收")]
        //Recovery = 2
    }
}
