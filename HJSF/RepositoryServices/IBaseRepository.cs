using ISqlSguar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HJSF.RepositoryServices;
using HJSF.ORM.Models;
using SqlSugar;
using HJSF.RepositoryServices.Models;

namespace RepositoryServices
{
    public interface IBaseRepository<T> where T: IRepositoryEntity
    {
        string OnBefore();
        /// <summary>
        /// 操作之后
        /// </summary>
        /// <returns></returns>
        string OnAlert();
        /// <summary>
        /// 更新之前操作
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert<T>(T t) where T : class, new();
        /// <summary>
        /// 异步插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> InsertAsync<T>(T t) where T : class, new();
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int Edit<T>(T t, Expression<Func<T, bool>> UpdateExpression, Expression<Func<T, bool>> WhereExpression) where T : class, new();
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> EditAsync<T>(T t, Expression<Func<T, bool>> UpdateExpression, Expression<Func<T, bool>> WhereExpression) where T : class, new();
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int Remove<T>(T t) where T : class, new();
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> RemoveAsync<T>(T t) where T : class, new();

        /// <summary>
        /// EF查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> BaseQueryAsync<T>(Expression<Func<T, bool>> WhereExpression = null) where T : class;
        /// <summary>
        /// 异步根据条件获取第一个,需要判断是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        Task<T> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression, Expression<Func<T, object>> expression = null, OrderByType type = OrderByType.Asc) where T : class;

        /// <summary>
        /// 同步获取第一个(FirstOrDefault)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        T FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class;

        List<T> BaseQuery<T>(Expression<Func<T, bool>> WhereExpression = null) where T : class;


        Task<List<T>> QuerySqlAsync<T>(string sql) where T : class, new();

        List<T> QuerySql<T>(string sql) where T : class, new();



    }
}
