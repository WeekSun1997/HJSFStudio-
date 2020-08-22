using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HJSF.RepositoryServices
{
    public class UnitOfWork : IUnitOfWork
    {
        public event Action Events;

        /// <summary>
        /// 提交分布式事务
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    Events?.Invoke();
                    trans.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 异步提交分布式事务
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            return await Task.FromResult(Commit());
        }
    }
}
