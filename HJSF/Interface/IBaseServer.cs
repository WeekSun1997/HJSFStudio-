using HJSF.RepositoryServices;
using RepositoryServices;
using System;

namespace Interface
{
    public interface IBaseServer<T>: IBaseRepository<T> where T: class
    {
      
    }
}
