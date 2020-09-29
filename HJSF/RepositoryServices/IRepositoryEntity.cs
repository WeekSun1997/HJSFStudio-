using System;
using System.Collections.Generic;
using System.Text;

namespace HJSF.RepositoryServices
{
    public interface IRepositoryEntity
    {
        /// <summary>
        /// 记录编号
        /// </summary>
        long Id { get; set; }
    }
}
