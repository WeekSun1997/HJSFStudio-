using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HJSF.RepositoryServices
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        event Action Events;
        /// <summary>
        /// 提交事务 使用事务执行 <see cref="Events"/> 事件
        /// </summary>
        bool Commit();
        /// <summary>
        /// 异步方法 - 提交事务 使用事务执行 <see cref="Events"/> 事件
        /// </summary>
        Task<bool> CommitAsync();

      
    }
}
