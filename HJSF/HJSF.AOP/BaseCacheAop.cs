using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HJSF.AOP
{
    public abstract class BaseCacheAop : IInterceptor
    {
        public abstract void Intercept(IInvocation invocation);
         
    }
}
