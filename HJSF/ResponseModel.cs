using System;

public class ResponseModel
{
    public ResponseCode Code { get; set; }
    public string Mesagess { get; set; }
    public List<T> Data { get; set; }
    public ResponseModel(ResponseCode _code, string _mesagess)
    {
        code = _code;
        mesagess = _mesagess;
    }
    public ResponseModel(ResponseCode _code, string _mesagess, List<T> _data)
    {
        Code = _code;
        Mesagess = _mesagess;
        Data = _data;
    }
}
