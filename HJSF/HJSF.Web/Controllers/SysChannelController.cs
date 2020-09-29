using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using HJSF.Enum;
using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using HJSF.Web.Model;
using HJSF.Web.Model.SysChannel;
using Interface;
using ISqlSguar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryServices;
using Utility.AttributeEntity;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>

    public class SysChannelController : BaseApiController<HjsfSysChannel, ISysChannelServer>
    {  /// <summary>
       ///  业务接口
       /// </summary>
        public ISysChannelServer _server;
 
        /// <summary>
        /// 缓存接口
        /// </summary>
        public ICache _cache;
        public IDBServices _dB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="base"></param>
        /// <param name="dB"></param>
        /// <param name="cache"></param>
        public SysChannelController(ISysChannelServer @server,  ICache @cache,IDBServices dB)
            : base(server, @cache)
        {
            _server = @server;
            _cache = @cache;
            _dB = dB;
        }

        /// <summary>
        /// 左侧菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("PageChannelList")]
        public async Task<ResponseModel<List<PageChannelListEntity>>> PageChannelList()
        {
            var account = base.GetAccount();
            if (null == account)
                return new ResponseModel<List<PageChannelListEntity>>(Enum.ResponseCode.NonLogin, "未登录或者登录超时", null);

            List<PageChannelListEntity> list = new List<PageChannelListEntity>();



            string sql = @$"select d.id, d.ParentId,d.ChannelName,d.IconName,d.ChannelLink,d.EventName from HJSF_SysUser a 
                            left join HJSF_SysUserRoleMapping b on a.Id=b.UserId
                            left join HJSF_SysRoleChannelMapping c on b.RoleId=c.RoleId
                            left join HJSF_SysChannel d on c.ChannelId=d.id 
                            where a.Id={account.UserId} and ChannelType=0";
            string msg = "";
            List<HjsfSysChannel> query = await _defaultService.QuerySqlAsync<HjsfSysChannel>(sql);

            try
            {
                var lists = GetPageChannelListLoop(query, 0);
                list.AddRange(lists);
                list = list.Where((x, i) => list.FindIndex(z => z.Title == x.Title) == i).ToList();
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<PageChannelListEntity>>(Enum.ResponseCode.Error, ex.Message, null);
            }
            return new ResponseModel<List<PageChannelListEntity>>(Enum.ResponseCode.Success, "Success", list);
        }


        /// <summary>
        /// 菜单树-不包含按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost("MenuTreeList"), IgnoreRole]
        public async Task<ResponseModel<List<TreeEntity>>> MenuTreeList()
        {
            var channelList = await _defaultService.BaseQueryAsync<HjsfSysChannel>(a => a.ChannelType == 0);
            var list = TreeLoop(channelList, 0, new long[] { });
            return new ResponseModel<List<TreeEntity>>(ResponseCode.Success, "", list);

        }
        private List<TreeEntity> TreeLoop(List<HjsfSysChannel> query, long parentId, long[] defaultValue)
        {
            var q = query.Where(o => o.ParentId == parentId).OrderBy(o => o.Sort).Select(o => new TreeEntity
            {
                Checked = defaultValue.Contains(o.Id),
                Id = o.Id,
                Label = o.ChannelName,
                Children = TreeLoop(query, o.Id, defaultValue)
            }).ToList();
            return q;
        }

        private List<PageChannelListEntity> GetPageChannelListLoop(List<HjsfSysChannel> query, long parentId)
        {
            List<PageChannelListEntity> list = new List<PageChannelListEntity>();
            try
            {
                list = query.Where(o => o.ParentId == parentId).Select(o => new PageChannelListEntity
                {
                    Title = o.ChannelName,
                    Icon = o.IconName,
                    Jump = o.ChannelLink,
                    Name = o.EventName,
                    List = GetPageChannelListLoop(query, o.Id)
                }).ToList();
                list = list.Where((x, i) => list.FindIndex(z => z.Title == x.Title) == i).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }

        /// <summary>
        /// 获取指定连接按钮列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("buttonlist")]
        public async Task<ResponseModel<List<ButtonListEntity>>> GetButtons([FromForm] ButtonSearch search)
        {
            List<ButtonListEntity> list = new List<ButtonListEntity>();
            string msg = "";
            var account = base.GetAccount();
            if (null == account)
                return new ResponseModel<List<ButtonListEntity>>(Enum.ResponseCode.NonLogin, "未登录或者登录超时", null);
            var parent = base.FisrtEntity<HjsfSysChannel>(o => o.Status == 1 && o.EventName == search.EventName);
            var parentId = parent.Data.Id;
            string sql = $@"select distinct a.ChannelLink,ChannelName,ClassName,EventName,IconName,ViewLink,IsShow from Hjsf_SysChannel a 
                            left join HJSF_SysRoleChannelMapping b on a.id=b.ChannelId
                            left join HJSF_SysUserRoleMapping c on b.RoleId=c.RoleId
                            left join HJSF_SysUser d on c.UserId=d.Id
                            where d.Id=3 and a.Status=1 and a.ChannelType=1 and a.ParentId={parentId}";
            var table = _dB.QueryTableSql(sql, ref msg);
            foreach (DataRow item in table.Rows)
            {
                ButtonListEntity entity = new ButtonListEntity();
                entity.ChannelLink = item["ChannelLink"]?.ToString();
                entity.ViewLink = item["ViewLink"]?.ToString();
                entity.IsShow = item["IsShow"] == null ? false : Convert.ToBoolean(item["IsShow"]);
                entity.IconName = item["IconName"]?.ToString();
                entity.ChannelName = item["ChannelName"]?.ToString();
                entity.ClassName = item["ClassName"]?.ToString();
                entity.EventName = item["EventName"]?.ToString();
                list.Add(entity);
            }
            return new ResponseModel<List<ButtonListEntity>>(ResponseCode.Success, "", list);
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("Index")]
        public async Task<ResponsePageHelper<HjsfSysChannel>> Index([FromForm] SysChannelSearch search)
        {

            var query = await _defaultService.LoadPageData(search.ParentId);
            return new ResponsePageHelper<HjsfSysChannel>(query, 100, 1, 100);
        }

        /// <summary>
        /// 获取最大排序
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSort/{id}"), IgnoreRole]
        public async Task<ResponseModel<int>> GetSort([FromRoute] long id)
        {
            var entity = await FisrtEntityAsync<HjsfSysChannel>(a => a.ParentId == id, a => a.Sort, SqlSugar.OrderByType.Desc);
            entity.Data.Sort = entity.Data.Sort + 1;
            return new ResponseModel<int>(entity.Code, entity.msg, entity.Data.Sort);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ResultHelp> Add([FromForm]HjsfSysChannel entity)
            => await base.AddEntityAsync(entity);

    }
}
