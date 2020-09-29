using HJSF.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJSF.Web.Model
{

    /// <summary>
    /// 返回信息类
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 返回异常信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 无返回数据实体
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="code"></param>
        public ResponseModel(string Msg, ResponseCode code)
        {
            this.Code = code;
            this.Msg = Msg;
        }

    }
    /// <summary>
    /// 有数据返回实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>   
    {
        /// <summary>
        /// 返回异常信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="code"></param>
        /// <param name="Data"></param>
        public ResponseModel(ResponseCode code, string Msg, T Data)
        {
            this.Code = code;
            this.Msg = Msg;
            this.Data = Data;
        }

    }
}
