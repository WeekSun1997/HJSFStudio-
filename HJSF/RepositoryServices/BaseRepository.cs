using HJSF.Enum;
using HJSF.ORM.Models;
using HJSF.RepositoryServices;
using HJSF.RepositoryServices.Models;
using Interface.ISqlSguar;
using ISqlSguar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Utility;

namespace RepositoryServices
{
    public class BaseRepository<T>: IBaseRepository<T> where T : IRepositoryEntity, new()
    {

        public SqlSugarClient db;
        public BaseRepository()
        {

            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = "Data Source=.;Initial Catalog=HJSF;User=sa;Password=123.com",
                DbType = SqlSugar.DbType.SqlServer, //SqlSugar.DbType)Constant.AppSetting.DataBase.DbType,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据库，如果存在事务，在事务结束之后释放。
                InitKeyType = InitKeyType.Attribute//从实体特性中读取主键自增列信息
            });
        }


        #region EF Core
        //public HJSFContext _dbContext { get; set; }

        //public BaseRepository() : base("Data Source=.;Initial Catalog=HJSF;User=sa;Password=123.com")
        //{

        //}
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <param name="msg">返回异常信息</param>
        ///// <returns></returns>
        //public virtual ResultHelp Edit<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Update(t);
        //            var result = _dbContext.SaveChanges() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);
        //    }
        //}
        ///// <summary>
        ///// 异步修改
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public virtual async Task<ResultHelp> EditAsync<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Update(t);
        //            var result = await _dbContext.SaveChangesAsync() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (AggregateException ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);
        //    }
        //}
        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <param name="msg">异常信息</param>
        ///// <returns></returns>
        //public virtual ResultHelp Insert<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Add(t);
        //            var result = _dbContext.SaveChanges() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);

        //    }
        //}

        ///// <summary>
        ///// 异步添加
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public virtual async Task<ResultHelp> InsertAsync<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Add(t);
        //            var result = await _dbContext.SaveChangesAsync() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (AggregateException ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);
        //    }
        //}
        ///// <summary>
        ///// 操作之后方法
        ///// </summary>
        ///// <returns></returns>
        //public virtual string OnAlert()
        //{
        //    return "";
        //}
        ///// <summary>
        /////  操作之前方法
        ///// </summary>
        ///// <returns></returns>
        //public virtual string OnBefore()
        //{
        //    return "";
        //}

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public virtual ResultHelp Remove<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Remove(t);
        //            var result = _dbContext.SaveChanges() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);
        //    }

        //}
        ///// <summary>
        ///// 异步删除
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public async Task<ResultHelp> RemoveAsync<T>(T t) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            _dbContext.Set<T>().Remove(t);
        //            var result = await _dbContext.SaveChangesAsync() > 0;
        //            return new ResultHelp(result ? ResponseCode.Success : ResponseCode.Error, null);
        //        }
        //    }
        //    catch (AggregateException ex)
        //    {
        //        return new ResultHelp(ResponseCode.Error, ex.Message);
        //    }
        //}
        ///// <summary>
        ///// 事务执行sql
        ///// </summary>
        ///// <param name="Sql"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public virtual async Task<ResultHelp<List<T>>> Query<T>(Expression<Func<T, bool>> expression, Expression<Func<T, T>> selectExpression = null) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            var result = await _dbContext.Set<T>().Where(expression).Select(selectExpression).ToListAsync();
        //            return new ResultHelp<List<T>>(ResponseCode.Success, "", result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp<List<T>>(ResponseCode.Error, ex.Message, null);
        //    }
        //}

        //public async Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            var query = await _dbContext.Set<T>().Where(WhereExpression).FirstAsync();

        //            return new ResultHelp<T>(ResponseCode.Success, "", query);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp<T>(ResponseCode.Error, ex.Message, null);
        //    }
        //}

        //public ResultHelp<T> FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        //{
        //    try
        //    {
        //        using (_dbContext = new HJSFContext())
        //        {
        //            var result = _dbContext.Set<T>().Where(WhereExpression).FirstOrDefault();
        //            return new ResultHelp<T>(ResponseCode.Success, null, result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultHelp<T>(ResponseCode.Error, ex.Message, null);
        //    }

        //}
        ///// <summary>
        ///// 获取EF上下文
        ///// </summary>
        ///// <returns></returns>
        //public HJSFContext GetDbContext()
        //{
        //    var dbcontext = new HJSFContext();
        //    return dbcontext;
        //}

        //public Task<ResultHelp<List<T>>> Query<T>(Expression<Func<T, bool>> WhereExpression, object ) where T : class
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
        public int Edit<T>(T t, Expression<Func<T, bool>> UpdateExpression, Expression<Func<T, bool>> WhereExpression) where T : class, new()
        {
            return db.Updateable<T>(t).SetColumns(UpdateExpression).Where(WhereExpression).ExecuteCommand();
        }

        public Task<int> EditAsync<T>(T t, Expression<Func<T, bool>> UpdateExpression, Expression<Func<T, bool>> WhereExpression) where T : class, new()
        {
            return db.Updateable<T>(t).SetColumns(UpdateExpression).Where(WhereExpression).ExecuteCommandAsync();
        }

        public T FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class
        { 
            return db.Queryable<T>().WhereIF(WhereExpression != null, WhereExpression).First();
        }

        public async Task<T> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression, Expression<Func<T, object>> expression=null, OrderByType type = OrderByType.Asc) where T : class
        {
            return await db.Queryable<T>().WhereIF(WhereExpression != null, WhereExpression)
                .OrderByIF(expression != null, expression, type)

                .FirstAsync();

        }

        public int Insert<T>(T t) where T : class, new()
        {
            return db.Insertable<T>(t).ExecuteCommand();
        }

        public virtual Task<int> InsertAsync<T>(T t) where T : class, new()
        {
            return db.Insertable<T>(t).ExecuteCommandAsync();
        }

        public string OnAlert()
        {
            throw new NotImplementedException();
        }

        public string OnBefore()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> BaseQueryAsync<T>(Expression<Func<T, bool>> WhereExpression = null) where T : class
        {
            return db.Queryable<T>().WhereIF(WhereExpression != null, WhereExpression).ToListAsync();
        }


        public async Task<List<T>> QuerySqlAsync<T>(string sql) where T : class, new()
        {
            return await db.SqlQueryable<T>(sql).ToListAsync();
        }
        public List<T> QuerySql<T>(string sql) where T : class, new()
        {
            return db.SqlQueryable<T>(sql).ToList();
        }
        public int Remove<T>(T t) where T : class, new()
        {
            return db.Deleteable<T>(t).ExecuteCommand();
        }

        public Task<int> RemoveAsync<T>(T t) where T : class, new()
        {
            return db.Deleteable<T>(t).ExecuteCommandAsync();
        }

        public List<T> BaseQuery<T>(Expression<Func<T, bool>> WhereExpression = null) where T : class
        {
            return db.Queryable<T>().WhereIF(WhereExpression != null, WhereExpression).ToList();
        }
    }
}
