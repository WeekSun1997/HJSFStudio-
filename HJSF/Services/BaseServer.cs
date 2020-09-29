using HJSF.RepositoryServices;
using HJSF.RepositoryServices.Models;
using Interface;
using Microsoft.AspNetCore.Http;
using RepositoryServices;
using System;

namespace Services
{
    public class BaseServer<T> : BaseRepository<T>, IBaseServer<T> where T : IRepositoryEntity, new()
    {
         
    }
}
