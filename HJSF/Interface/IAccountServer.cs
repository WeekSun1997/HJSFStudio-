using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAccountServer
    {
        Task<bool> CheckAuth(string url);
    }
}
