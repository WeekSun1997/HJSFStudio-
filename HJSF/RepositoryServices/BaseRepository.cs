using HJSF.ORM.Models;
using Interface.ISqlSguar;
using ISqlSguar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryServices
{
    public class BaseRepository : IBaseRepository
    {
        public HJSFContext _dbContext { get; set; }

        public BaseRepository()
        {

        }

        
       

       
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public bool Edit<T>(T t, ref string msg) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Update(t);
                    return _dbContext.SaveChanges() > 0;

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(T t) where T : class
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
        public bool Insert<T>(T t, ref string msg) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Add(t);
                    return _dbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 异步添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync<T>(T t) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                _dbContext.Set<T>().Add(t);
                return await _dbContext.SaveChangesAsync() > 0;
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
        public bool Remove<T>(T t, ref string msg) where T : class
        {
            try
            {
                using (_dbContext = new HJSFContext())
                {
                    _dbContext.Set<T>().Remove(t);
                    return _dbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
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
        public async Task<List<T>> Query<T>() where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
        }

    }
}
