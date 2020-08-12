using HJSF.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HFJS.Entity.ResponseModel
{
    public class BaseResponse
    {
        public ResponseCode Code { get; set; }
        public string Mesagess { get; set; }

        public BaseResponse()
        {
            
        }
        public BaseResponse(ResponseCode _code, string _mesagess)
        {
            Code = _code;
            Mesagess = _mesagess;
        }
    }
}
