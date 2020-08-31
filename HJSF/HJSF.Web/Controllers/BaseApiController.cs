using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cache;
using HFJS.Entity.ResponseModel;
using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using Interface;
using ISqlSguar;
using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryServices;
using Services;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TService"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
   
    public class BaseApiController<T, TService> : ControllerBase
        where T : class, new()
        where TService : IBaseServer<T>
    {
        /// <summary>
        /// 默认使用接口
        /// </summary>
        protected TService _defaultService;

        private IDBServices _db;

        private IBaseRepository _baseRepository;

        private ICache _cache;

   
        /// <summary>
        ///  初始化
        /// </summary>
        /// <param name="service"></param>
        /// <param name="base"></param>
        /// <param name="db"></param>
        public BaseApiController(TService service, IBaseRepository @base, IDBServices db, ICache cache)
        {
            _defaultService = service;
            _db = db;
            _baseRepository = @base;
            _cache = cache;
        }

        /// <summary>
        /// 同步添加方法
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ResultHelp AddEntity<TEntity>(TEntity t) where TEntity : class
        {
                return _baseRepository.Insert<TEntity>(t);
        }
        /// <summary>
        /// 异步添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<ResultHelp> AddEntityAsync<TEntity>(TEntity T) where TEntity : class
        {
            return await _baseRepository.InsertAsync<TEntity>(T) ;
        }
        /// <summary>
        /// 条件获取第一条数据
        /// </summary>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public async Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {

            return await _baseRepository.FisrtEntityAsync<T>(WhereExpression);
        }
      
    }
}
