using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.WebModel
{
    /// <summary>
    /// 数据库表实体
    /// </summary>
    public class TableEntity
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public string Length { get; set; }
        /// <summary>
        /// 是否自增长
        /// </summary>
        public bool Identity { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public bool Key { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


    }
}
