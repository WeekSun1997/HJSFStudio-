using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Attributes;

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




        [InsertEntity]
        public async override Task<bool> InsertAsync<T>(T t)
        {
            HjsfSysChannel entity = t as HjsfSysChannel;
            var query = await base.FisrtEntityAsync<HjsfSysChannel>(a => a.ParentId == entity.ParentId);

            entity.LevelPath = $"{query.LevelPath}{query.Id}/";
            entity.LevelNumber = query.LevelNumber + 1;
            var r = await base.db.UseTranAsync(() =>
            {
                var model = base.db.Insertable<HjsfSysChannel>(entity).ExecuteReturnEntity();
                if (model != null)
                {
                    foreach (var item in entity.ButtonList)
                    {
                        item.ParentId = model.Id;
                        item.LevelPath = model.LevelPath + model.Id + "/";
                        item.CreateDate = entity.CreateDate;
                        item.CreateUserId = entity.CreateUserId;
                        item.CreateUserName = entity.CreateUserName;
                        item.ChannelType = 1;
                        base.Insert<HjsfSysChannel>(item);
                    }
                }
            });
            return r.Data;
        }

        public async override Task<bool> EditAsync<T>(T t, Expression<Func<T, bool>> WhereExpression, Expression<Func<T, bool>> UpdateExpression = null)
        {
            var entity = t as HjsfSysChannel;
            var result = await base.db.UseTranAsync(() =>
              {
                  var parent = base.db.Context.Queryable<HjsfSysChannel>().Where(a => a.Id == entity.ParentId).First();
                  entity.LevelPath = parent.LevelPath + entity.Id + "/";
                  base.db.Context.Updateable<HjsfSysChannel>(entity)
                  .IgnoreColumns(a => a.CreateDate)
                  .IgnoreColumns(a => a.CreateUserId)
                  .IgnoreColumns(a => a.CreateUserName).ExecuteCommand();
                  var buttonList = base.BaseQuery<HjsfSysChannel>(a => a.ParentId == entity.Id);
                  foreach (var item in buttonList)
                  {
                      item.LevelPath = entity.LevelPath + item.Id + "/";
                  }

              });
            return result.Data;

        }

    }
}
