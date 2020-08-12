using System;
using System.Threading.Tasks;

namespace Cache
{
    public interface ICache
    {
        Task<string> GetStringValue(string Key);
        Task<bool> SetStringValue(string Key, string value, TimeSpan time);
        //获取 Reids 缓存值

        //获取值，并序列化
        Task<T> Get<T>(string key);

        //保存
        Task<bool> Set(string key, object value, TimeSpan cacheTime);

        //判断是否存在
        Task<bool> Exists(string key);

        //移除某一个缓存值
        Task<bool> Remove(string key);

        Task<bool> HashSet(string Key, string DataKey, object value, TimeSpan? cacheTime);

        Task<bool> HashExists(string Key, string DataKey);

        Task<T> GetHash<T>(string Key, string DataKey);
        //全部清除
        void Clear();
    }
}
