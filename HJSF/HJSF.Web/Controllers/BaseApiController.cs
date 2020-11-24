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
using HJSF.RepositoryServices.Models;
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
using SqlSugar;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TService"></typeparam>
    [Route("v1/[controller]")]
    [ApiController]

    public class BaseApiController<T, TService> : ControllerBase
        where T : class, new()
        where TService : IBaseServer<T>
    {
        /// <summary>
        /// 默认使用接口
        /// </summary>
        protected TService _defaultService;

        private ICache _cache;


        /// <summary>
        ///  初始化
        /// </summary>
        /// <param name="service"></param>
        /// <param name="base"></param>
        /// <param name="db"></param>
        public BaseApiController(TService service, ICache cache)
        {
            _defaultService = service;
            _cache = cache;
        }

        /// <summary>
        /// 同步添加方法
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public ResultHelp Add<TEntity>(TEntity t) where TEntity : class, new()
        {
            try
            {
                var i = _defaultService.Insert<TEntity>(t);
                if (i)
                    return new ResultHelp(Enum.ResponseCode.Success, "添加成功");
                else
                    return new ResultHelp(Enum.ResponseCode.Error, "添加失败");
            }
            catch (Exception ex)
            {
                return new ResultHelp(Enum.ResponseCode.Success, ex.Message);
            }

        }
        /// <summary>
        /// 异步添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<ResultHelp> AddEntityAsync<TEntity>(TEntity T) where TEntity : class, new()
        {
            try
            {
                var i = await _defaultService.InsertAsync<TEntity>(T);
                if (i)
                    return new ResultHelp(Enum.ResponseCode.Success, "添加成功");
                else
                    return new ResultHelp(Enum.ResponseCode.Error, "添加失败");
            }
            catch (Exception ex)
            {
                return new ResultHelp(Enum.ResponseCode.Success, ex.Message);
            }
        }
        /// <summary>
        /// 条件获取第一条数据(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <param name="OrderExpression"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression, Expression<Func<T, object>> OrderExpression = null, OrderByType type = OrderByType.Asc) where T : class, new()
        {
            try
            {
                var result = await _defaultService.FisrtEntityAsync<T>(WhereExpression, OrderExpression, type);
                return new ResultHelp<T>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<T>(Enum.ResponseCode.Error, ex.Message, null);
            }
        }
        /// <summary>
        /// 获取第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public ResultHelp<T> FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class, new()
        {
            try
            {
                var result = _defaultService.FisrtEntity<T>(WhereExpression);
                return new ResultHelp<T>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<T>(Enum.ResponseCode.Error, ex.Message, null);
            }
        }

        /// <summary>
        ///查询集合异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public async Task<ResultHelp<List<T>>> QueryListAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class, new()
        {
            try
            {
                var result = await _defaultService.BaseQueryAsync<T>(WhereExpression);
                return new ResultHelp<List<T>>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<List<T>>(Enum.ResponseCode.Error, ex.Message, null);
            }
        }
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public ResultHelp<T> QueryList<T>(Expression<Func<T, bool>> WhereExpression) where T : class, new()
        {
            try
            {
                var result = _defaultService.FisrtEntity<T>(WhereExpression);
                return new ResultHelp<T>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<T>(Enum.ResponseCode.Error, ex.Message, null);
            }
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
        /// <summary>
        /// sql执行查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ResultHelp<List<T>> QuerySql<T>(string sql) where T : class, new()
        {
            try
            {
                var result = _defaultService.QuerySql<T>(sql);
                return new ResultHelp<List<T>>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<List<T>>(Enum.ResponseCode.Error, ex.Message, null);
            }
        }

        /// <summary>
        /// 异步sql执行查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<ResultHelp<List<T>>> QuerySqlAsync<T>(string sql) where T : class, new()
        {
            try
            {
                var result = await _defaultService.QuerySqlAsync<T>(sql);
                return new ResultHelp<List<T>>(Enum.ResponseCode.Success, "", result);
            }
            catch (Exception ex)
            {
                return new ResultHelp<List<T>>(Enum.ResponseCode.Error, ex.Message, null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        /// <param name="UpdateExpression"></param>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        public async Task<ResultHelp> BaseUpdateAsync<TEntity>(TEntity t, Expression<Func<TEntity, bool>> UpdateExpression, Expression<Func<TEntity, bool>> WhereExpression = null)
        where TEntity : class, new()
        {

            try
            {
                var i = await _defaultService.EditAsync<TEntity>(t, WhereExpression, UpdateExpression);
                if (i)
                    return new ResultHelp(Enum.ResponseCode.Success, "修改成功");
                else
                    return new ResultHelp(Enum.ResponseCode.Error, "修改失败");
            }
            catch (Exception ex)
            {
                return new ResultHelp(Enum.ResponseCode.Success, ex.Message);
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public async Task<ResultHelp> BaseRemoveAsync(List<T> entityList)
        {

            var result = await _defaultService.RemoveAsync(entityList);
            if (result)
                return new ResultHelp(Enum.ResponseCode.Success, "删除成功");
            else
                return new ResultHelp(Enum.ResponseCode.Error, "删除失败");
        }


    }
}
