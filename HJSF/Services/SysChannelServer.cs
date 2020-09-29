using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SysChannelServer : BaseServer<HjsfSysChannel>, ISysChannelServer
    {
        public async Task<List<HjsfSysChannel>> LoadPageData(long ParentId)
        {
            var entity = await FisrtEntityAsync<HjsfSysChannel>(a => a.Id == ParentId);

            var list = await BaseQueryAsync<HjsfSysChannel>(a => a.ParentId == ParentId);
            foreach (var item in list)
            {
                item.ParentChannelName = entity == null ? "一级模块" : entity.ChannelName;
            }
            return list;

        }

        public override Task<int> InsertAsync<T>(T t)
        {
            HjsfSysChannel entity = t as HjsfSysChannel;
            return base.InsertAsync(t);
        }

    }
}
