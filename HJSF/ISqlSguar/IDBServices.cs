using System;
using System.Data;
using System.Threading.Tasks;

namespace ISqlSguar
{
    public interface IDBServices
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        bool TransactionExec(string Sql, ref string msg);
        /// <summary>
        /// 执行Sql返回Table
        /// </summary>
        /// <param name="Sql">执行的Sql</param>
        /// <returns></returns>
        DataTable QueryTableSql(string Sql, ref string msg);
        /// <summary>
        /// 执行Sql返回DataSet
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        DataSet QuerySetSql(string Sql, ref string msg);
        /// <summary>
        /// 执行增删改语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool ExceNoQuery(string sql, ref string msg);
        /// <summary>
        /// 异步执行增删改语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<bool> ExceNoQueryAsync(string sql);
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
    }

}
