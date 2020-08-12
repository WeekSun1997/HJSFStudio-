using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class AppSettingModel
    {
        /// <summary>
        /// 应用信息
        /// </summary>
        public App App { get; set; }
        /// <summary>
        /// 数据库信息
        /// </summary>
        public Database DataBase { get; set; }
        /// <summary>
        /// 域名信息
        /// </summary>
        public Domain Domain { get; set; }
        /// <summary>
        /// 文件上传信息
        /// </summary>
        public Upload UpLoad { get; set; }
        /// <summary>
        /// 密钥信息
        /// </summary>
        public Encrypt Encrypt { get; set; }
        /// <summary>
        /// Redis配置信息
        /// </summary>
        public Redis Redis { get; set; }
        /// <summary>
        /// JWT配置信息
        /// </summary>
        public Jwt Jwt { get; set; }
    }

    /// <summary>
    /// 应用信息
    /// </summary>
    public class App
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 允许跨域URL
        /// </summary>
        public string[] Cors { get; set; }
        /// <summary>
        /// 是否开启Swagger
        /// </summary>
        public bool IsOpenSwagger { get; set; }
        /// <summary>
        /// ICO注入列表
        /// </summary>
        public string[] IocDllList { get; set; }
        /// <summary>
        /// 控制器dll，用于获取swagger控制器描述
        /// </summary>
        public string ControllerDll { get; set; }

    }

    /// <summary>
    /// 数据库信息
    /// </summary>
    public class Database
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ContextConn { get; set; }
        /// <summary>
        /// 数据库类型 MySql = 0, SqlServer = 1, Sqlite = 2, Oracle = 3, PostgreSQL = 4
        /// </summary>
        public int DbType { get; set; }
    }

    /// <summary>
    /// 域名信息
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string WebDomain { get; set; }
        /// <summary>
        /// 文件服务器域名
        /// </summary>
        public string FileDomain { get; set; }
    }

    /// <summary>
    /// 文件上传信息
    /// </summary>
    public class Upload
    {
        /// <summary>
        /// 文件上传保存文件夹
        /// </summary>
        public string UpLoadDirectory { get; set; }
        /// <summary>
        /// 允许上传图片扩展名
        /// </summary>
        public string[] UpLoadImageExt { get; set; }
        /// <summary>
        /// 允许上传附件扩展名
        /// </summary>
        public string[] UpLoadFileExt { get; set; }
        /// <summary>
        /// 允许上传文件大小
        /// </summary>
        public long MaxLength { get; set; }
    }

    /// <summary>
    /// 密钥信息
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// DES密钥
        /// </summary>
        public string DESEncryptKey { get; set; }
        /// <summary>
        /// Md5加盐
        /// </summary>
        public string Md5EncryptKey { get; set; }
    }

    /// <summary>
    /// Redis配置信息
    /// </summary>
    public class Redis
    {
        /// <summary>
        /// Redis保存的Key前缀，会自动添加到指定的Key名称前
        /// </summary>
        public string RedisSysCustomKey { get; set; }

        /// <summary>
        /// 当前连接的Redis中连接字符串，格式为：127.0.0.1:6379,allowadmin=true,passowrd=pwd,defaultDatabase=1
        /// </summary>
        public string RedisHostConnection { get; set; }
    }

    /// <summary>
    /// JWT配置信息
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// Jwt密钥
        /// </summary>
        public string JwtSecurityKey { get; set; }
        /// <summary>
        /// Jwt 颁发者
        /// </summary>
        public string JwtIssuer { get; set; }
        /// <summary>
        /// Jwt 接收者
        /// </summary>
        public string JwtAudience { get; set; }
        /// <summary>
        /// Jwt超时时间（分钟）
        /// </summary>
        public int ExpiresTime { get; set; }
    }

}
