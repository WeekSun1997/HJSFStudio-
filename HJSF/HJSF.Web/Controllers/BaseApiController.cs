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
using HJSF.Web.Model.Login;
using Interface;
using ISqlSguar;
using Library;
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
            return await _baseRepository.InsertAsync<TEntity>(T);
        }
        /// <summary>
        /// 条件获取第一条数据
        /// </summary>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public async Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {

            return await  _baseRepository.FisrtEntityAsync<T>(WhereExpression);
        }

        public  ResultHelp<T> FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {
            return  _baseRepository.FisrtEntity<T>(WhereExpression);
        }

        /// <summary>
        /// 获取当前操作用户信息，null表示未登录
        /// </summary>
        public AccountUser GetAccount()
        {
            if (HttpContext == null) return default;

            try
            {
                if (HttpContext.Items["AccountItem"] is AccountUser account)
                    return account;

                var user = HttpContext.User;
                if (user == null) return default;
                if (!user.Identity.IsAuthenticated) return default;
                var UserId = Convert.ToInt32(user.FindFirst("UserId").Value.ToString());
                var UserName = user.FindFirst("UserName").Value.ToString();
                var OrgId = Convert.ToInt32(user.FindFirst("OrgId").Value.ToString());
                var accounts = new AccountUser { OrgId = OrgId, UserName = UserName, UserId = UserId };
                HttpContext.Items["AccountItem"] = accounts;
                return accounts;
            }
            catch (Exception ex)
            {
            }

            return default;
        }


    }
}
