using Cache;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utility.AttributeEntity;
using Utility.Attributes;

namespace HJSF.AOP
{
    public class RedisAOP : BaseCacheAop
    {
        public ICache cache;
        public RedisAOP(ICache _cache)
        {
            cache = _cache;
        }
        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attributes = method.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(CacheAttribute));
            if (attributes == null)
            {
                invocation.Proceed();
            }
            else
            {

                if (attributes is CacheAttribute)
                {

                    var CacheKey = attributes.GetType().GetProperty("CacheName").GetValue(attributes)?.ToString();
                    string K = CacheKey;
                    var CacheType = attributes.GetType().GetProperty("CacheType").GetValue(attributes)?.ToString();
                    var invalidData = attributes.GetType().GetProperty("invalidData").GetValue(attributes);
                    var key = invocation.Arguments.FirstOrDefault()?.ToString();
                    object response;
                    if (!string.IsNullOrEmpty(key))
                    {
                        CacheKey += "_" + key;
                    }
                    if (CacheType == "Query")
                    {
                        var value = cache.GetStringValue(CacheKey).Result;
                        var type = invocation.Method.ReturnType;
                        if (!string.IsNullOrEmpty(value))
                        {

                            var resultTypes = type.GenericTypeArguments;
                            if (type.FullName == "System.Void")
                            {
                                return;
                            }

                            if (typeof(Task).IsAssignableFrom(type))
                            {
                                //返回Task<T>
                                if (resultTypes.Any())
                                {
                                    var resultType = resultTypes.FirstOrDefault();
                                    // 核心1，直接获取 dynamic 类型
                                    dynamic temp = JsonConvert.DeserializeObject(value, resultType);
                                    //response = Task.FromResult(temp);
                                    invocation.ReturnValue = Task.FromResult(temp);
                                    return;
                                }
                                else
                                {
                                    //Task 无返回方法 指定时间内不允许重新运行
                                    response = Task.Yield();
                                }
                            }
                            else
                            {
                                // 核心2，要进行 ChangeType
                                response = Convert.ChangeType(cache.Get<object>(CacheKey), type);
                            }
                        }
                        invocation.Proceed();//直接执行被拦截方法
                        if (typeof(Task).IsAssignableFrom(type))
                        {
                            var resultProperty = type.GetProperty("Result");
                            response = resultProperty.GetValue(invocation.ReturnValue);
                        }
                        else
                        {
                            response = invocation.ReturnValue;
                        }
                        if (response == null) response = string.Empty;
                        var ReturnValue = invocation.ReturnValue;
                        cache.Set(CacheKey, response, TimeSpan.FromDays(Convert.ToInt32(invalidData)));
                    }
                    else
                    {
                        invocation.Proceed();
                        response = invocation.ReturnValue;
                        if (Convert.ToBoolean(response))
                        {
                            cache.Remove(CacheKey);
                            cache.Remove(K);
                        }

                    }


                }
                else
                {
                    invocation.Proceed();
                }
            }
        }

    }
}
