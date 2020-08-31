using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cache;
using HJSF.ORM.Models;
using HJSF.Web.Model;
using HJSF.Web.Model.Login;
using Interface;
using ISqlSguar;
using Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepositoryServices;
using Services;
using Utility;
using Utility.AttributeEntity;

namespace HJSF.Web.Controllers
{
    /// <summary>
    /// 用户操作控制器
    /// </summary>
    [ApiController]

    [Route("v1/[controller]")]
    public class SysUserController : BaseApiController<HjsfSysUser, ISysUserServer>
    {
        /// <summary>
        ///  业务接口
        /// </summary>
        public ISysUserServer _server;
        /// <summary>
        /// 数据库接口
        /// </summary>
        public IDBServices _db;
        /// <summary>
        /// EF接口
        /// </summary>
        public IBaseRepository _baseRepository;
        /// <summary>
        /// 缓存接口
        /// </summary>
        public ICache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="base"></param>
        /// <param name="dB"></param>
        /// <param name="cache"></param>
        public SysUserController(ISysUserServer @server, IBaseRepository @base, IDBServices @dB, ICache @cache)
            : base(server, @base, @dB, @cache)
        {
            _server = @server;
            _db = @dB;
            _baseRepository = @base;
            _cache = @cache;
        }
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVerify")]
        public ActionResult GetVerify(string id)

        {
            var bs = VerifyHelper.Create();
            return File(bs, "image/png");
        }

        /// <summary>
        /// 登录方法-设置token
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ResponseModel<string>> Login([FromForm] SysLoginEntity entity)
        {
            string tokenString = string.Empty;
            var user = await _baseRepository.FisrtEntityAsync<HjsfSysUser>(a => a.Id == 1);
            if (user.Data != null)
            {
                var claims = new[]
               {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(Utility.Constant.AppSetting.Jwt.ExpiresTime)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Iss, Utility.Constant.AppSetting.Jwt.JwtIssuer),
                    new Claim(JwtRegisteredClaimNames.Aud, Utility.Constant.AppSetting.Jwt.JwtAudience),
                    new Claim("User",Other.JsonToString(user.Data)),

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Utility.Constant.AppSetting.Jwt.JwtSecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    //颁发者
                    issuer: Utility.Constant.AppSetting.Jwt.JwtIssuer,
                    //接收者
                    audience: Utility.Constant.AppSetting.Jwt.JwtAudience,
                    //过期时间
                    expires: DateTime.Now.AddMinutes(Utility.Constant.AppSetting.Jwt.ExpiresTime),
                    //签名证书
                    signingCredentials: creds,
                    //自定义参数
                    claims: claims
                    );
                tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                tokenString = $"Bearer {tokenString}";
                HttpContext.Session.Set("User", Other.SerializeToByte(user.Data));

            }
            return new ResponseModel<string>(user.Code, user.msg, tokenString);
        }

        /// <summary>
        /// 获取session
        /// </summary>
        /// <returns></returns>
        [HttpGet("Session"), IgnoreRole]
        [Authorize]

        public ResponseModel<HjsfSysUser> GetSeesion()
        {
            var userbytes = HttpContext.Session.Get("User");
            var user = Other.SerializeToObject<HjsfSysUser>(userbytes);
            return new ResponseModel<HjsfSysUser>(Enum.ResponseCode.Success, "", user);
        }
    }
}
