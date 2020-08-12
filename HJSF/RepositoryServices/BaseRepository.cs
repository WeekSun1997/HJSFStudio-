using HJSF.ORM.Models;
using Interface.ISqlSguar;
using ISqlSguar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryServices
{
    public class BaseRepository : DBServices, IBaseRepository
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
        /// Sql语句执行返回bool 可重写
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public new virtual bool ExceNoQuery(string sql, ref string msg)
        {
            return base.ExceNoQuery(sql, ref msg);
        }

        /// <summary>
        /// 异步执行Sql 返回bool
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public new virtual Task<bool> ExceNoQueryAsync(string sql)
        {
            return base.ExceNoQueryAsync(sql);
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
        /// 执行sql返回dataset
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public new virtual DataSet QuerySetSql(string Sql, ref string msg)
        {

            return base.QuerySetSql(Sql, ref msg);
        }
        /// <summary>
        /// 执行sql返回datatable
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public new virtual DataTable QueryTableSql(string Sql, ref string msg)
        {
            return base.QueryTableSql(Sql, ref msg);
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
        public new virtual bool TransactionExec(string Sql, ref string msg)
        {
            return base.TransactionExec(Sql, ref msg);
        }


        /// <summary>
        /// 事务执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool TransactionImplement<T>(Func<T, bool> action, T t, ref string msg) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                using (var tran = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        msg = OnBefore();
                        if (!string.IsNullOrEmpty(msg))
                        {
                            tran.Rollback();
                            return false;
                        }
                        if (action(t))
                        {
                            msg = OnAlert();
                            if (!string.IsNullOrEmpty(msg))
                            {
                                tran.Rollback();
                                return false;
                            }
                            tran.Commit();
                            return true;
                        }
                        tran.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 异步执行事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> TransactionImplementAsync<T>(Func<T, bool> action, T t) where T : class
        {
            using (_dbContext = new HJSFContext())
            {
                using (var tran = await _dbContext.Database.BeginTransactionAsync())
                {
                    string msg = OnBefore();
                    if (!string.IsNullOrEmpty(msg))
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (action(t))
                    {
                        msg = OnAlert();
                        if (!string.IsNullOrEmpty(msg))
                        {
                            tran.Rollback();
                            return false;
                        }
                        tran.Commit();
                        return true;
                    }
                    tran.Rollback();
                    return false;
                }
            }
        }

        public List<T> Query<T>() where T : class
        {
            using (_dbContext = new HJSFContext())
            {
               return  _dbContext.Set<T>().ToList();
            }
        }
    }
}
