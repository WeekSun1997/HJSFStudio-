using ISqlSguar;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HJSF.RepositoryServices;

namespace RepositoryServices
{
    public interface IBaseRepository : IDBServices
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
        ResultHelp Insert<T>(T t) where T : class;
        /// <summary>
        /// 异步插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<ResultHelp> InsertAsync<T>(T t) where T : class;
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        ResultHelp Edit<T>(T t) where T : class;
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<ResultHelp> EditAsync<T>(T t) where T : class;
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        ResultHelp Remove<T>(T t) where T : class;
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<ResultHelp> RemoveAsync<T>(T t) where T : class;

        /// <summary>
        /// EF查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ResultHelp<List<T>>> Query<T>(Expression<Func<T, bool>> WhereExpression) where T : class;
        /// <summary>
        /// 异步根据条件获取第一个,需要判断是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        Task<ResultHelp<T>> FisrtEntityAsync<T>(Expression<Func<T, bool>> WhereExpression) where T : class;

        /// <summary>
        /// 同步获取第一个(FirstOrDefault)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="WhereExpression"></param>
        /// <returns></returns>
        ResultHelp<T> FisrtEntity<T>(Expression<Func<T, bool>> WhereExpression) where T : class;
    }
}
