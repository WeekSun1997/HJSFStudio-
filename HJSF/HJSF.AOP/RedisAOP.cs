using Cache;
using Castle.DynamicProxy;
using System;

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
            throw new NotImplementedException();
        }

    }
}
