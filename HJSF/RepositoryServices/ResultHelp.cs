using HJSF.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HJSF.RepositoryServices
{
    public class ResultHelp<T> where T : class
    {
        public ResultHelp(ResponseCode Code, string msg, T Data)
        {
            this.Code = Code;
            this.msg = msg;
            this.Data = Data;
        }
        public ResponseCode Code { get; set; }

        public string msg { get; set; }

        public T Data { get; set; }

    }

    public class ResultHelp
    {
        public ResultHelp(ResponseCode Code, string msg)
        {

            this.Code = Code;
            this.msg = msg;
        }
        public ResponseCode Code { get; set; }

        public string msg { get; set; }

    }
}
