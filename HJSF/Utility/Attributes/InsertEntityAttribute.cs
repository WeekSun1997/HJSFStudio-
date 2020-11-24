using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Attributes
{
    public class InsertEntityAttribute : Attribute
    {

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }
    }
}
