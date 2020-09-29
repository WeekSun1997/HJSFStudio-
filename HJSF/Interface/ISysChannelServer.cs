using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ISysChannelServer : IBaseServer<HjsfSysChannel>
    {
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        Task<List<HjsfSysChannel>> LoadPageData(long ParentId);
    }
}
