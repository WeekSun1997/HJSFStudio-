using System;
using System.ComponentModel;

namespace HJSF.Enum
{
    public enum ResponseCode:int
    {

        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success = 0,
        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        Error = 500,
        /// <summary>
        /// 链接不存在
        /// </summary>
        [Description("链接不存在")]
        NotFound = 404,
        /// <summary>
        /// 未登录
        /// </summary>
        [Description("未登录")]
        NonLogin = 1001,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("没有操作权限")]
        NonRole = 1002,
        /// <summary>
        /// 数据为空
        /// </summary>
        [Description("数据为空")]
        Null = 2001


    }
}
