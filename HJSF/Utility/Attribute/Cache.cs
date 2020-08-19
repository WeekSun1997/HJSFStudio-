using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.AttributeEntity
{
   public class CacheAttribute : Attribute
    {
        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int invalidData { get; set; }
        /// <summary>
        /// 环境Key
        /// </summary>
        public string CacheKey { get; set; }
       /// <summary>
       /// 缓存类型
       /// </summary>
        public string CacheType { get; set; }
    }
}
