using Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountServer : IAccountServer
    {
        public async Task<bool> CheckAuth(string url)
        {
            return await Task.Run(() => { return true; });
        }
    }
}
