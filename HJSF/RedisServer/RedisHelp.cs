using Library;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class RedisHelp : ICache
    {
        public string _connectionString;

        //默认数据库
        public int _defaultDB;

        public ConnectionMultiplexer redisConnection;
        public RedisHelp() { }
        public RedisHelp(string connectionString)
        {
            _connectionString = connectionString;

            _defaultDB = 0;

        }

        public void Clear()
        {

        }

        public void Conntion()
        {

            if (redisConnection == null)
            {
                this.redisConnection = ConnectionMultiplexer.Connect(_connectionString);
            }
        }

        public async Task<bool> Exists(string key)
        {
            Conntion();
            return await redisConnection.GetDatabase().KeyExistsAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            Conntion();
            var value = await redisConnection.GetDatabase().StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return Library.Other.Deserialize<T>(value);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<string> GetStringValue(string Key)
        {
            Conntion();
            return await redisConnection.GetDatabase().StringGetAsync(Key);
        }

        public async Task<bool> Remove(string key)
        {
            Conntion();
            return await redisConnection.GetDatabase().KeyDeleteAsync(key);
        }

        public async Task<bool> Set(string key, object value, TimeSpan cacheTime)
        {

            if (value != null)
            {
                Conntion();
                byte[] values = Other.SerializeToByte(value);
                return await redisConnection.GetDatabase().StringSetAsync(key, values, cacheTime);
            }
            return false;
        }

        public async Task<bool> HashSet(string Key, string DataKey, object value, TimeSpan cacheTime)
        {

            if (value != null)
            {
                Conntion();
                byte[] values = Library.Other.SerializeToByte(value);
                return await redisConnection.GetDatabase().HashSetAsync(Key, DataKey, values);
            }
            return false;
        }

        public async Task<bool> SetStringValue(string Key, string value, TimeSpan time)
        {
            if (value != null)
            {
                Conntion();
                return await redisConnection.GetDatabase().StringSetAsync(Key, Library.Other.SerializeToByte(value), time);
            }
            return false;
        }
        public async Task<bool> HashExists(string Key, string DataKey)
        {
            Conntion();
            return await redisConnection.GetDatabase().HashExistsAsync(Key, DataKey);
        }

        public async Task<T> GetHash<T>(string Key, string DataKey)
        {
            Conntion();
            var value = await redisConnection.GetDatabase().HashGetAsync(Key, DataKey);
            return Library.Other.SerializeToObject<T>(value);
        }

        public Task<bool> HashSet(string Key, string DataKey, object value, TimeSpan? cacheTime)
        {
            throw new NotImplementedException();
        }
    }
}
