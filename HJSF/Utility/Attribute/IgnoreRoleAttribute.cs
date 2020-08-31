using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.AttributeEntity
{
    /// <summary>
    /// 数据插入时忽略该字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreRoleAttribute : Attribute
    {
    }
}
