using Newtonsoft.Json;
using System;
using System.Text;


namespace Library
{
    public static class Other
    {
        /// <summary>
        /// 序列化Byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Byte[] SerializeToByte(object str)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.Default.GetBytes(JsonConvert.SerializeObject(str));
        }
        /// <summary>
        /// 反序列化Byte[]
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static T SerializeToObject<T>(Byte[] Bytes)
        {
            if (Bytes == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(Bytes));
        }
        public static string JsonToString(object obj)
        {
            if (obj != null)
            {
                return JsonConvert.SerializeObject(obj);
            }
            return null;
        }
        public static T ObjectToJson<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            var obj = JsonConvert.DeserializeObject<T>(value);
            return obj;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if (value == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }

        public static Type GetCoreType(Type type)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(type);
            else
                return type;
        }
    }
}
