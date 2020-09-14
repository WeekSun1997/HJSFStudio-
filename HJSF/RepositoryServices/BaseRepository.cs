using HJSF.Enum;
using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using Interface.ISqlSguar;
using ISqlSguar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace RepositoryServices
{
    public class BaseRepository : DBServices, IBaseRepository
    {
        public HJSFContext _dbContext { get; set; }

        public BaseRepository() { }


        public BaseRepository(string _ConnectionString) : base(_ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public virtual ResultHelp Edit<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Update(t);
                    var result = _dbContext.SaveChanges() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);
            }
        }
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<ResultHelp> EditAsync<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Update(t);
                    var result = await _dbContext.SaveChangesAsync() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (AggregateException ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        public virtual ResultHelp Insert<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Add(t);
                    var result = _dbContext.SaveChanges() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);

            }
        }

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<ResultHelp> InsertAsync<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Add(t);
                    var result = await _dbContext.SaveChangesAsync() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (AggregateException ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);
            }
        }
        /// <summary>
        /// 操作之后方法
        /// </summary>
        /// <returns></returns>
        public virtual string OnAlert()
        {
            return "";
        }
        /// <summary>
        ///  操作之前方法
        /// </summary>
        /// <returns></returns>
        public virtual string OnBefore()
        {
            return "";
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual ResultHelp Remove<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Remove(t);
                    var result = _dbContext.SaveChanges() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);
            }

        }
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<ResultHelp> RemoveAsync<T>(T t) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Remove(t);
                    var result = await _dbContext.SaveChangesAsync() > 0;
                    return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
                }
            }
            catch (AggregateException ex)
            {
                return new ResultHelp(ResponseCode.Error, ex.Message);
            }
        }
        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual async Task<ResultHelp<List<T>>> Query<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    var result = await _dbContext.Set<T>().Where(expression).ToListAsync();
                    return new ResultHelp<List<T>>(ResponseCode.Success, "", result);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp<List<T>>(ResponseCode.Error, ex.Message, null);
            }
        }

        public async Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    var query = await _dbContext.Set<T>().Where(WhereExpression).FirstAsync();

                    return new ResultHelp<T>(ResponseCode.Success, "", query);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp<T>(ResponseCode.Error, ex.Message, null);
            }
        }

        public ResultHelp<T> FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    var result = _dbContext.Set<T>().Where(WhereExpression).FirstOrDefault();
                    return new ResultHelp<T>(ResponseCode.Success, null, result);
                }
            }
            catch (Exception ex)
            {
                return new ResultHelp<T>(ResponseCode.Error, ex.Message, null);
            }

        }
        /// <summary>
        /// 获取EF上下文
        /// </summary>
        /// <returns></returns>
        public HJSFContext GetDbContext()
        { 
            var dbcontext = new HJSFContext();
            return dbcontext;
        }
    }
}
