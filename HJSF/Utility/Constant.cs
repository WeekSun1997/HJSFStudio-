using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Constant
    {
        /// <summary>
        /// 基本信息配置
        /// </summary>
        public static AppSettingModel AppSetting = new AppSettingModel();


        /// <summary>
        /// Json系列化格式
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSetting => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };



    }
}
