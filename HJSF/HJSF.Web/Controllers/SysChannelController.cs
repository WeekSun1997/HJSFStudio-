using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using HJSF.Enum;
using HJSF.ORM.Models;
using HJSF.Web.Model;
using HJSF.Web.Model.SysChannel;
using Interface;
using ISqlSguar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryServices;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class SysChannelController : BaseApiController<HjsfSysChannel, ISysChannelServer>
    {  /// <summary>
       ///  业务接口
       /// </summary>
        public ISysChannelServer _server;
        /// <summary>
        /// 数据库接口
        /// </summary>
        public IDBServices _db;
        /// <summary>
        /// EF接口
        /// </summary>
        public IBaseRepository _baseRepository;
        /// <summary>
        /// 缓存接口
        /// </summary>
        public ICache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="base"></param>
        /// <param name="dB"></param>
        /// <param name="cache"></param>
        public SysChannelController(ISysChannelServer @server, IBaseRepository @base, IDBServices @dB, ICache @cache)
            : base(server, @base, @dB, @cache)
        {
            _server = @server;
            _db = @dB;
            _baseRepository = @base;
            _cache = @cache;
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

            list.Add(new PageChannelListEntity() { Title = "平台首页", Icon = "layui-icon-home", Jump = "/" });

            string sql = @$"select d.id, d.ParentId,d.ChannelName,d.IconName,d.ChannelLink,d.EventName from HJSF_SysUser a 
                            left join HJSF_SysUserRoleMapping b on a.Id=b.UserId
                            left join HJSF_SysRoleChannelMapping c on b.RoleId=c.RoleId
                            left join HJSF_SysChannel d on c.ChannelId=d.id 
                            where a.Id={account.UserId} and ChannelType=0";
            string msg = "";
            var datatable = _db.QueryTableSql(sql, ref msg);
            List<HjsfSysChannel> query = new List<HjsfSysChannel>();
            foreach (DataRow item in datatable.Rows)
            {
                HjsfSysChannel entity = new HjsfSysChannel();
                entity.Id = Convert.ToInt64(item["Id"]);
                entity.ParentId = Convert.ToInt64(item["ParentId"]);
                entity.ChannelName = item["ChannelName"].ToString();
                entity.IconName = item["IconName"].ToString();
                entity.EventName = item["EventName"].ToString();
                entity.ChannelLink = item["ChannelLink"].ToString();
                query.Add(entity);
            }
            try
            {
                var lists = GetPageChannelListLoop(query, 0);
                list.AddRange(lists);
                list = list.Where((x, i) => list.FindIndex(z => z.Title == x.Title) == i).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return new ResponseModel<List<PageChannelListEntity>>(Enum.ResponseCode.Success, "Success", list);
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
        public async Task<ResponseModel<DataTable>> GetButtons([FromForm] ButtonSearch search)
        {
            List<ButtonListEntity> list = new List<ButtonListEntity>();
            string msg = "";
            var account = base.GetAccount();
            if (null == account)
                return new ResponseModel<DataTable>(Enum.ResponseCode.NonLogin, "未登录或者登录超时", null);
            var parent =  base.FisrtEntity<HjsfSysChannel>(o => o.Status == 1 && o.EventName == search.EventName);
            var parentId = parent.Data.Id;
            string sql = $@"select distinct a.ChannelLink,ChannelName,ClassName,EventName,IconName,ViewLink,IsShow from Hjsf_SysChannel a 
                            left join HJSF_SysRoleChannelMapping b on a.id=b.ChannelId
                            left join HJSF_SysUserRoleMapping c on b.RoleId=c.RoleId
                            left join HJSF_SysUser d on c.UserId=d.Id
                            where d.Id=3 and a.Status=1 and a.ChannelType=1 and a.ParentId={parentId}";
            var table = _db.QueryTableSql(sql, ref msg);

            //List<ButtonListEntity> list = await _defaultService.GetButtons(search.EventName);
            return new ResponseModel<DataTable>(ResponseCode.Success, "", table);
        }
    }
}
