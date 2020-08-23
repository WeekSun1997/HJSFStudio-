using HJSF.ORM.Models;
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
using System.Threading.Tasks;

namespace RepositoryServices
{
    public class BaseRepository : DBServices,IBaseRepository
    {
        public HJSFContext _dbContext { get; set; }

        public BaseRepository() { }


        public BaseRepository(string _ConnectionString) :base(_ConnectionString)
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
        public virtual bool Edit<T>(T t) where T : class
        {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Update(t);
                    return _dbContext.SaveChanges() > 0;

                }
        }
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<bool> EditAsync<T>(T t) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                _dbContext.Set<T>().Update(t);
                return await _dbContext.SaveChangesAsync() > 0;
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        public virtual bool Insert<T>(T t) where T : class
        {
              using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Add(t);
                    return _dbContext.SaveChanges() > 0;
                }
        }

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync<T>(T t) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                _dbContext.Set<T>().Add(t);
                return await _dbContext.SaveChangesAsync();
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
        public virtual  string OnBefore()
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
        public virtual bool Remove<T>(T t) where T : class
        {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Remove(t);
                    return _dbContext.SaveChanges() > 0;
                }
        }
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync<T>(T t) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                _dbContext.Set<T>().Remove(t);
                return await _dbContext.SaveChangesAsync() > 0;
            }
        }
        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> Query<T>(Expression<Func<T, bool>> expression) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                return await _dbContext.Set<T>().Where(expression).ToListAsync();
            }
        }

        public async Task<T> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T:class
        {
            using (_dbContext = new HJSFContext())
            {
                return await _dbContext.Set<T>().Where(WhereExpression).FirstAsync();
            }
        }

        public T FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                return  _dbContext.Set<T>().Where(WhereExpression).FirstOrDefault();
            }
        }
    }
}
