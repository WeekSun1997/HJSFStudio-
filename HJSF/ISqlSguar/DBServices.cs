using ISqlSguar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Interface.ISqlSguar
{
    public class DBServices : IDBServices
    {
        public  string ConnectionString { get; set; }

        public DBServices() { }
        public DBServices(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public SqlConnection connection;

        public SqlConnection DBContext()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// 增删改方法
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public bool ExceNoQuery(string sql, ref string msg)
        {
            using (SqlConnection conn = DBContext() as SqlConnection)
            {
                try
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    return comm.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;

                    return false;

                }
            }
        }

        /// <summary>
        /// 查询返回DataSet
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public DataSet QuerySetSql(string Sql, ref string msg)
        {
            using (SqlConnection db = DBContext() as SqlConnection)
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(Sql, db);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return null;
                }
            }
        }
        /// <summary>
        /// 查询返回DataTable
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public DataTable QueryTableSql(string Sql, ref string msg)
        {
            using (SqlConnection db = DBContext())
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(Sql, db);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {

                    msg = ex.Message;
                    return null;
                }
            }
        }
        /// <summary>
        /// 事务执行增删改
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <param name="msg">返回异常信息</param>
        /// <returns></returns>
        public bool TransactionExec(string Sql, ref string msg)
        {
            using (SqlConnection db = DBContext() as SqlConnection)
            {
                SqlTransaction transaction = db.BeginTransaction();
                try
                {

                    SqlCommand comm = new SqlCommand(Sql, db);
                    if (comm.ExecuteNonQuery() <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                        transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    transaction.Rollback();
                    return false;
                }

            }
        }
        /// <summary>
        /// 异步执行增删改
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <returns></returns>
        public async Task<bool> ExceNoQueryAsync(string sql)
        {
            using (SqlConnection conn = DBContext() as SqlConnection)
            {
                SqlCommand comm = new SqlCommand(sql, conn);
                var i = await comm.ExecuteNonQueryAsync();
                return i > 0;
            }
        }
    }
}
