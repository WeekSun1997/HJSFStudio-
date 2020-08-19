using Interface;
using RepositoryServices;
using System;

namespace Services
{
    public class BaseServer<T> : BaseRepository<T>, IBaseServer<T> where T : class, new()
    {
    }
}
