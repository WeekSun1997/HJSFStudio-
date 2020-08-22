using Interface;
using RepositoryServices;
using System;

namespace Services
{
    public class BaseServer<T> : IBaseServer<T> where T : class, new()
    {
    }
}
