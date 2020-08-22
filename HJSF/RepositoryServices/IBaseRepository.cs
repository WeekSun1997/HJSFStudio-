using ISqlSguar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServices
{
    public interface IBaseRepository  
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
        bool Insert<T>(T t, ref string msg) where T : class;
        /// <summary>
        /// 异步插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> InsertAsync<T>(T t) where T : class;
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Edit<T>(T t, ref string msg) where T : class;
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(T t) where T : class;
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Remove<T>(T t, ref string msg) where T : class;
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync<T>(T t) where T : class;
  

        /// <summary>
        /// EF查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<List<T>> Query<T>() where T : class;
    }
}
