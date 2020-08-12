using System;

public enum ResponseCode
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    OK = 0,
    /// <summary>
    /// 未找到
    /// </summary>
    [Description("未找到")]
    NoFount = 404,
    /// <summary>
    /// 未授权
    /// </summary>
    [Description("未授权")]
    Unauthorized = 401,
    /// <summary>
    /// 异常
    /// </summary>
    [Description("异常")]
    Error = 500
}
