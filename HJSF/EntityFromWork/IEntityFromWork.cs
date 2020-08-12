using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.EntityFromWork
{
    public interface IEntityFromWork
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(T t);
        /// <summary>
        /// 异步插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> InsertAsync<T>(T t);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Edit<T>(T t);
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(T t);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Remove<T>(T t);
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync<T>(T t);
   
    }
}
