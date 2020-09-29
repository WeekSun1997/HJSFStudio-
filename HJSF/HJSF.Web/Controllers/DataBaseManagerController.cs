using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HFJS.Entity.ResponseModel;
using HJSF.ORM.Models;
using HJSF.RepositoryServices.Models;
using HJSF.Web.Model;
using Interface;
using ISqlSguar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryServices;
using Services;
using Utility.WebModel;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 数据库操作控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseManagerController : Controller
    {
        /// <summary>
        /// 接口对象
        /// </summary>
        public IDataBaseServer _server;
    
      
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service"></param>
        public DataBaseManagerController(IDataBaseServer service)
        {
            _server = service;
             
        
        }
        ///// <summary>
        ///// 加载所有数据表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("LoadTable")]
        //public Task<ResponseModel<List<ORM.Models.TableEntity>>> LoadDataTable()
        //{
        //    string msg = "";
        //  //  var ds = _repository.QueryTableSql("select * from Hjsf_SysUser", ref msg);
          
          
        //    return null;
        //}

    }
}
